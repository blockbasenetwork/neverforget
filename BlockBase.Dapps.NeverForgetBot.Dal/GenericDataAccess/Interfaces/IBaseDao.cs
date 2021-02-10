using BlockBase.Dapps.NeverForgetBot.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess.Interfaces
{
    public interface IBaseDao<TEntity, TKey> where TEntity : class, IEntity
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(Guid id);
        Task InsertAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
