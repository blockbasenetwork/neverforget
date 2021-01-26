using System;
using System.Threading.Tasks;
using System.Transactions;

namespace BlockBase.Dapps.NeverForgetBot.Business.OperationResults
{
    public class DbOperationExecutor
    {
        public async Task<OperationResult> ExecuteOperation(Func<Task> func)
        {
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TimeSpan.FromSeconds(10)
            };
            using (var transactionScope = new TransansactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    await func.Invoke();

                    transactionScope.Complete();

                    return new OperationResult(true);
                }
                catch (Exception ex)
                {

                    return new OperationResult(ex);
                }
            }
        }

        public async Task<OperationResult<TResult>> ExecuteOperations<TResult>(Func<Task<TResult>> func)
        {
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TimeSpan.FromSeconds(10)
            };
            using (var transactionScope = new TransansactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    await func.Invoke();

                    transactionScope.Complete();

                    return new OperationResult<TResult>(result);
                }
                catch (Exception ex)
                {

                    return new OperationResult<TResult>(ex);
                }
            }
        }
    }
}
