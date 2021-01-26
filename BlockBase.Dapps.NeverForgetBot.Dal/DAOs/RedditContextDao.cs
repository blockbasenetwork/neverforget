using BlockBase.BBLinq.Results;
using BlockBase.Dapps.NeverForgetBot.Dal.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Dal.Interfaces.Base;
using BlockBase.Dapps.NeverForgetBot.Data.Context;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Dal.DAOs
{
    public class RedditContextDao : IRedditContextDao
    {
        protected readonly NeverForgetBotDbContext BbContext;

        public RedditContextDao(NeverForgetBotDbContext bbContext)
        {
            BbContext = bbContext;
        }

        public async Task DeleteAsync(RedditContext entity)
        {
            using (BbContext)
            {
                await BbContext.RedditContext.Delete(entity);
                
            }
        }

        public Task<List<QueryResult>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<RedditContext> GetAsync(Guid id)
        {
            using (BbContext)
            {
                var result = await BbContext.RedditContext.Get(id);
                return result.Result;
            }
        }

        //public async Task<List<RedditContext>> GetAllAsync()
        //{
        //    using (BbContext)
        //    {
        //        var result = await BbContext.RedditContext.List();
        //        return result.Result;
        //    }
        //}

        public async Task InsertAsync(RedditContext entity)
        {
            using (BbContext)
            {
                await BbContext.RedditContext.Insert(entity);
            }
        }

        public async Task UpdateAsync(RedditContext entity)
        {
            using (BbContext)
            {
                await BbContext.RedditContext.Update(entity);
            }
        }
    }
}
