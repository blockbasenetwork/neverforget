using BlockBase.Dapps.NeverForgetBot.Business.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Common.Enums;
using BlockBase.Dapps.NeverForgetBot.Dal.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Services.API;
using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.BOs
{
    public class RedditContextBo : IRedditContextBo
    {
        private readonly IRedditContextDao _dao;
        private readonly IDbOperationExecutor _opExecutor;
        private readonly IRedditSubmissionBo _submissionBo;
        private readonly IRedditCommentBo _commentBo;
        private readonly IRedditCommentDao _commentDao;
        private readonly RedditCollector _redditCollector;

        public RedditContextBo(IRedditContextDao dao, IDbOperationExecutor opExecutor, IRedditSubmissionBo submissionBo, IRedditCommentBo commentBo, IRedditCommentDao commentDao, RedditCollector redditCollector)
        {
            _dao = dao;
            _opExecutor = opExecutor;
            _submissionBo = submissionBo;
            _commentBo = commentBo;
            _commentDao = commentDao;
            _redditCollector = redditCollector;
        }

        public async Task<OperationResult> FromApiRedditModel(RedditContextModel[] modelArray, RedditCommentModel[] commentArray)
        {
            for (var i = 0; i < modelArray.Length; i++)
            {
                if (!_commentDao.GetAllNonDeletedAsync().Result.Any(c => c.CommentId == commentArray[i].Id))
                {
                    #region Create Context
                    var contextModel = new RedditContext()
                    {
                        Id = Guid.NewGuid(),
                        CreatedAt = DateTime.UtcNow
                    };
                    var requestType = CheckRequestType(modelArray[i].Body);
                    await _dao.InsertAsync(contextModel);
                    #endregion

                    if (requestType == RequestTypeEnum.Comment || requestType == RequestTypeEnum.Default)
                    {
                        await _commentBo.FromApiRedditCommentModel(commentArray[i], contextModel.Id);
                        await GetAndInsertParentComment(commentArray[i], contextModel.Id);
                    }
                    if (requestType == RequestTypeEnum.Thread)
                    {
                        await _commentBo.FromApiRedditCommentModel(commentArray[i], contextModel.Id);
                        await GetAndInsertAllParentComment(commentArray[i], contextModel.Id);
                    }
                    else if (requestType == RequestTypeEnum.Post)
                    {
                        await _commentBo.FromApiRedditCommentModel(commentArray[i], contextModel.Id);
                        await GetAndInsertSubmission(commentArray[i], contextModel.Id);
                    }
                }
            }
            return new OperationResult() { Success = true };
        }

        #region Process Data
        private async Task<OperationResult> GetAndInsertParentComment(RedditCommentModel comment, Guid id)
        {
            var parentId = comment.Parent_Id;
            bool checkParent = Regex.IsMatch(parentId, @"^t3_");
            if (!checkParent)
            {
                var cleanId = Regex.Replace(parentId, @"^(\bt1_\B)", " ");
                var result = await _redditCollector.RedditParentCommentInfo(cleanId);
                await _commentBo.FromApiRedditCommentModel(result.FirstOrDefault(), id);
            }
            else
            {
                var cleanId = Regex.Replace(parentId, @"^(\bt3_\B)", " ");
                var submission = await _redditCollector.RedditSubmissionInfo(cleanId);
                await _submissionBo.FromApiRedditSubmissionModel(submission.FirstOrDefault(), id);
            }
            return new OperationResult() { Success = true };
        }

        private async Task<OperationResult> GetAndInsertAllParentComment(RedditCommentModel comment, Guid id)
        {
            var parentId = comment.Parent_Id;
            bool checkParent = Regex.IsMatch(parentId, @"^t3_");
            if (!checkParent)
            {
                var cleanId = Regex.Replace(parentId, @"^(\bt1_\B)", " ");
                var result = await _redditCollector.RedditParentCommentInfo(cleanId);
                await _commentBo.FromApiRedditCommentModel(result.FirstOrDefault(), id);
                await GetAndInsertAllParentComment(result.FirstOrDefault(), id);
            }
            return new OperationResult() { Success = true };
        }
        private async Task<OperationResult> GetAndInsertSubmission(RedditCommentModel comment, Guid id)
        {
            var parentId = comment.Link_Id;
            var cleanId = Regex.Replace(parentId, @"^(\bt3_\B)", " ");
            var submission = await _redditCollector.RedditSubmissionInfo(cleanId);
            await _submissionBo.FromApiRedditSubmissionModel(submission.FirstOrDefault(), id);
            return new OperationResult() { Success = true };
        }

        private RequestTypeEnum CheckRequestType(string body)
        {
            if (body.ToLower().Contains("!neverforgetbot post")) //Regex(!neverforgetbot+ +post)
            {
                return RequestTypeEnum.Post;
            }
            else if (Regex.IsMatch(body, @"(!neverforgetbot+ +thread)"))
            {
                return RequestTypeEnum.Thread;
            }
            else return RequestTypeEnum.Comment;
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
                var result = await _dao.GetNonDeletedAsync(id);
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
                var result = await _dao.GetAllNonDeletedAsync();
                return result.Select(context => context).ToList();
            });
        }
        #endregion

    }
}
