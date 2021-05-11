using BlockBase.Dapps.NeverForget.Business.BusinessModels;
using BlockBase.Dapps.NeverForget.Business.BusinessModels.OperationResults;
using BlockBase.Dapps.NeverForget.Data.Entities;
using BlockBase.Dapps.NeverForget.Services.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.Business.Interfaces
{
    public interface IRedditContextBusinessObject : IBaseAuditBusinessObject<RedditContext>
    {
        Task<OperationResult> FromApiRedditAllComments();
        Task<List<OperationResult>> FromApiRedditModel(RedditCommentModel[] commentArray);
        Task<OperationResult<List<RedditContextBusinessModel>>> GetAllPocoAsync();
        Task<OperationResult<RedditContextBusinessModel>> GetPocoAsync(Guid id);
    }
}
