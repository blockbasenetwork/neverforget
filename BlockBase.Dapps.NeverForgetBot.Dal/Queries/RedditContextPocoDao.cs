using BlockBase.Dapps.NeverForgetBot.Data.Context;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Dal.Queries
{

    public class A
    {
        public Guid Id { get; set; }
    }
    public class RedditContextPocoDao : IRedditContextPocoDao
    {
        public async Task<RedditContextPoco> GetRedditContextById(Guid contextId)
        {
            
            RedditContextPoco result = new RedditContextPoco();
            using (var _context = new NeverForgetBotDbContext())
            {
                var retrievedContext = await _context.RedditContext.Join<RedditComment>().Join<RedditSubmission>()
                                                                    .Where((rCtx, rCom, rSub) => rCtx.Id == contextId && rCtx.IsDeleted == false)
                                                                    .List((rCtx, rCom, rSub) => new A(){Id =rCtx.Id});

                //result.Context = retrievedContext.Result.GetEnumerator().Current.Context;
                //result.Submission = retrievedContext.Result.GetEnumerator().Current.Submission;

                //foreach (var context in retrievedContext.Result)
                //{
                //    result.Comments.Add(context.Comment);
                //}

                return null;
            }
        }

        public async Task<List<RedditContextPoco>> GetAllRedditContexts()
        {
            List<RedditContextPoco> result = new List<RedditContextPoco>();
            using (var _context = new NeverForgetBotDbContext())
            {
                var retrievedContextIds = await _context.RedditContext.Where(ctx => ctx.IsDeleted == false).List();

                foreach (var context in retrievedContextIds.Result)
                {
                    result.Add(GetRedditContextById(context.Id).Result);
                }
            }
            return result;
        }

        public async Task<List<RedditContextPoco>> GetRecentRedditContexts()
        {
            List<RedditContextPoco> result = new List<RedditContextPoco>();

            var retrievedContextIds = await GetAllRedditContexts();

            List<RedditContextPoco> orderedList = retrievedContextIds.OrderByDescending(c => c.Context.CreatedAt).ToList();

            result.AddRange(orderedList.Take(10));

            return result;
        }
    }
}
