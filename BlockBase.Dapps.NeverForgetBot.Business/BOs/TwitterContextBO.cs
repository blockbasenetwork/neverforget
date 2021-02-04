﻿using BlockBase.Dapps.NeverForgetBot.Business.BusinessModels;
using BlockBase.Dapps.NeverForgetBot.Business.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Dal.Interfaces;
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
        public async Task<OperationResult> InsertAsync(TwitterContextBusinessModel twitterContext)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                twitterContext.CreatedAt = DateTime.UtcNow;
                twitterContext.UpdatedAt = twitterContext.CreatedAt;
                await _dao.InsertAsync(twitterContext.ToData());
            });
        }
        #endregion

        #region Read
        public async Task<OperationResult<TwitterContextBusinessModel>> GetAsync(Guid id)
        {
            return await _opExecutor.ExecuteOperation<TwitterContextBusinessModel>(async () =>
            {
                var result = await _dao.GetNonDeletedAsync(id);
                return TwitterContextBusinessModel.FromData(result);
            });
        }
        #endregion

        #region Update
        public async Task<OperationResult> UpdateAsync(TwitterContextBusinessModel twitterContext)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                twitterContext.UpdatedAt = DateTime.UtcNow;
                await _dao.UpdateAsync(twitterContext.ToData());
            });
        }
        #endregion

        #region Delete
        public async Task<OperationResult> DeleteAsync(TwitterContextBusinessModel twitterContext)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                twitterContext.IsDeleted = true;
                twitterContext.DeletedAt = DateTime.UtcNow;
                var twitterContextModel = await _dao.GetAsync(twitterContext.Id);
                await _dao.DeleteAsync(twitterContextModel);
            });
        }
        #endregion

        #region List
        public async Task<OperationResult<List<TwitterContextBusinessModel>>> GetAllAsync()
        {
            return await _opExecutor.ExecuteOperation<List<TwitterContextBusinessModel>>(async () =>
            {
                var result = await _dao.GetAllNonDeletedAsync();
                return result.Select(context => TwitterContextBusinessModel.FromData(context)).ToList();
            });
        }
        #endregion
    }
}
