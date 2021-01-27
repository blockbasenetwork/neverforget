using BlockBase.BBLinq.Results;
using BlockBase.Dapps.NeverForgetBot.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Dal.Interfaces.Base
{
    public interface IBaseDao<TEntity> where TEntity : class, IEntity
    {
        Task InsertAsync(TEntity entity);
        Task<TEntity> GetAsync(Guid id);
        Task<List<TEntity>> GetAllAsync();
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
