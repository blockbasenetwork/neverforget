using BlockBase.Dapps.NeverForget.Business.BusinessModels;
using BlockBase.Dapps.NeverForget.Business.BusinessModels.OperationResults;
using BlockBase.Dapps.NeverForget.Business.Interfaces;
using BlockBase.Dapps.NeverForget.Common.Enums;
using BlockBase.Dapps.NeverForget.Data.Entities;
using BlockBase.Dapps.NeverForget.Data.Pocos;
using BlockBase.Dapps.NeverForget.DataAccess.Interfaces;
using BlockBase.Dapps.NeverForget.Services.API;
using BlockBase.Dapps.NeverForget.Services.API.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.Business.BusinessObjects
{
    public class RedditContextBusinessObject : BaseAuditBusinessObject<RedditContext>, IRedditContextBusinessObject
    {
        private readonly IRedditContextDataAccessObject _dataAccessObject;
        private readonly IRedditCommentDataAccessObject _commentDataAccessObject;
        private readonly IRedditSubmissionDataAccessObject _submissionDataAccessObject;
        private readonly IRedditContextPocoDataAccessObject _pocoDataAccessObject;
        private readonly RedditCollector _redditCollector;

        string url = "http://neverforgetbot.azurewebsites.net/redditcontexts/details/";

        public RedditContextBusinessObject(IRedditCommentDataAccessObject commentDataAccessObject, IRedditSubmissionDataAccessObject submissionDataAccessObject, IRedditContextPocoDataAccessObject pocoDataAccessObject, RedditCollector redditCollector, IRedditContextDataAccessObject dataAccessObject, IGenericDataAccessObject genericDataAccessObject, ILogger<BaseBusinessObject> logger) : base(dataAccessObject, genericDataAccessObject, logger)
        {
            _dataAccessObject = dataAccessObject;
            _commentDataAccessObject = commentDataAccessObject;
            _submissionDataAccessObject = submissionDataAccessObject;
            _pocoDataAccessObject = pocoDataAccessObject;
            _redditCollector = redditCollector;
        }

        public async Task<OperationResult> FromApiRedditAllComments()
        {
            int lastDate = 0;
            RedditCommentModel[] commentBatch = new RedditCommentModel[] { };

            return await ExecuteOperation(async () =>
            {
                do
                {
                    try
                    {
                        lastDate = _redditCollector.ReadLastCommentDate();
                    }
                    catch (Exception e)
                    {
                        _redditCollector.CreateLastCommentDate(lastDate);
                    }

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
        }

        public async Task<List<OperationResult>> FromApiRedditModel(RedditCommentModel[] commentArray)
        {
            List<OperationResult> result = new List<OperationResult>();
            List<RedditCommentContextPoco> toReply = new List<RedditCommentContextPoco>();

            var commentsList = CheckKeyword(commentArray);
            var commentsToAdd = await _dataAccessObject.GetUniqueComments(commentsList.ToArray());

            for (int i = 0; i < commentsList.Count; i++)
            {
                var opResult = await ExecuteOperation(async () =>
                {
                    #region Create Context
                    var contextModel = new RedditContext()
                    {
                        Id = Guid.NewGuid(),
                        CreatedAt = DateTime.UtcNow,
                        RequestTypeId = (int)CheckRequestType(commentsToAdd[i].Body)
                    };
                    var requestType = CheckRequestType(commentsToAdd[i].Body);
                    await _dataAccessObject.InsertAsync(contextModel);
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
                            await _commentDataAccessObject.InsertAsync(comment);
                            await _commentDataAccessObject.InsertAsync(parentComment);

                            if (await _dataAccessObject.IsContextPresent(contextModel.Id) && await _dataAccessObject.IsCommentPresent(contextModel.Id))
                            {
                                toReply.Add(new RedditCommentContextPoco() { ContextId = contextModel.Id.ToString(), CommentId = comment.CommentId });
                            }
                        }
                        else
                        {
                            var parentSubmission = await GetDefaultSubmissions(comment, contextModel.Id);
                            await _commentDataAccessObject.InsertAsync(comment);
                            await _submissionDataAccessObject.InsertAsync(parentSubmission);

                            if (await _dataAccessObject.IsContextPresent(contextModel.Id) && await _dataAccessObject.IsSubmissionPresent(contextModel.Id))
                            {
                                toReply.Add(new RedditCommentContextPoco() { ContextId = contextModel.Id.ToString(), CommentId = comment.CommentId });
                            }
                        }
                    }
                    else if (requestType == RequestTypeEnum.Post)
                    {
                        var submission = await GetSubmission(comment, contextModel.Id);
                        await _commentDataAccessObject.InsertAsync(comment);
                        await _submissionDataAccessObject.InsertAsync(submission);

                        if (await _dataAccessObject.IsContextPresent(contextModel.Id) && await _dataAccessObject.IsSubmissionPresent(contextModel.Id))
                        {
                            toReply.Add(new RedditCommentContextPoco() { ContextId = contextModel.Id.ToString(), CommentId = comment.CommentId });
                        }
                    }
                    #endregion
                });
                result.Add(opResult);
                //await PublishReplies(toReply);
            }
            return result;
        }

        public async Task<OperationResult> PublishReplies(List<RedditCommentContextPoco> toReply)
        {
            return await ExecuteOperation(async () =>
            {
                foreach (var reply in toReply)
                {
                    _redditCollector.PublishUrl($"{url}{reply.ContextId}", reply.CommentId);
                }
            });
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
                if (Regex.IsMatch(comment.Body, @"(\B!neverforget)", RegexOptions.IgnoreCase) && comment.Author != "NeverForget-Bot")
                {
                    comments.Add(comment);
                }
            }
            return comments;
        }
        #endregion

        public async Task<OperationResult<RedditContextPoco>> GetPocoAsync(Guid id)
        {
            return await ExecuteOperation(async () =>
            {
                var result = await _pocoDataAccessObject.GetRedditContextById(id);
                return result;
            });
        }

        public async Task<OperationResult<List<RedditContextPoco>>> GetAllPocoAsync()
        {
            return await ExecuteOperation(async () =>
            {
                var result = await _pocoDataAccessObject.GetAllRedditContexts();
                return result.ToList();
            });
        }
    }
}
