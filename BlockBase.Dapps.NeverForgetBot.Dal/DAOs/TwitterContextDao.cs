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


        public async Task DeleteAsync(TwitterContext entity)
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

        public async Task<List<TwitterContext>> GetAllAsync()
        {
            using (BbContext)
            {
                var result = await BbContext.TwitterContext.List();
                return (List<TwitterContext>)result.Result;
            }
        }

        public async Task InsertAsync(TwitterContext entity)
        {
            using (BbContext)
            {
                await BbContext.TwitterContext.Insert(entity);
            }
        }

        public async Task UpdateAsync(TwitterContext entity)
        {
            using (BbContext)
            {
                await BbContext.TwitterContext.Update(entity);
            }
        }
    }
}
