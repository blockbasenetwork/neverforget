using System;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.OperationResults
{
    public class DbOperationExecutor
    {
        public async Task<OperationResult> ExecuteOperation(Func<Task> func)
        {
            try
            {

                await func.Invoke();


                return new OperationResult() {Success = true};
            }
            catch (Exception ex)
            {

                return new OperationResult() { Success = false, Exception = ex};
            }
        }

        public async Task<OperationResult<TResult>> ExecuteOperations<TResult>(Func<Task<TResult>> func)
        {
            try
            {

                await func.Invoke();


                return new OperationResult<TResult>() { Success = true};
            }
            catch (Exception ex)
            {

                return new OperationResult<TResult>() { Success = false, Exception = ex };
            }
        }
    }
}
