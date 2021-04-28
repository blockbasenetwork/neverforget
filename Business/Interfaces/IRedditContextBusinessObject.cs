using BlockBase.Dapps.NeverForget.Business.BusinessModels.OperationResults;
using BlockBase.Dapps.NeverForget.Data.Entities;
using BlockBase.Dapps.NeverForget.Data.Pocos;
using BlockBase.Dapps.NeverForget.Services.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.Business.Interfaces
{
    public interface IRedditContextBusinessObject : IBaseBusinessObject<RedditContext>
    {
        Task<OperationResult> FromApiRedditAllComments();
        Task<List<OperationResult>> FromApiRedditModel(RedditCommentModel[] commentArray);
        Task<OperationResult<List<RedditContextPoco>>> GetAllPocoAsync();
        Task<OperationResult<RedditContextPoco>> GetPocoAsync(Guid id);
    }
}
