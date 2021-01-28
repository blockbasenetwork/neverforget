using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using System;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.Interfaces
{
    public interface IDbOperationExecutor
    {
        Task<OperationResult> ExecuteOperation(Func<Task> func);
        Task<OperationResult<TResult>> ExecuteOperations<TResult>(Func<Task<TResult>> func);
    }
}

