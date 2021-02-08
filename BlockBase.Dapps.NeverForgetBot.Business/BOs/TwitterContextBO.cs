using BlockBase.Dapps.NeverForgetBot.Business.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Dal.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.BOs
{
    public class TwitterContextBo : ITwitterContextBo
    {
        private readonly ITwitterContextDao _dao;
        private readonly IDbOperationExecutor _opExecutor;

        public TwitterContextBo(ITwitterContextDao dao, IDbOperationExecutor opExecutor)
        {
            _dao = dao;
            _opExecutor = opExecutor;
        }

        #region Create
        public async Task<OperationResult> InsertAsync(TwitterContext twitterContext)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                twitterContext.CreatedAt = DateTime.UtcNow;
                await _dao.InsertAsync(twitterContext);
            });
        }
        #endregion

        #region Read
        public async Task<OperationResult<TwitterContext>> GetAsync(Guid id)
        {
            return await _opExecutor.ExecuteOperation<TwitterContext>(async () =>
            {
                var result = await _dao.GetNonDeletedAsync(id);
                return result;
            });
        }
        #endregion

        #region Delete
        public async Task<OperationResult> DeleteAsync(TwitterContext twitterContext)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                twitterContext.IsDeleted = true;
                twitterContext.DeletedAt = DateTime.UtcNow;
                var contextDelete = await _dao.GetAsync(twitterContext.Id);
                await _dao.DeleteAsync(contextDelete);
            });
        }
        #endregion

        #region List
        public async Task<OperationResult<List<TwitterContext>>> GetAllAsync()
        {
            return await _opExecutor.ExecuteOperation<List<TwitterContext>>(async () =>
            {
                var result = await _dao.GetAllNonDeletedAsync();
                return result.Select(context => context).ToList();
            });
        }
        #endregion
    }
}
