using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.BusinessLayer.Interfaces
{
    public interface IRedditContextBo
    {
        Task<List<OperationResult>> FromApiRedditModel(RedditCommentModel[] commentArray);
        Task<OperationResult> InsertAsync(RedditContext entity);
        Task<OperationResult<RedditContext>> GetAsync(Guid id);
        Task<OperationResult> DeleteAsync(RedditContext entity);
        Task<OperationResult<List<RedditContext>>> GetAllAsync();
        Task<OperationResult<List<RedditContextPoco>>> GetAllPocoAsync();
        Task<OperationResult<RedditContextPoco>> GetPocoAsync(Guid id);
    }
}
