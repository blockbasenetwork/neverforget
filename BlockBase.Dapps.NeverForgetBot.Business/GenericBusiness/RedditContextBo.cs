//using BlockBase.Dapps.NeverForgetBot.Business.GenericBusiness.Interfaces;
//using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
//using BlockBase.Dapps.NeverForgetBot.Common.Enums;
//using BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess.Interfaces;
//using BlockBase.Dapps.NeverForgetBot.Data.Entities;
//using BlockBase.Dapps.NeverForgetBot.Services.API;
//using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;

//namespace BlockBase.Dapps.NeverForgetBot.Business.GenericBusiness
//{
//    public class RedditContextBo : BaseBo<RedditContext>, IRedditContextBo
//    {
//        private readonly IBaseAuditDao<RedditContext, Guid> _dao;
//        private readonly IDbOperationExecutor _opExecutor;
//        private readonly IRedditCommentDao _commentDao;
//        private readonly IRedditSubmissionDao _submissionDao;
//        private readonly RedditCollector _redditCollector;

//        public RedditContextBo(IBaseAuditDao<RedditContext, Guid> dao, IDbOperationExecutor opExecutor, IRedditCommentDao commentDao, IRedditSubmissionDao submissionDao, RedditCollector redditCollector) : base(dao, opExecutor)
//        {
//            _dao = dao;
//            _opExecutor = opExecutor;
//            _commentDao = commentDao;
//            _submissionDao = submissionDao;
//            _redditCollector = redditCollector;
//        }

//        //public async Task<List<OperationResult>> FromApiRedditModel(RedditContextModel[] modelArray, RedditCommentModel[] commentArray)
//        //{
//        //    List<OperationResult> result = new List<OperationResult>();

//        //    for (int i = 0; i < modelArray.Length; i++)
//        //    {
//        //        var opResult = await _opExecutor.ExecuteOperation(async () =>
//        //        {
//        //            if (!_commentDao.GetAllAsync().Result.Any(c => c.CommentId == commentArray[i].Id))
//        //            {
//        //                #region Create Context
//        //                var contextModel = new RedditContext()
//        //                {
//        //                    Id = Guid.NewGuid(),
//        //                    CreatedAt = DateTime.UtcNow
//        //                };
//        //                var requestType = CheckRequestType(modelArray[i].Body);
//        //                await _dao.InsertAsync(contextModel);
//        //                #endregion

//        //                #region Get comment with full link
//        //                var comment = commentArray[i].ToData();
//        //                comment.RedditContextId = contextModel.Id;
//        //                if (comment.Link != null)
//        //                {
//        //                    comment.Link = Regex.Replace(comment.Link, @"^(/)", "https://www.reddit.com/");
//        //                }
//        //                else
//        //                {
//        //                    comment.Link = await GetLink(comment);
//        //                }
//        //                #endregion

//        //                #region Request Type conditions
//        //                if (requestType == RequestTypeEnum.Comment || requestType == RequestTypeEnum.Default)
//        //                {
//        //                    var isParent = CheckParentId(comment.ParentId);
//        //                    if (isParent)
//        //                    {
//        //                        var parentComment = await GetDefaultComments(comment, contextModel.Id);
//        //                        await _commentDao.InsertAsync(comment);
//        //                        await _commentDao.InsertAsync(parentComment);
//        //                    }
//        //                    else
//        //                    {
//        //                        var parentSubmission = await GetDefaultSubmissions(comment, contextModel.Id);
//        //                        await _commentDao.InsertAsync(comment);
//        //                        await _submissionDao.InsertAsync(parentSubmission);
//        //                    }
//        //                }
//        //                else if (requestType == RequestTypeEnum.Post)
//        //                {
//        //                    var submission = await GetSubmission(comment, contextModel.Id);
//        //                    await _commentDao.InsertAsync(comment);
//        //                    await _submissionDao.InsertAsync(submission);
//        //                }
//        //                #endregion

//        //                #region To be implemented
//        //                //else if (requestType == RequestTypeEnum.Thread)
//        //                //{
//        //                //    await _commentDao.FromApiRedditCommentModel(commentArray[i], contextModel.Id);
//        //                //    await GetAndInsertAllParentComment(commentArray[i], contextModel.Id);
//        //                //}
//        //                #endregion
//        //            }
//        //        });
//        //        result.Add(opResult);
//        //    }
//        //    return result;
//        //}

