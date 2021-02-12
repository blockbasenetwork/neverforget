using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.Obsolete.Interfaces
{
    public interface IRedditSubmissionBo
    {
        Task<OperationResult> InsertAsync(RedditSubmission entity);
        Task<OperationResult<RedditSubmission>> GetAsync(Guid id);
        Task<OperationResult> DeleteAsync(RedditSubmission entity);
        Task<OperationResult<List<RedditSubmission>>> GetAllAsync();
    }
}
