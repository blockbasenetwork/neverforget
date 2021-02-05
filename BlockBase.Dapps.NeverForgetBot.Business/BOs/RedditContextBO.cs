using BlockBase.Dapps.NeverForgetBot.Business.BusinessModels;
using BlockBase.Dapps.NeverForgetBot.Business.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Common.Enums;
using BlockBase.Dapps.NeverForgetBot.Dal.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.BOs
{
    public class RedditContextBo : IRedditContextBo
    {
        private readonly IRedditContextDao _dao;
        private readonly IDbOperationExecutor _opExecutor;
        private readonly IRedditSubmissionBo _submissionBo;
        private readonly IRedditCommentBo _commentBo;


        public RedditContextBo(IRedditContextDao dao, IDbOperationExecutor opExecutor, IRedditSubmissionBo submissionBo, IRedditCommentBo commentBo)
        {
            _dao = dao;
            _opExecutor = opExecutor;
            _submissionBo = submissionBo;
            _commentBo = commentBo;
        }

        public async Task<OperationResult> FromApiRedditModel(RedditContextModel[] modelArray, RedditCommentModel[] commentArray)
        {
            foreach (var model in modelArray)
            {
                var boModel = new RedditContextBusinessModel()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow
                };
                var requestType = CheckRequestType(model.Body);

                if (requestType == RequestTypeEnum.Post)
                {
                    await _commentBo.FromApiRedditCommentModel(commentArray, boModel.Id);
                }



                var result = boModel.ToData();
                await _dao.InsertAsync(result);
            }

            return new OperationResult() { Success = true };
        }

        #region Process Data
        private RequestTypeEnum CheckRequestType(string body)
        {
            if (body.ToLower().Contains("!neverforgetbot post"))
            {
                return RequestTypeEnum.Post;
            }
            else if (body.ToLower().Contains("!neverforgetbot thread"))
            {
                return RequestTypeEnum.Thread;
            }
            else return RequestTypeEnum.Comment;
        }
        #endregion

        #region Create
        public async Task<OperationResult> InsertAsync(RedditContextBusinessModel redditContext)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                redditContext.CreatedAt = DateTime.UtcNow;
                await _dao.InsertAsync(redditContext.ToData());
            });
        }
        #endregion

        #region Read
        public async Task<OperationResult<RedditContextBusinessModel>> GetAsync(Guid id)
        {
            return await _opExecutor.ExecuteOperation<RedditContextBusinessModel>(async () =>
            {
                var result = await _dao.GetNonDeletedAsync(id);
                return RedditContextBusinessModel.FromData(result);
            });
        }
        #endregion

        #region Delete
        public async Task<OperationResult> DeleteAsync(RedditContextBusinessModel redditContext)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                redditContext.IsDeleted = true;
                redditContext.DeletedAt = DateTime.UtcNow;
                var redditContextModel = await _dao.GetAsync(redditContext.Id);
                await _dao.DeleteAsync(redditContextModel);
            });
        }
        #endregion

        #region List
        public async Task<OperationResult<List<RedditContextBusinessModel>>> GetAllAsync()
        {
            return await _opExecutor.ExecuteOperation<List<RedditContextBusinessModel>>(async () =>
            {
                var result = await _dao.GetAllNonDeletedAsync();
                return result.Select(context => RedditContextBusinessModel.FromData(context)).ToList();
            });
        }
        #endregion

    }
}
