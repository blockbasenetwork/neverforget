using BlockBase.Dapps.NeverForgetBot.Business.BusinessModels;
using BlockBase.Dapps.NeverForgetBot.Business.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Dal.DAOs;
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

        public RedditContextBo(IRedditContextDao dao, IDbOperationExecutor opExecutor)
        {
            _dao = dao;
            _opExecutor = opExecutor;

        }


        public async Task<OperationResult> ProcessRedditInfoAsync(RedditModel[] modelArray)
        {
            //faz post no reddit com comentário com link para os dados

            foreach (RedditModel model in modelArray)
            {
                var boModel = new RedditContextBusinessModel();
                boModel.Id = Guid.NewGuid();
                boModel.Author = model.Author;
                boModel.CommentPost = model.Body;
                boModel.PostingDate = model.Created_Utc;
                boModel.CommentId = model.Id;
                boModel.SubReddit = model.SubReddit;
                boModel.CreatedAt = DateTime.UtcNow;
                boModel.UpdatedAt = boModel.CreatedAt;

                await _dao.InsertAsync(boModel.ToData());
            }
            return new OperationResult() { Success = true };
        }

        #region Create
        public async Task<OperationResult> InsertAsync(RedditContextBusinessModel redditContext)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                redditContext.CreatedAt = DateTime.UtcNow;
                redditContext.UpdatedAt = redditContext.CreatedAt;
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

        #region Update
        public async Task<OperationResult> UpdateAsync(RedditContextBusinessModel redditContext)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                redditContext.UpdatedAt = DateTime.UtcNow;
                await _dao.UpdateAsync(redditContext.ToData());
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
