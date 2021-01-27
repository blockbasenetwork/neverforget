using BlockBase.Dapps.NeverForgetBot.Business.BusinessModels;
using BlockBase.Dapps.NeverForgetBot.Business.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.BOs
{
    public class TwitterContextBo : ITwitterContextBo
    {
        private readonly ITwitterContextDao _dao;

        public TwitterContextBo(ITwitterContextDao dao)
        {
            _dao = dao;
        }

        #region Create
        public async Task<OperationResult> InsertAsync(TwitterContextBusinessModel twitterContext)
        {
            var executor = new DbOperationExecutor();

            return await executor.ExecuteOperation(async () =>
            {
                twitterContext.CreatedAt = DateTime.UtcNow;
                twitterContext.UpdatedAt = twitterContext.CreatedAt;
                await _dao.InsertAsync(twitterContext.ToData());
            });
        }

        //public async Task<OperationResult> AddAsync<TwitterContext>(List<TwitterContext> entities)
        //{
        //    var executor = new DbOperationExecutor();

        //    return await executor.ExecuteOperation(async () =>
        //    {
        //        var dao = new TwitterContextDao();
        //        await dao.InsertAsync<TwitterContext>(entity);
        //    });
        //}
        #endregion

        #region Read
        public async Task<OperationResult<TwitterContextBusinessModel>> GetAsync(Guid id)
        {
            var executor = new DbOperationExecutor();

            return (OperationResult<TwitterContextBusinessModel>)await executor.ExecuteOperation(async () =>
            {
                await _dao.GetAsync(id);
            });
        }
        #endregion

        #region Update
        public async Task<OperationResult> UpdateAsync(TwitterContextBusinessModel twitterContext)
        {
            var executor = new DbOperationExecutor();

            return await executor.ExecuteOperation(async () =>
            {
                twitterContext.UpdatedAt = DateTime.UtcNow;
                await _dao.UpdateAsync(twitterContext.ToData());
            });
        }
        #endregion

        #region Delete
        public async Task<OperationResult> DeleteAsync(TwitterContextBusinessModel twitterContext)
        {
            var executor = new DbOperationExecutor();

            return await executor.ExecuteOperation(async () =>
            {
                twitterContext.DeletedAt = DateTime.UtcNow;
                var twitterContextModel = await _dao.GetAsync(twitterContext.Id);
                await _dao.DeleteAsync(twitterContextModel);
            });
        }
        #endregion

        #region List
        public async Task<OperationResult<List<TwitterContextBusinessModel>>> GetAllAsync()
        {
            var executor = new DbOperationExecutor();

            return (OperationResult<List<TwitterContextBusinessModel>>)await executor.ExecuteOperation(async () =>
            {
                await _dao.GetAllAsync();
            });
        }
        #endregion
    }
}
