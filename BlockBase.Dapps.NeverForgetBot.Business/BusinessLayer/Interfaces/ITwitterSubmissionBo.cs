using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.BusinessLayer.Interfaces
{
    public interface ITwitterSubmissionBo
    {
        Task<OperationResult> InsertAsync(TwitterSubmission entity);
        Task<OperationResult<TwitterSubmission>> GetAsync(Guid id);
        Task<OperationResult> DeleteAsync(TwitterSubmission entity);
        Task<OperationResult<List<TwitterSubmission>>> GetAllAsync();
    }
}
