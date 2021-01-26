using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Dal
{
    public class BaseDalObject
    {
        //public async Task<TEntity> AddAsync(TEntity entity)
        //{
        //    EntityEntry<TEntity> result = await DbContext.AddAsync(entity);
        //    await DbContext.SaveChangesAsync();
        //    return result.Entity;
        //}

        //public virtual async Task<TEntity> GetAsync(Guid id)
        //{
        //    return await DbContext.Set<TEntity>().SingleOrDefaultAsync(e => e.Id == id);
        //}

        //public async Task<TEntity> UpdateAsync(TEntity entity)
        //{
        //    EntityEntry<TEntity> result = DbContext.Update(entity);
        //    await DbContext.SaveChangesAsync();
        //    return result.Entity;
        //}

        //public virtual async Task<List<TEntity>> GetAllAsync()
        //{
        //    return await DbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        //}

        //public virtual async Task DeleteAsync(TEntity entity)
        //{
        //    DbContext.Remove(entity);
        //    await DbContext.SaveChangesAsync();
        //}
    }
}
