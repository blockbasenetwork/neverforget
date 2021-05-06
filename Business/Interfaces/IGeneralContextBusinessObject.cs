using BlockBase.Dapps.NeverForget.Business.BusinessModels.OperationResults;
using BlockBase.Dapps.NeverForget.Data.Pocos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.Business.Interfaces
{
    public interface IGeneralContextBusinessObject
    {
        Task<OperationResult<List<GeneralContextPoco>>> GetRecentCalls();
    }
}
