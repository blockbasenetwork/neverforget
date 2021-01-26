using BlockBase.BBLinq.Results;
using BlockBase.Dapps.NeverForgetBot.Dal.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Data.Context;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Dal.DAOs
{
    public class TwitterContextDao : ITwitterContextDao
    {
        protected readonly NeverForgetBotDbContext BbContext;

        public TwitterContextDao(NeverForgetBotDbContext bbContext)
        {
            BbContext = bbContext;
        }

        public void DeleteAsync(TwitterContext entity)
        {
            using (BbContext)
            {
                await BbContext.TwitterContext.Delete(entity);
            }
        }

        public async Task<TwitterContext> GetAsync(Guid id)
        {
            using (BbContext)
            {
                var result = await BbContext.TwitterContext.Get(id);
                return result.Result;
            }
        }

        public async Task<List<IEnumerable<TwitterContext>>> GetAllAsync()
        {
            using (BbContext)
            {
                var result = await BbContext.TwitterContext.List();
                return result.Result;
            }
        }

        public void InsertAsync(TwitterContext entity)
        {
            using (BbContext)
            {
                BbContext.TwitterContext.Insert(entity);
            }
        }

        public void UpdateAsync(TwitterContext entity)
        {
            using (BbContext)
            {
                BbContext.TwitterContext.Update(entity);
            }
        }
    }
}
