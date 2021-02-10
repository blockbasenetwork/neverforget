using BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Data.Context;
using BlockBase.Dapps.NeverForgetBot.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess
{
    public abstract class BaseDao<TEntity, TKey> : IBaseDao<TEntity, TKey> where TEntity : class, IEntity
    {
        #region Create
        public async Task InsertAsync(TEntity entity)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                await context.Set<TEntity, TKey>().Insert(entity);
            }
        }
        #endregion

        #region Read
        public virtual async Task<TEntity> GetAsync(Guid id)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var result = await context.Set<TEntity, Guid>().Get(id);
                return result.Result;
            }
        }
        #endregion

        #region Update
        public async Task UpdateAsync(TEntity entity)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                await context.Set<TEntity, TKey>().Update(entity);
            }
        }
        #endregion

        #region Delete
        public virtual async Task DeleteAsync(TEntity entity)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                await context.Set<TEntity, TKey>().Delete(entity);
            }
        }

        #endregion

        #region List
        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var result = await context.Set<TEntity, TKey>().List();
                return result.Result.ToList();
            }
        }
        #endregion
    }
}
