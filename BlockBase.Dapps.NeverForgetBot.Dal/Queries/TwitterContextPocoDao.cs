    using BlockBase.Dapps.NeverForgetBot.Data.Context;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Dal.Queries
{
    public class TwitterContextPocoDao : ITwitterContextPocoDao
    {
        public async Task<TwitterContextPoco> GetTwitterContextById(Guid contextId)
        {

            TwitterContextPoco result = new TwitterContextPoco();
            using (var _context = new NeverForgetBotDbContext())
            {
                var retrievedContext = await _context.TwitterContext.Where((tCtx) => (tCtx.Id == contextId) && (tCtx.IsDeleted == false)).List();

                if (retrievedContext.Result.Count() != 0)
                {
                    var retrievedComments = await _context.TwitterComment.Where((tCom) => (tCom.TwitterContextId == contextId)).List();
                    var retrievedSubmission = await _context.TwitterSubmission.Where((tSub) => (tSub.TwitterContextId == contextId)).List();
                    foreach (var item in retrievedContext.Result)
                    {
                        result.Context = item;
                    }
                    foreach (var item in retrievedSubmission.Result)
                    {
                        result.Submission = item;
                    }
                    foreach (var item in retrievedComments.Result)
                    {
                        result.Comments.Add(item);
                    }
                }
                return result;
            }
        }

        public async Task<List<TwitterContextPoco>> GetAllTwitterContexts()
        {
            List<TwitterContextPoco> result = new List<TwitterContextPoco>();
            using (var _context = new NeverForgetBotDbContext())
            {
                var retrievedContextIds = await _context.TwitterContext.Where(ctx => ctx.IsDeleted == false).List();

                foreach (var context in retrievedContextIds.Result)
                {
                    result.Add(GetTwitterContextById(context.Id).Result);
                }
            }
            return result;
        }

        public async Task<List<TwitterContextPoco>> GetRecentTwitterContexts()
        {
            List<TwitterContextPoco> result = new List<TwitterContextPoco>();

            var retrievedContextIds = await GetAllTwitterContexts();

            List<TwitterContextPoco> orderedList = retrievedContextIds.OrderByDescending(c => c.Context.CreatedAt).ToList();

            result.AddRange(orderedList.Take(10));

            return result;
        }
    }
}
