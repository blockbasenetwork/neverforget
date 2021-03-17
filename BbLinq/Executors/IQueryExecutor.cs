using System.Collections.Generic;
using System.Threading.Tasks;
using BlockBase.BBLinq.Model.Responses;
using BlockBase.BBLinq.Queries.Base;

namespace BlockBase.BBLinq.Executors
{
    public interface IQueryExecutor
    {
        public Task ExecuteQueryAsync(IQuery query);
        public Task<IEnumerable<TResult>> ExecuteQueryAsync<TResult>(ISelectQuery query);
    }
}
