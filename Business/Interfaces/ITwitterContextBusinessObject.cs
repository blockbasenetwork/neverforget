using BlockBase.Dapps.NeverForget.Business.BusinessModels;
using BlockBase.Dapps.NeverForget.Business.BusinessModels.OperationResults;
using BlockBase.Dapps.NeverForget.Data.Entities;
using BlockBase.Dapps.NeverForget.Services.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.Business.Interfaces
{
    public interface ITwitterContextBusinessObject : IBaseAuditBusinessObject<TwitterContext>
    {
        Task<List<OperationResult>> FromApiTwitterModel(TweetModel[] modelArray);
        Task<OperationResult<List<TwitterContextBusinessModel>>> GetAllPocoAsync();
        Task<OperationResult<TwitterContextBusinessModel>> GetPocoAsync(Guid id);
    }
}
