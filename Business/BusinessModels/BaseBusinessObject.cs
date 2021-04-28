using BlockBase.Dapps.NeverForget.Business.BusinessModels.OperationResults;
using BlockBase.Dapps.NeverForget.Business.Interfaces;
using BlockBase.Dapps.NeverForget.Data.Interfaces;
using BlockBase.Dapps.NeverForget.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.Business.BusinessModels
{
    public class BaseBusinessObject<TEntity> : IBaseBusinessObject<TEntity> where TEntity : class, IEntity
    {
        private readonly IBaseDataAccessObject<TEntity> _baseDataAccessObject;

        #region Create
        public async Task<OperationResult> InsertAsync(TEntity redditComment)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                redditComment.CreatedAt = DateTime.UtcNow;
                await _baseDataAccessObject.InsertAsync(redditComment);
            });
        }
        #endregion

        #region Read
        public async Task<OperationResult<TEntity>> GetAsync(Guid id)
        {
            return await _opExecutor.ExecuteOperation<TEntity>(async () =>
            {
                var result = await _baseDataAccessObject.GetAsync(id);
                return result;
            });
        }
        #endregion

        #region Delete
        public async Task<OperationResult> DeleteAsync(TEntity redditComment)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                redditComment.IsDeleted = true;
                redditComment.DeletedAt = DateTime.UtcNow;
                var commentDelete = await _baseDataAccessObject.GetAsync(redditComment.Id);
                await _baseDataAccessObject.DeleteAsync(commentDelete);
            });
        }
        #endregion

        #region List
        public async Task<OperationResult<List<TEntity>>> GetAllAsync()
        {
            return await _opExecutor.ExecuteOperation<List<TEntity>>(async () =>
            {
                var result = await _baseDataAccessObject.GetAllAsync();
                return result.Select(context => context).ToList();
            });
        }
        #endregion
    }
}
