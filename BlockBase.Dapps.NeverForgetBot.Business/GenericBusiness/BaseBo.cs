using BlockBase.Dapps.NeverForgetBot.Business.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Data.Entities.Base;
using BlockBase.Dapps.NeverForgetBot.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.GenericBusiness
{
    public class BaseBo<TEntity> : IBaseBo<TEntity> where TEntity : AuditEntity, IEntity
    {
        private readonly IBaseAuditDao<TEntity, Guid> _dao;
        private readonly IDbOperationExecutor _opExecutor;

        public BaseBo(IBaseAuditDao<TEntity, Guid> dao, IDbOperationExecutor opExecutor)
        {
            _dao = dao;
            _opExecutor = opExecutor;
        }

        #region Create
        public async Task<OperationResult> InsertAsync(TEntity entity)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                entity.CreatedAt = DateTime.UtcNow;
                await _dao.InsertAsync(entity);
            });
        }
        #endregion

        #region Read
        public async Task<OperationResult<TEntity>> GetAsync(Guid id)
        {
            return await _opExecutor.ExecuteOperation<TEntity>(async () =>
            {
                var result = await _dao.GetAsync(id);
                return result;
            });
        }
        #endregion

        #region Delete
        public async Task<OperationResult> DeleteAsync(TEntity entity)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.UtcNow;
                var commentDelete = await _dao.GetAsync(entity.Id);
                await _dao.DeleteAsync(commentDelete);
            });
        }
        #endregion

        #region List
        public async Task<OperationResult<List<TEntity>>> GetAllAsync()
        {
            return await _opExecutor.ExecuteOperation<List<TEntity>>(async () =>
            {
                var result = await _dao.GetAllAsync();
                return result.Select(context => context).ToList();
            });
        }
        #endregion
    }
}
