using BlockBase.BBLinq.Context;
using BlockBase.Dapps.NeverForgetBot.Data.Interfaces;
using System;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Dal.DaoGeneric
{
    public class BaseDao<TEntity> : IBaseDao<TEntity> where TEntity : class, IEntity
    {
        protected readonly BbContext BbContext;


        public BaseDao(BbContext bbContext)
        {
            BbContext = bbContext;
        }


        public virtual async Task<TEntity> GetAsync(Guid id)
        {
            return await BbContext.Set<TEntity, Guid>().SingleOrDefaultAsync(e => e.Id == id);
        }


    }
}
