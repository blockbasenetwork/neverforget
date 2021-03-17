using BlockBase.Dapps.NeverForgetBot.Common.Enums;
using BlockBase.Dapps.NeverForgetBot.Data.Context;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Dal.Queries
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

        public async Task<GeneralContextPoco> GetRecentRedditContext(RedditContext context)
        {
            GeneralContextPoco result = new GeneralContextPoco();
            using (var _context = new NeverForgetBotDbContext())
            {
                var retrievedSubmission = await _context.RedditSubmission.Where((rSub) => (rSub.RedditContextId == context.Id)).SelectAsync();

                if (retrievedSubmission.Count() != 0)
                {
                    result.Author = retrievedSubmission.First().Author;
                    result.Content = retrievedSubmission.First().Content;
                    result.Date = retrievedSubmission.First().SubmissionDate;
                    result.Title = retrievedSubmission.First().Title;
                    result.SourceType = SourceTypeEnum.Reddit;

                    return result;
                }
                else
                {
                    var retrievedComments = await _context.RedditComment.Where((rCom) => (rCom.RedditContextId == context.Id)).SelectAsync();
                    var comment = retrievedComments.OrderBy(c => c.CommentDate).First();

                    result.Author = comment.Author;
                    result.Content = comment.Content;
                    result.Date = comment.CommentDate;
                    result.SourceType = SourceTypeEnum.Reddit;

                    return result;
                }
            }
        }

        public async Task<List<RedditContextPoco>> GetAllRedditContexts()
        {
            List<RedditContextPoco> result = new List<RedditContextPoco>();
            using (var _context = new NeverForgetBotDbContext())
            {
                var retrievedContextIds = await _context.RedditContext.Where(ctx => ctx.IsDeleted == false).SelectAsync((ctx) => new RedditContext() { Id = ctx.Id });

                foreach (var context in retrievedContextIds)
                {
                    result.Add(GetRedditContextById(context.Id).Result);
                }
            }
            return result;
        }

        public async Task<List<GeneralContextPoco>> GetRecentRedditContexts()
        {
            List<GeneralContextPoco> result = new List<GeneralContextPoco>();

            using (var _context = new NeverForgetBotDbContext())
            {
                var retrievedContextIds = await _context.RedditContext.Where(ctx => ctx.IsDeleted == false).SelectAsync();

                foreach (var context in retrievedContextIds.ToList().OrderByDescending(c => c.CreatedAt).Take(10))
                {
                    result.Add(GetRecentRedditContext(context).Result);
                }
            }

            return result;
        }
    }
}
