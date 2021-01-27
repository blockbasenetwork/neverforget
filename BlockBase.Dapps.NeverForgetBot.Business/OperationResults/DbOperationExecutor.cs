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


                return new OperationResult();
            }
            catch (Exception ex)
            {

                return new OperationResult();
            }
        }

        public async Task<OperationResult<TResult>> ExecuteOperations<TResult>(Func<Task<TResult>> func)
        {
            try
            {

                await func.Invoke();


                return new OperationResult<TResult>();
            }
            catch (Exception ex)
            {

                return new OperationResult<TResult>();
            }
        }
    }
}
