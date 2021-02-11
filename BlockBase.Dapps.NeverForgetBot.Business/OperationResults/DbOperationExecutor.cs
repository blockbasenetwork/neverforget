using BlockBase.Dapps.NeverForgetBot.Business.GenericBusiness.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.OperationResults
{
    public class DbOperationExecutor : IDbOperationExecutor
    {
        private readonly ILogger<DbOperationExecutor> _logger;

        public DbOperationExecutor(ILogger<DbOperationExecutor> logger)
        {
            _logger = logger;
        }

        public async Task<OperationResult> ExecuteOperation(Func<Task> func)
        {
            try
            {

                await func.Invoke();


                return new OperationResult() {Success = true};
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
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
                _logger.LogError(ex.ToString());
                return new OperationResult<TResult>(ex);
            }
        }
    }
}
