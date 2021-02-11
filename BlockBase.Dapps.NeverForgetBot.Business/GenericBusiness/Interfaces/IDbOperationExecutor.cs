using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using System;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.GenericBusiness.Interfaces
{
    public interface IDbOperationExecutor
    {
        Task<OperationResult> ExecuteOperation(Func<Task> func);
        Task<OperationResult<TResult>> ExecuteOperation<TResult>(Func<Task<TResult>> func);
    }
}

