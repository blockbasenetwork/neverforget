using System.Collections.Generic;

namespace BlockBase.BBLinq.Model.Responses
{
    public class OperationResult
    {
        public bool Executed { get; set; }
        public string Message { get; set; }
    }

    public class OperationResult<T> : OperationResult
    {
        public IEnumerable<T> Content { get; set; }
    }
}
