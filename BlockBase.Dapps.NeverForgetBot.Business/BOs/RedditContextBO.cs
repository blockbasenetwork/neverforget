using BlockBase.Dapps.NeverForgetBot.Business.BusinessModels;
using BlockBase.Dapps.NeverForgetBot.Business.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Dal.Interfaces;
using System;
using System.Collections.Generic;
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
            return (OperationResult<RedditContextBusinessModel>)await _opExecutor.ExecuteOperation(async () =>
            {
                await _dao.GetAsync(id);
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
            return (OperationResult<List<RedditContextBusinessModel>>)await _opExecutor.ExecuteOperation(async () =>
            {
                await _dao.GetAllAsync();
            });
        }
        #endregion

    }
}
