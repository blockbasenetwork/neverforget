using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.DataAccess.Interfaces
{
    public interface IGenericDataAccessObject
    {
        Task<IEnumerable<T>> ListAsync<T>() where T : class;
        Task InsertAsync<T>(T item) where T : class;
        Task<T> GetAsync<T>(Guid id) where T : class;
        Task<IEnumerable<T>> Find<T>(Expression<Func<T, bool>> condition) where T : class;
        Task UpdateAsync<T>(T item) where T : class;
        Task DeleteAsync<T>(T item) where T : class;
        Task DeleteAsync<T>(Guid id) where T : class;
        Task<int> Count<T>(Expression<Func<T, bool>> condition) where T : class;
        Task<bool> Any<T>(Expression<Func<T, bool>> condition) where T : class;
        Task<T> FirstOrDefault<T>(Expression<Func<T, bool>> condition) where T : class;
    }
}
