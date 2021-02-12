using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.Obsolete.Interfaces
{
    public interface IRedditCommentBo
    {
        Task<OperationResult> InsertAsync(RedditComment entity);
        Task<OperationResult<RedditComment>> GetAsync(Guid id);
        Task<OperationResult> DeleteAsync(RedditComment entity);
        Task<OperationResult<List<RedditComment>>> GetAllAsync();
    }
}
