using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Dal.DAOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.BOs
{
    public class RedditContextBO
    {
        public async Task<OperationResult> AddAsync<RedditContext>(RedditContext entity)
        {
            var executor = new DbOperationExecutor();

            return await executor.ExecuteOperation(async () =>
            {
                var dao = new RedditContextDao();
                await dao.InsertAsync<RedditContext>(entity);
            });
        }

        public async Task<OperationResult> AddAsync<RedditContext>(List<RedditContext> entities)
        {
            var executor = new DbOperationExecutor();

            return await executor.ExecuteOperation(async () =>
            {
                var dao = new RedditContextDao();
                await dao.InsertAsync<RedditContext>(entity);
            });
        }

        public async Task<OperationResult> UpdateAsync<RedditContext>(RedditContext entity)
        {
            var executor = new DbOperationExecutor();

            return await executor.ExecuteOperation(async () =>
            {
                var dao = new RedditContextDao();
                await dao.UpdateAsync<RedditContext>(entity);
            });
        }

        public async Task<OperationResult> RemoveAsync<RedditContext>(RedditContext entity)
        {
            var executor = new DbOperationExecutor();

            return await executor.ExecuteOperation(async () =>
            {
                var dao = new RedditContextDao();
                await dao.DeleteAsync<RedditContext>(entity);
            });
        }

        public async Task<OperationResult> GetAsync<RedditContext>(Guid id)
        {
            var executor = new DbOperationExecutor();

            return await executor.ExecuteOperation<RedditContext>(async () =>
            {
                var dao = new RedditContextDao();
                return await dao.GetAsync<RedditContext>(id);
            });
        }

        public async Task<OpResult<List<RedditContext>>> GetListAsync<RedditContext>()
        {
            var executor = new DbOperationExecutor();

            return await executor.ExecuteOperation<List<RedditContext>>(async () =>
            {
                var dao = new RedditContextDao();
                return await dao.ListAsync<RedditContext>();
            });
        }
    }
}
