using System.Collections.Generic;
using Newtonsoft.Json;

namespace BlockBase.BBLinq.Model.Responses
{
    public abstract class RequestResultBase
    {
        [JsonProperty("succeeded")]
        public bool Succeeded { get; set; }

        [JsonProperty("exception")]
        public string Exception { get; set; }

        [JsonProperty("responseMessage")]
        public string Message { get; set; }
    }

    public class RequestResult<T> : RequestResultBase
    {
        [JsonIgnore]
        public List<OperationResult<T>> Responses { get; set; } = new List<OperationResult<T>>();
    }
}
