using System;

namespace BlockBase.Dapps.NeverForgetBot.Business.OperationResults
{
    public class OperationResult
    {
        public bool Success { get; set; }
        
        public Exception Exception { get; set; }
    }

    public class OperationResult<T> : OperationResult
    {
        public T Result { get; set; }

        public OperationResult(T result)
        {
            Result = result;
            Success = true;
        }

        public OperationResult(Exception ex)
        {
            Exception = ex;
            Success = false;    
        }
    }
}
