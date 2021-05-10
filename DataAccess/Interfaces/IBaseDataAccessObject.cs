using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.DataAccess.Interfaces
{
    public interface IBaseDataAccessObject<T> where T : class
    {
        Task<IEnumerable<T>> List();
        Task InsertAsync(T item);
        Task<T> GetAsync(Guid id);
        Task UpdateAsync(T item);
        Task DeleteAsync(T item);
    }
}
