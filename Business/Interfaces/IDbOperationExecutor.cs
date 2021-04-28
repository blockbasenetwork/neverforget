using BlockBase.Dapps.NeverForget.Business.BusinessModels.OperationResults;
using System;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.Business.Interfaces
{
    public interface IDbOperationExecutor
    {
        Task<OperationResult> ExecuteOperation(Func<Task> func);
        Task<OperationResult<TResult>> ExecuteOperation<TResult>(Func<Task<TResult>> func);
    }
}
