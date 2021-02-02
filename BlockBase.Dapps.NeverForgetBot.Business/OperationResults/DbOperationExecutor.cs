using BlockBase.Dapps.NeverForgetBot.Business.Interfaces;
using System;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.OperationResults
{
    public class DbOperationExecutor : IDbOperationExecutor
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

        public async Task<OperationResult<TResult>> ExecuteOperation<TResult>(Func<Task<TResult>> func)
        {
            try
            {

                var result = await func.Invoke();


                return new OperationResult<TResult>(result);
            }
            catch (Exception ex)
            {

                return new OperationResult<TResult>(ex);
            }
        }
    }
}
