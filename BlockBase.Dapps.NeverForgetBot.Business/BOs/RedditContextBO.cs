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

        public RedditContextBo(IRedditContextDao dao)
        {
            _dao = dao;
        }

        #region Create
        public async Task<OperationResult> InsertAsync(RedditContextBusinessModel redditContext)
        {
            var executor = new DbOperationExecutor();

            return await executor.ExecuteOperation(async () =>
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
            var executor = new DbOperationExecutor();

            return (OperationResult<RedditContextBusinessModel>)await executor.ExecuteOperation(async () =>
            {
                await _dao.GetAsync(id);
            });
        }
        #endregion

        #region Update
        public async Task<OperationResult> UpdateAsync(RedditContextBusinessModel redditContext)
        {
            var executor = new DbOperationExecutor();

            return await executor.ExecuteOperation(async () =>
            {
                redditContext.UpdatedAt = DateTime.UtcNow;
                await _dao.UpdateAsync(redditContext.ToData());
            });
        }
        #endregion

        #region Delete
        public async Task<OperationResult> DeleteAsync(RedditContextBusinessModel redditContext)
        {
            var executor = new DbOperationExecutor();

            return await executor.ExecuteOperation(async () =>
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
            var executor = new DbOperationExecutor();

            return (OperationResult<List<RedditContextBusinessModel>>)await executor.ExecuteOperation(async () =>
            {
                await _dao.GetAllAsync();
            });
        }
        #endregion

    }
}
