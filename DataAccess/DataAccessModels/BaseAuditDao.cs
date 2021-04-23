using BlockBase.Dapps.NeverForget.Data.Context;
using BlockBase.Dapps.NeverForget.Data.Entities.Base;
using BlockBase.Dapps.NeverForget.Data.Interfaces;
using BlockBase.Dapps.NeverForget.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.DataAccess.DataAccessModels
{
    public class BaseAuditDao<TAuditEntity> : BaseDao<TAuditEntity>, IBaseAuditDao<TAuditEntity> where TAuditEntity : AuditEntity, IEntity
    {
        #region Read
        public override async Task<TAuditEntity> GetAsync(Guid id)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var result = await context.Set<TAuditEntity>().Where(e => e.Id == id && e.IsDeleted == false).SelectAsync();
                if (result == null)
                {
                    return null;
                }
                else
                {
                    return result.ToList().FirstOrDefault();
                }
            }
        }

        public async Task<TAuditEntity> GetDeletedAsync(Guid id)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var result = await context.Set<TAuditEntity>().Where(e => e.Id == id && e.IsDeleted == true).SelectAsync();
                if (result == null)
                {
                    return null;
                }
                else
                {
                    return result.ToList().FirstOrDefault();
                }
            }
        }
        #endregion

        #region Delete
        public override async Task DeleteAsync(TAuditEntity entity)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                entity.IsDeleted = true;
                await context.Set<TAuditEntity>().UpdateAsync(entity);
            }
        }
        #endregion

        #region List
        public override async Task<List<TAuditEntity>> GetAllAsync()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var result = await context.Set<TAuditEntity>().Where(e => e.IsDeleted == false).SelectAsync();
                if (result == null)
                {
                    return new List<TAuditEntity>();
                }
                else
                {
                    return result.ToList();
                }
            }
        }

        public async Task<List<TAuditEntity>> GetAllDeletedAsync()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var result = await context.Set<TAuditEntity>().Where(e => e.IsDeleted == true).SelectAsync();
                if (result == null)
                {
                    return new List<TAuditEntity>();
                }
                else
                {
                    return result.ToList();
                }
            }
        }
        #endregion
    }
}
