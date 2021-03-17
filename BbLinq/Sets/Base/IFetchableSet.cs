using System.Threading.Tasks;
using BlockBase.BBLinq.Queries.Base;

namespace BlockBase.BBLinq.Sets.Base
{
    public interface IFetchableSet<T> : IBlockBaseBaseSet<IFetchableSet<T>>
    {
        public ISelectQuery GetGetQuery(object id);

        public void BatchGet(object id);

        public Task<T> GetAsync(object id);
    }
}
