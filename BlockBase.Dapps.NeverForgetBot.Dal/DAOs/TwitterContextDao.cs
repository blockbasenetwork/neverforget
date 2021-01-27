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
        //protected readonly NeverForgetBotDbContext BbContext;

        //public TwitterContextDao(NeverForgetBotDbContext bbContext)
        //{
        //    BbContext = bbContext;
        //}


        public async Task DeleteAsync(TwitterContext entity)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                await context.TwitterContext.Delete(entity);
            }
        }

        public async Task<TwitterContext> GetAsync(Guid id)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var result = await context.TwitterContext.Get(id);
                return result.Result;
            }
        }

        public async Task<List<TwitterContext>> GetAllAsync()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var result = await context.TwitterContext.List();
                return (List<TwitterContext>)result.Result;
            }
        }

        public async Task InsertAsync(TwitterContext entity)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                await context.TwitterContext.Insert(entity);
            }
        }

        public async Task UpdateAsync(TwitterContext entity)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                await context.TwitterContext.Update(entity);
            }
        }
    }
}
