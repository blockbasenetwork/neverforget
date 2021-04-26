using BlockBase.Dapps.NeverForget.Common.Enums;
using BlockBase.Dapps.NeverForget.Data.Context;
using BlockBase.Dapps.NeverForget.Data.Entities;
using BlockBase.Dapps.NeverForget.Data.Pocos;
using BlockBase.Dapps.NeverForget.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.DataAccess.DataAccessObjects
{
    public class RedditContextPocoDao : IRedditContextPocoDao
    {
        public async Task<RedditContextPoco> GetRedditContextById(Guid contextId)
        {
            RedditContextPoco result = new RedditContextPoco();
            using (var _context = new NeverForgetBotDbContext())
            {
                var retrievedContext = await _context.RedditContext.Where((rCtx) => (rCtx.Id == contextId) && (rCtx.IsDeleted == false)).SelectAsync();

                if (retrievedContext.Count() != 0)
                {
                    var retrievedComments = await _context.RedditComment.Where((rCom) => (rCom.RedditContextId == contextId)).SelectAsync();
                    var retrievedSubmission = await _context.RedditSubmission.Where((rSub) => (rSub.RedditContextId == contextId)).SelectAsync();
                    foreach (var item in retrievedContext)
                    {
                        result.Context = item;
                    }
                    foreach (var item in retrievedSubmission)
                    {
                        result.Submission = item;
                    }
                    foreach (var item in retrievedComments)
                    {
                        result.Comments.Add(item);
                    }
                }
                return result;
            }
        }

        public async Task<List<RedditContextPoco>> GetAllRedditContexts()
        {
            List<RedditContextPoco> result = new List<RedditContextPoco>();
            using (var _context = new NeverForgetBotDbContext())
            {
                var retrievedContextIds = await _context.RedditContext.Where(rCtx => rCtx.IsDeleted == false).SelectAsync((rCtx) => new RedditContext() { Id = rCtx.Id });

                foreach (var context in retrievedContextIds)
                {
                    result.Add(GetRedditContextById(context.Id).Result);
                }
            }
            return result;
        }

        public async Task<List<GeneralContextPoco>> GetRecentReddit()
        {
            List<GeneralContextPoco> result = new List<GeneralContextPoco>();

            using (var _context = new NeverForgetBotDbContext())
            {
                var retrievedSubmissions = await _context.RedditSubmission.Where((tSub) => !tSub.IsDeleted).SelectAsync((tSub) => new GeneralContextPoco
                {
                    Author = tSub.Author,
                    Content = tSub.Content,
                    Date = tSub.SubmissionDate,
                    SourceType = SourceTypeEnum.Reddit,
                    Title = tSub.Title
                });

                foreach (var submission in retrievedSubmissions.ToList().OrderByDescending(ctx => ctx.Date).Take(10))
                {
                    result.Add(submission);
                }
            }
            return result;
        }
    }
}
