using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Dal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business
{
    public class GenericBO
    {
        public async Task<OperationResult> AddAsync<TEntity>(TEntity entity) where TEntity : class 
        {
            var executor = new DbOperationExecutor();

            return await executor.ExecuteOperation(async () =>
            {
                var dao = new GenericDao();
                await dao.AddAsync<TEntity>(entity);
            });
        }

        public async Task<OperationResult> AddAsync<TEntity>(List<TEntity> entities) where TEntity : class
        {
            var executor = new DbOperationExecutor();

            return await executor.ExecuteOperation(async () =>
            {
                var dao = new GenericDao();
                await dao.AddAsync<TEntity>(entity);
            });
        }

        public async Task<OperationResult> UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            var executor = new DbOperationExecutor();

            return await executor.ExecuteOperation(async () =>
            {
                var dao = new GenericDao();
                await dao.UpdateAsync<TEntity>(entity);
            });
        }

        public async Task<OperationResult> RemoveAsync<TEntity>(TEntity entity) where TEntity : class
        {
            var executor = new DbOperationExecutor();

            return await executor.ExecuteOperation(async () =>
            {
                var dao = new GenericDao();
                await dao.DeleteAsync<TEntity>(entity);
            });
        }

        public async Task<OperationResult> GetAsync<TEntity>(Guid id) where TEntity : class
        {
            var executor = new DbOperationExecutor();

            return await executor.ExecuteOperation<TEntity>(async () =>
            {
                var dao = new GenericDao();
                return await dao.GetAsync<TEntity>(id);
            });
        }

        public async Task<OpResult<List<TEntity>>> GetListAsync<TEntity>() where TEntity : class
        {
            var executor = new DbOperationExecutor();

            return await executor.ExecuteOperation<List<TEntity>>(async () =>
            {
                var dao = new GenericDao();
                return await dao.GetListAsync<TEntity>();
            });
        }
    }
}
