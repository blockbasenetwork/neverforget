using System.Collections.Generic;
using System.Threading.Tasks;
using BlockBase.BBLinq.Queries.Base;

namespace BlockBase.BBLinq.Sets.Base
{
    public interface IInsertableSet<in T> : IBlockBaseBaseSet<IInsertableSet<T>>
    {
        public IQuery GetInsertQuery(T records);
        public void BatchInsert(T record);
        public Task InsertAsync(T record);

        public IQuery GetInsertQuery(IEnumerable<T> records);
        public void BatchInsert(IEnumerable<T> records);
        public Task InsertAsync(IEnumerable<T> records);
    }
}
