using BlockBase.Dapps.NeverForgetBot.Business.BusinessModels;
using BlockBase.Dapps.NeverForgetBot.Business.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.BOs
{
    public class TwitterSubmissionBo : ITwitterSubmissionBo
    {
        private readonly ITwitterSubmissionDao _dao;
        private readonly IDbOperationExecutor _opExecutor;

        public TwitterSubmissionBo(ITwitterSubmissionDao dao, IDbOperationExecutor opExecutor)
        {
            _dao = dao;
            _opExecutor = opExecutor;
        }

        #region Create
        public async Task<OperationResult> InsertAsync(TwitterSubmissionBusinessModel twitterSubmission)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                twitterSubmission.CreatedAt = DateTime.UtcNow;
                await _dao.InsertAsync(twitterSubmission.ToData());
            });
        }
        #endregion

        #region Read
        public async Task<OperationResult<TwitterSubmissionBusinessModel>> GetAsync(Guid id)
        {
            return await _opExecutor.ExecuteOperation<TwitterSubmissionBusinessModel>(async () =>
            {
                var result = await _dao.GetNonDeletedAsync(id);
                return TwitterSubmissionBusinessModel.FromData(result);
            });
        }
        #endregion

        #region Update
        public async Task<OperationResult> UpdateAsync(TwitterSubmissionBusinessModel twitterSubmission)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                await _dao.UpdateAsync(twitterSubmission.ToData());
            });
        }
        #endregion

        #region Delete
        public async Task<OperationResult> DeleteAsync(TwitterSubmissionBusinessModel twitterSubmission)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                twitterSubmission.IsDeleted = true;
                twitterSubmission.DeletedAt = DateTime.UtcNow;
                var twitterSubmissionModel = await _dao.GetAsync(twitterSubmission.Id);
                await _dao.DeleteAsync(twitterSubmissionModel);
            });
        }
        #endregion

        #region List
        public async Task<OperationResult<List<TwitterSubmissionBusinessModel>>> GetAllAsync()
        {
            return await _opExecutor.ExecuteOperation<List<TwitterSubmissionBusinessModel>>(async () =>
            {
                var result = await _dao.GetAllNonDeletedAsync();
                return result.Select(submission => TwitterSubmissionBusinessModel.FromData(submission)).ToList();
            });
        }
        #endregion
    }
}
