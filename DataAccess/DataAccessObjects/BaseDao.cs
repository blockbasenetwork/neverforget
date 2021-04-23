using BlockBase.Dapps.NeverForget.Data.Context;
using BlockBase.Dapps.NeverForget.Data.Interfaces;
using BlockBase.Dapps.NeverForget.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.DataAccess.DataAccessObjects
{
    public abstract class BaseDao<TEntity> : IBaseDao<TEntity> where TEntity : class, IEntity
    {
        #region Create
        public async Task InsertAsync(TEntity entity)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                await context.Set<TEntity>().InsertAsync(entity);
            }
        }
        #endregion

        #region Read
        public virtual async Task<TEntity> GetAsync(Guid id)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var result = await context.Set<TEntity>().GetAsync(id);
                return result;
            }
        }
        #endregion

        #region Update
        public async Task UpdateAsync(TEntity entity)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                await context.Set<TEntity>().UpdateAsync(entity);
            }
        }
        #endregion

        #region Delete
        public virtual async Task DeleteAsync(TEntity entity)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                await context.Set<TEntity>().DeleteAsync(entity);
            }
        }

        #endregion

        #region List
        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var result = await context.Set<TEntity>().SelectAsync();
                return result.ToList();
            }
        }
        #endregion
    }
}
