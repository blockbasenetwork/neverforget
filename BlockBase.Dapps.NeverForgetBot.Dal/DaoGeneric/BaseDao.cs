using BlockBase.BBLinq.Context;
using BlockBase.Dapps.NeverForgetBot.Data.Interfaces;
using System;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Dal.DaoGeneric
{
    public class BaseDao<TEntity, TKey> : IBaseDao<TEntity, TKey> where TEntity : class, IEntity
    {
        protected readonly BbContext BbContext;


        public BaseDao(BbContext bbContext)
        {
            BbContext = bbContext;
        }


        public virtual async Task<TEntity> GetAsync(TKey id)
        {
            return await BbContext.Set<TEntity, Guid>();
        }


    }
}
