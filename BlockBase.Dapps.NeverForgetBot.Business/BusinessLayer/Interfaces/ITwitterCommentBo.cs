using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.BusinessLayer.Interfaces
{
    public interface ITwitterCommentBo
    {
        Task<OperationResult> InsertAsync(TwitterComment entity);
        Task<OperationResult<TwitterComment>> GetAsync(Guid id);
        Task<OperationResult> DeleteAsync(TwitterComment entity);
        Task<OperationResult<List<TwitterComment>>> GetAllAsync();
    }
}
