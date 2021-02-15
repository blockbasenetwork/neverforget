using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.BusinessLayer.Interfaces
{
    public interface ITwitterContextBo
    {
        Task<List<OperationResult>> FromApiTwitterModel(TweetModel[] modelArray);
        Task<OperationResult> InsertAsync(TwitterContext entity);
        Task<OperationResult<TwitterContext>> GetAsync(Guid id);
        Task<OperationResult> DeleteAsync(TwitterContext entity);
        Task<OperationResult<List<TwitterContext>>> GetAllAsync();
    }
}
