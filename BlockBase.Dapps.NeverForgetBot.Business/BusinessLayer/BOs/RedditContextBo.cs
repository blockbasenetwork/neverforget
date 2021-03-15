using BlockBase.Dapps.NeverForgetBot.Business.BusinessLayer.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Common.Enums;
using BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Dal.Queries;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using BlockBase.Dapps.NeverForgetBot.Services.API;
using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.BusinessLayer.BOs
{
    public class RedditContextBo : IRedditContextBo
    {
        private readonly IRedditContextDao _dao;
        private readonly IDbOperationExecutor _opExecutor;
        private readonly IRedditCommentDao _commentDao;
        private readonly IRedditSubmissionDao _submissionDao;
        private readonly IRedditContextPocoDao _pocoDao;
        private readonly RedditCollector _redditCollector;

        string url = "https://localhost:44371/redditcontexts/details/";
        //string contextIdToPublish = string.Empty;

        public RedditContextBo(IRedditContextDao dao, IDbOperationExecutor opExecutor, IRedditSubmissionDao submissionDao, IRedditCommentDao commentDao, IRedditContextPocoDao pocoDao, RedditCollector redditCollector)
        {
            _dao = dao;
            _opExecutor = opExecutor;
            _submissionDao = submissionDao;
            _commentDao = commentDao;
            _pocoDao = pocoDao;
            _redditCollector = redditCollector;
        }


        public async Task<OperationResult> FromApiRedditAllComments()
        {
            int lastDate;
            RedditCommentModel[] commentBatch = new RedditCommentModel[] { };

            var opResult = await _opExecutor.ExecuteOperation(async () =>
            {
                do
                {
                    lastDate = _redditCollector.ReadLastCommentDate();
                    commentBatch = await _redditCollector.RedditCommentInfo(lastDate);
                    if (commentBatch.Length != 0)
                    {
                        await FromApiRedditModel(commentBatch);
                        if (commentBatch[^1] != null)
                        {
                            lastDate = commentBatch[^1].Created_Utc;
                            _redditCollector.CreateLastCommentDate(lastDate);
                        }
                    }
                } while (commentBatch.Length != 0);
            });
            return opResult;
        }

        public async Task<List<OperationResult>> FromApiRedditModel(RedditCommentModel[] commentArray)
        {
            List<OperationResult> result = new List<OperationResult>();
            List<RedditCommentContextPoco> toReply = new List<RedditCommentContextPoco>();

            var commentsList = CheckKeyword(commentArray);
            var commentsToAdd = await _dao.GetUniqueComments(commentsList.ToArray());

            for (int i = 0; i < commentsToAdd.Count; i++)
            {
                var opResult = await _opExecutor.ExecuteOperation(async () =>
                {
                    #region Create Context
                    var contextModel = new RedditContext()
                    {
                        Id = Guid.NewGuid(),
                        CreatedAt = DateTime.UtcNow,
                        RequestTypeId = (int)CheckRequestType(commentsToAdd[i].Body)
                    };
                    var requestType = CheckRequestType(commentsToAdd[i].Body);
                    await _dao.InsertAsync(contextModel);
                    //contextIdToPublish = contextModel.Id.ToString();
                    #endregion

                    #region Get comment with full link
                    var comment = commentsToAdd[i].ToData();
                    comment.RedditContextId = contextModel.Id;
                    if (comment.Link != null)
                    {
                        comment.Link = Regex.Replace(comment.Link, @"^(/)", "https://www.reddit.com/");
                    }
                    else
                    {
                        comment.Link = await GetLink(comment);
                    }
                    #endregion

                    #region Request Type conditions
                    if (requestType == RequestTypeEnum.Comment || requestType == RequestTypeEnum.Default)
                    {
                        var isParent = CheckParentId(comment.ParentId);
                        if (isParent)
                        {
                            var parentComment = await GetDefaultComments(comment, contextModel.Id);
                            var submission = await GetSubmission(comment, contextModel.Id);
                            await _commentDao.InsertAsync(comment);
                            await _commentDao.InsertAsync(parentComment);

                            if (await _dao.IsContextPresent(contextModel.Id) && await _dao.IsSubmissionPresent(contextModel.Id))
                            {
                                toReply.Add(new RedditCommentContextPoco() { ContextId = contextModel.Id.ToString(), CommentId = comment.CommentId });
                                //_redditCollector.PublishUrl($"{url}{contextIdToPublish}", comment.CommentId);
                            }
                        }
                        else
                        {
                            var parentSubmission = await GetDefaultSubmissions(comment, contextModel.Id);
                            var submission = await GetSubmission(comment, contextModel.Id);
                            await _commentDao.InsertAsync(comment);
                            await _submissionDao.InsertAsync(parentSubmission);

                            if (await _dao.IsContextPresent(contextModel.Id) && await _dao.IsSubmissionPresent(contextModel.Id))
                            {
                                toReply.Add(new RedditCommentContextPoco() { ContextId = contextModel.Id.ToString(), CommentId = comment.CommentId });
                                //_redditCollector.PublishUrl($"{url}{contextIdToPublish}", comment.CommentId);
                            }
                        }
                    }
                    else if (requestType == RequestTypeEnum.Post)
                    {
                        var submission = await GetSubmission(comment, contextModel.Id);
                        await _commentDao.InsertAsync(comment);
                        await _submissionDao.InsertAsync(submission);

                        if (await _dao.IsContextPresent(contextModel.Id) && await _dao.IsSubmissionPresent(contextModel.Id))
                        {
                            toReply.Add(new RedditCommentContextPoco() { ContextId = contextModel.Id.ToString(), CommentId = comment.CommentId });
                            //_redditCollector.PublishUrl($"{url}{contextIdToPublish}", comment.CommentId);
                        }
                    }
                    #endregion

                    #region To be implemented
                    //else if (requestType == RequestTypeEnum.Thread)
                    //{
                    //    await _commentBo.FromApiRedditCommentModel(commentArray[i], contextModel.Id);
                    //    await GetAndInsertAllParentComment(commentArray[i], contextModel.Id);
                    //}
                    #endregion

                });
                result.Add(opResult);
                await PublishReplies(toReply);
            }
            return result;
        }

        public async Task<OperationResult> PublishReplies(List<RedditCommentContextPoco> toReply)
        {
            var opResult = await _opExecutor.ExecuteOperation(async () =>
            {
                foreach (var reply in toReply)
                {
                    _redditCollector.PublishUrl($"{url}{reply.ContextId}", reply.CommentId);
                }
            });
            return opResult;
        }

        #region Process Data

        private async Task<RedditComment> GetDefaultComments(RedditComment comment, Guid id)
        {
            var cleanId = Regex.Replace(comment.ParentId, @"^(\bt1_\B)", " ");
            var commentArray = await _redditCollector.RedditParentCommentInfo(cleanId);
            if (commentArray.Length != 0)
            {
                var parentToData = commentArray.First().ToData();
                parentToData.RedditContextId = id;
                parentToData.Link = Regex.Replace(parentToData.Link, @"^(/)", "https://www.reddit.com/");
                return parentToData;
            }
            else
            {
                return new RedditComment();
            }

        }

        private async Task<RedditSubmission> GetDefaultSubmissions(RedditComment comment, Guid id)
        {
            var cleanId = Regex.Replace(comment.ParentId, @"^(\bt3_\B)", " ");
            var submissionArray = await _redditCollector.RedditSubmissionInfo(cleanId);
            if (submissionArray.Length != 0)
            {
                var parentToData = submissionArray.First().ToData();
                parentToData.RedditContextId = id;
                var permalink = Regex.Replace(parentToData.Link, @"^(\bhttps://www.reddit.com\B)", " ");
                if (permalink == parentToData.MediaLink) parentToData.MediaLink = null;
                return parentToData;
            }
            else
            {
                return new RedditSubmission();
            }
        }

        private async Task<RedditSubmission> GetSubmission(RedditComment comment, Guid id)
        {
            var cleanId = Regex.Replace(comment.ParentSubmissionId, @"^(\bt3_\B)", " ");
            var submissionArray = await _redditCollector.RedditSubmissionInfo(cleanId);
            if (submissionArray.Length != 0)
            {
                var submissionToData = submissionArray.First().ToData();
                submissionToData.RedditContextId = id;
                var permalink = Regex.Replace(submissionToData.Link, @"^(\bhttps://www.reddit.com\B)", " ");
                if (permalink == submissionToData.MediaLink) submissionToData.MediaLink = null;
                return submissionToData;
            }
            else
            {
                return new RedditSubmission();
            }
        }

        #region To be implemented
        //private async Task GetAndInsertAllParentComment(RedditCommentModel comment, Guid id)
        //{
        //    var parentId = comment.Parent_Id;
        //    bool checkParent = Regex.IsMatch(parentId, @"^t3_");
        //    if (!checkParent)
        //    {
        //        var cleanId = Regex.Replace(parentId, @"^(\bt1_\B)", " ");
        //        var result = await _redditCollector.RedditParentCommentInfo(cleanId);
        //        await _commentBo.FromApiRedditCommentModel(result.FirstOrDefault(), id);
        //        await GetAndInsertAllParentComment(result.FirstOrDefault(), id);
        //    }
        //}
        #endregion 

        private async Task<string> GetLink(RedditComment comment)
        {
            var cleanId = Regex.Replace(comment.ParentSubmissionId, @"^(\bt3_\B)", " ");
            var commentId = comment.CommentId;
            var submissionArray = await _redditCollector.RedditSubmissionInfo(cleanId);
            var sumbissionLink = submissionArray.First().ToData().Link;
            return Regex.Replace(sumbissionLink, @"$(/)", $"/{commentId}");
        }

        private bool CheckParentId(string id)
        {
            bool checkParent = Regex.IsMatch(id, @"^t1_");
            if (checkParent) return true;
            else return false;
        }

        private RequestTypeEnum CheckRequestType(string body)
        {
            if (Regex.IsMatch(body, @"(\B!neverforget\s+post)", RegexOptions.IgnoreCase))
            {
                return RequestTypeEnum.Post;
            }
            //else if (Regex.IsMatch(body, @"(\B!neverforget\s+thread)", RegexOptions.IgnoreCase))
            //{
            //    return RequestTypeEnum.Thread;
            //}
            else if (Regex.IsMatch(body, @"(\B!neverforget\s+comment)", RegexOptions.IgnoreCase))
            {
                return RequestTypeEnum.Comment;
            }
            else return RequestTypeEnum.Default;
        }

        public List<RedditCommentModel> CheckKeyword(RedditCommentModel[] commentArray)
        {
            List<RedditCommentModel> comments = new List<RedditCommentModel>();
            foreach (var comment in commentArray)
            {
                if (Regex.IsMatch(comment.Body, @"(\B!neverforget)", RegexOptions.IgnoreCase)/* && comment.Author != "NeverForget-Bot"*/)
                {
                    comments.Add(comment);
                }
            }
            return comments;
        }
        #endregion

        #region Create
        public async Task<OperationResult> InsertAsync(RedditContext redditContext)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                redditContext.CreatedAt = DateTime.UtcNow;
                await _dao.InsertAsync(redditContext);
            });
        }
        #endregion

        #region Read
        public async Task<OperationResult<RedditContext>> GetAsync(Guid id)
        {
            return await _opExecutor.ExecuteOperation<RedditContext>(async () =>
            {
                var result = await _dao.GetAsync(id);
                return result;
            });
        }


        public async Task<OperationResult<RedditContextPoco>> GetPocoAsync(Guid id)
        {
            return await _opExecutor.ExecuteOperation<RedditContextPoco>(async () =>
            {
                var result = await _pocoDao.GetRedditContextById(id);
                return result;
            });
        }
        #endregion

        #region Delete
        public async Task<OperationResult> DeleteAsync(RedditContext redditContext)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                redditContext.IsDeleted = true;
                redditContext.DeletedAt = DateTime.UtcNow;
                var contextDelete = await _dao.GetAsync(redditContext.Id);
                await _dao.DeleteAsync(contextDelete);
            });
        }
        #endregion

        #region List
        public async Task<OperationResult<List<RedditContext>>> GetAllAsync()
        {
            return await _opExecutor.ExecuteOperation<List<RedditContext>>(async () =>
            {
                var result = await _dao.GetAllAsync();
                return result.Select(context => context).ToList();
            });
        }


        public async Task<OperationResult<List<RedditContextPoco>>> GetAllPocoAsync()
        {
            return await _opExecutor.ExecuteOperation<List<RedditContextPoco>>(async () =>
            {
                var result = await _pocoDao.GetAllRedditContexts();
                return result.Select(context => context).ToList();
            });
        }
        #endregion
    }
}
