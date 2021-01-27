using BlockBase.Dapps.NeverForgetBot.Dal.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Data.Context;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Dal.DAOs
{
    public class RedditContextDao : IRedditContextDao
    {
        //protected readonly NeverForgetBotDbContext BbContext;

        //public RedditContextDao(NeverForgetBotDbContext bbContext)
        //{
        //    BbContext = bbContext;
        //}


        public async Task DeleteAsync(RedditContext entity)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                await context.RedditContext.Delete(entity);
            }
        }

        public async Task<RedditContext> GetAsync(Guid id)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var result = await context.RedditContext.Get(id);
                return result.Result;
            }
        }

        public async Task<List<RedditContext>> GetAllAsync()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var result = await context.RedditContext.List();
                return (List<RedditContext>)result.Result;
            }
        }

        public async Task InsertAsync(RedditContext entity)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                await context.RedditContext.Insert(entity);
            }
        }

        public async Task UpdateAsync(RedditContext entity)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                await context.RedditContext.Update(entity);
            }
        }
    }
}
