using BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Data.Context;
using BlockBase.Dapps.NeverForgetBot.Data.Entities.Base;
using BlockBase.Dapps.NeverForgetBot.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess
{
    public class BaseAuditDao<TAuditEntity, TKey> : BaseDao<TAuditEntity, TKey>, IBaseAuditDao<TAuditEntity, TKey> where TAuditEntity : AuditEntity, IEntity
    {
        #region Read
        public override async Task<TAuditEntity> GetAsync(Guid id)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var result = await context.Set<TAuditEntity, Guid>().Where(e => e.Id == id && e.IsDeleted == false).List();
                if (result.Result == null)
                {
                    return null;
                }
                else
                {
                    return result.Result.ToList().FirstOrDefault();
                }
            }
        }

        public async Task<TAuditEntity> GetDeletedAsync(Guid id)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var result = await context.Set<TAuditEntity, Guid>().Where(e => e.Id == id && e.IsDeleted == true).List();
                if (result.Result == null)
                {
                    return null;
                }
                else
                {
                    return result.Result.ToList().FirstOrDefault();
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
                await context.Set<TAuditEntity, Guid>().Update(entity);
            }
        }
        #endregion

        #region List
        public override async Task<List<TAuditEntity>> GetAllAsync()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var result = await context.Set<TAuditEntity, Guid>().Where(e => e.IsDeleted == false).List();
                if (result.Result == null)
                {
                    return new List<TAuditEntity>();
                }
                else
                {
                    return result.Result.ToList();
                }
            }
        }

        public async Task<List<TAuditEntity>> GetAllDeletedAsync()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var result = await context.Set<TAuditEntity, Guid>().Where(e => e.IsDeleted == true).List();
                if (result.Result == null)
                {
                    return new List<TAuditEntity>();
                }
                else
                {
                    return result.Result.ToList();
                }
            }
        }
        #endregion
    }
}