//        #region Process Data
//        private async Task<RedditComment> GetDefaultComments(RedditComment comment, Guid id)
//        {
//            var cleanId = Regex.Replace(comment.ParentId, @"^(\bt1_\B)", " ");
//            var commentArray = await _redditCollector.RedditParentCommentInfo(cleanId);
//            var parentToData = commentArray.FirstOrDefault().ToData();
//            parentToData.RedditContextId = id;
//            parentToData.Link = Regex.Replace(parentToData.Link, @"^(/)", "https://www.reddit.com/");
//            return parentToData;
//        }

//        private async Task<RedditSubmission> GetDefaultSubmissions(RedditComment comment, Guid id)
//        {
//            var cleanId = Regex.Replace(comment.ParentId, @"^(\bt3_\B)", " ");
//            var submissionArray = await _redditCollector.RedditSubmissionInfo(cleanId);
//            var parentToData = submissionArray.FirstOrDefault().ToData();
//            parentToData.RedditContextId = id;
//            var permalink = Regex.Replace(parentToData.Link, @"^(\bhttps://www.reddit.com\B)", " ");
//            if (permalink == parentToData.MediaLink) parentToData.MediaLink = " ";
//            return parentToData;
//        }

//        private async Task<RedditSubmission> GetSubmission(RedditComment comment, Guid id)
//        {
//            var cleanId = Regex.Replace(comment.ParentSubmissionId, @"^(\bt3_\B)", " ");
//            var submissionArray = await _redditCollector.RedditSubmissionInfo(cleanId);
//            var submissionToData = submissionArray.FirstOrDefault().ToData();
//            submissionToData.RedditContextId = id;
//            var permalink = Regex.Replace(submissionToData.Link, @"^(\bhttps://www.reddit.com\B)", " ");
//            if (permalink == submissionToData.MediaLink) submissionToData.MediaLink = " ";
//            return submissionToData;
//        }

//        #region To be implemented
//        //private async Task GetAndInsertAllParentComment(RedditCommentModel comment, Guid id)
//        //{
//        //    var parentId = comment.Parent_Id;
//        //    bool checkParent = Regex.IsMatch(parentId, @"^t3_");
//        //    if (!checkParent)
//        //    {
//        //        var cleanId = Regex.Replace(parentId, @"^(\bt1_\B)", " ");
//        //        var result = await _redditCollector.RedditParentCommentInfo(cleanId);
//        //        await _commentDao.FromApiRedditCommentModel(result.FirstOrDefault(), id);
//        //        await GetAndInsertAllParentComment(result.FirstOrDefault(), id);
//        //    }
//        //}
//        #endregion

//        private async Task<string> GetLink(RedditComment comment)
//        {
//            var cleanId = Regex.Replace(comment.ParentSubmissionId, @"^(\bt3_\B)", " ");
//            var commentId = comment.CommentId;
//            var submissionArray = await _redditCollector.RedditSubmissionInfo(cleanId);
//            var sumbissionLink = submissionArray.FirstOrDefault().ToData().Link;
//            return Regex.Replace(sumbissionLink, @"$(/)", $"/{commentId}");
//        }

//        private bool CheckParentId(string id)
//        {
//            bool checkParent = Regex.IsMatch(id, @"^t1_");
//            if (checkParent) return true;
//            else return false;
//        }

//        private RequestTypeEnum CheckRequestType(string body)
//        {
//            if (Regex.IsMatch(body, @"(!neverforgetbot+ +post)"))
//            {
//                return RequestTypeEnum.Post;
//            }
//            else if (Regex.IsMatch(body, @"(!neverforgetbot+ +thread)"))
//            {
//                return RequestTypeEnum.Thread;
//            }
//            else if (Regex.IsMatch(body, @"(!neverforgetbot+ +comment)"))
//            {
//                return RequestTypeEnum.Comment;
//            }
//            else return RequestTypeEnum.Default;
//        }
//        #endregion
//    }
//}
