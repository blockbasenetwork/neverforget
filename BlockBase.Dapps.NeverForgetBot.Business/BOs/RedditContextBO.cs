using BlockBase.Dapps.NeverForgetBot.Business.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Common.Enums;
using BlockBase.Dapps.NeverForgetBot.Dal.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
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

        public RedditContextBo(IRedditContextDao dao, IDbOperationExecutor opExecutor, IRedditSubmissionBo submissionBo, IRedditCommentBo commentBo, IRedditCommentDao commentDao)
        {
            _dao = dao;
            _opExecutor = opExecutor;
            _submissionBo = submissionBo;
            _commentBo = commentBo;
            _commentDao = commentDao;
        }

        public async Task<OperationResult> FromApiRedditModel(RedditContextModel[] modelArray, RedditCommentModel[] commentArray)
        {
            for (var i = 0; i < modelArray.Length; i++)
            {
                if (_commentDao.GetAllNonDeletedAsync().Result.Any(c => c.CommentId == commentArray[i].Id))
                {

                }

                var dataModel = new RedditContext()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow
                };

                var requestType = CheckRequestType(modelArray[i].Body);

                await _dao.InsertAsync(dataModel);

                //var isContained = await _commentBo.GetAllAsync().Result.Result.Contains();


                if (requestType == RequestTypeEnum.Comment || requestType == RequestTypeEnum.Default)
                {
                    await _commentBo.FromApiRedditCommentModel(commentArray[i], dataModel.Id);
                }
                else if (requestType == RequestTypeEnum.Thread)
                {

                }
            }


            return new OperationResult() { Success = true };
        }

        #region Process Data
        //private bool CheckIfExists()

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
