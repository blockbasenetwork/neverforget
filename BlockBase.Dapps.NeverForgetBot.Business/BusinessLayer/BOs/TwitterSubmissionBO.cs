using BlockBase.Dapps.NeverForgetBot.Business.BusinessLayer.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.BusinessLayer.BOs
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
        public async Task<OperationResult> InsertAsync(TwitterSubmission twitterSubmission)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                twitterSubmission.CreatedAt = DateTime.UtcNow;
                await _dao.InsertAsync(twitterSubmission);
            });
        }
        #endregion

        #region Read
        public async Task<OperationResult<TwitterSubmission>> GetAsync(Guid id)
        {
            return await _opExecutor.ExecuteOperation<TwitterSubmission>(async () =>
            {
                var result = await _dao.GetAsync(id);
                return result;
            });
        }
        #endregion

        #region Delete
        public async Task<OperationResult> DeleteAsync(TwitterSubmission twitterSubmission)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                twitterSubmission.IsDeleted = true;
                twitterSubmission.DeletedAt = DateTime.UtcNow;
                var submissionDelete = await _dao.GetAsync(twitterSubmission.Id);
                await _dao.DeleteAsync(submissionDelete);
            });
        }
        #endregion

        #region List
        public async Task<OperationResult<List<TwitterSubmission>>> GetAllAsync()
        {
            return await _opExecutor.ExecuteOperation<List<TwitterSubmission>>(async () =>
            {
                var result = await _dao.GetAllAsync();
                return result.Select(submission => submission).ToList();
            });
        }
        #endregion
    }
}
