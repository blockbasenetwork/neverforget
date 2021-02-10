using BlockBase.Dapps.NeverForgetBot.Data.Interfaces;
using System;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Dal.DaoGeneric
{
    public interface IBaseDao<TEntity> where TEntity : class, IEntity
    {
        //Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(Guid id);
        //Task<TEntity> InsertAsync(TEntity entity);
        //Task<TEntity> UpdateAsync(TEntity entity);
        //Task DeleteAsync(TEntity entity);
    }
}
