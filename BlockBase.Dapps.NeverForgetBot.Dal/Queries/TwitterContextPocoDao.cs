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
    public class TwitterContextPocoDao : ITwitterContextPocoDao
    {
        public async Task<TwitterContextPoco> GetTwitterContextById(Guid contextId)
        {

            TwitterContextPoco result = new TwitterContextPoco();
            using (var _context = new NeverForgetBotDbContext())
            {
                var retrievedContext = await _context.TwitterContext.Where((tCtx) => (tCtx.Id == contextId) && (tCtx.IsDeleted == false)).SelectAsync();

                if (retrievedContext.Count() != 0)
                {
                    var retrievedComments = await _context.TwitterComment.Where((tCom) => (tCom.TwitterContextId == contextId)).SelectAsync();
                    var retrievedSubmission = await _context.TwitterSubmission.Where((tSub) => (tSub.TwitterContextId == contextId)).SelectAsync();
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

        public async Task<GeneralContextPoco> GetRecentTwitterContextById(TwitterContext context)
        {
            GeneralContextPoco result = new GeneralContextPoco();
            using (var _context = new NeverForgetBotDbContext())
            {
                var retrievedSubmission = await _context.TwitterSubmission.Where((tSub) => (tSub.TwitterContextId == context.Id)).SelectAsync();
                if (retrievedSubmission.Count() != 0)
                {
                    result.Author = retrievedSubmission.First().Author;
                    result.Content = retrievedSubmission.First().Content;
                    result.Date = retrievedSubmission.First().SubmissionDate;
                    result.SourceType = SourceTypeEnum.Twitter;

                    return result;
                }
                else
                {
                    var retrievedComments = await _context.TwitterComment.Where((tCom) => (tCom.TwitterContextId == context.Id)).SelectAsync();
                    var comment = retrievedComments.OrderBy(c => c.CommentDate).First();

                    result.Author = comment.Author;
                    result.Content = comment.Content;
                    result.Date = comment.CommentDate;
                    result.SourceType = SourceTypeEnum.Twitter;

                    return result;
                }
            }
        }

        public async Task<List<TwitterContextPoco>> GetAllTwitterContexts()
        {
            List<TwitterContextPoco> result = new List<TwitterContextPoco>();
            using (var _context = new NeverForgetBotDbContext())
            {
                var retrievedContextIds = await _context.TwitterContext.Where(tCtx => tCtx.IsDeleted == false).SelectAsync((tCtx) => new TwitterContext() { Id = tCtx.Id });
                ;
                foreach (var context in retrievedContextIds.OrderByDescending(c => c.CreatedAt).ToList())
                {
                    result.Add(GetTwitterContextById(context.Id).Result);
                }
            }
            return result;
        }

        public async Task<List<GeneralContextPoco>> GetRecentTwitterContexts()
        {
            List<GeneralContextPoco> result = new List<GeneralContextPoco>();

            using (var _context = new NeverForgetBotDbContext())
            {
                var retrievedContextIds = await _context.TwitterContext.Where((tCtx) => !tCtx.IsDeleted).SelectAsync();

                foreach (var context in retrievedContextIds.ToList().OrderByDescending(ctx => ctx.CreatedAt).Take(10))
                {
                    result.Add(GetRecentTwitterContextById(context).Result);
                }
            }

            return result;
        }
    }
}
