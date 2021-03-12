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

        public async Task<GeneralContextPoco> GetRecentTwitterContextById(TwitterContext context)
        {
            GeneralContextPoco result = new GeneralContextPoco();
            using (var _context = new NeverForgetBotDbContext())
            {
                //var retrievedSubmission = await _context.TwitterSubmission.Where((tSub) => (tSub.TwitterContextId == context.Id)).List(sub => new GeneralContextPoco
                //{
                //    Author = sub.Author,
                //    Content = sub.Content,
                //    Date = sub.SubmissionDate,
                //    SourceType = SourceTypeEnum.Twitter
                //});

                var retrievedSubmission = await _context.TwitterSubmission.Where((tSub) => (tSub.TwitterContextId == context.Id)).List();
                if (retrievedSubmission.Result.Count() != 0)
                {
                    result.Author = retrievedSubmission.Result.First().Author;
                    result.Content = retrievedSubmission.Result.First().Content;
                    result.Date = retrievedSubmission.Result.First().SubmissionDate;
                    result.SourceType = SourceTypeEnum.Twitter;

                    return result;
                }
                else
                {
                    //var retrievedComments = await _context.TwitterComment.Where((tCom) => (tCom.TwitterContextId == context.Id)).List(sub => new GeneralContextPoco
                    //{
                    //    Author = sub.Author,
                    //    Content = sub.Content,
                    //    Date = sub.CommentDate,
                    //    SourceType = SourceTypeEnum.Twitter
                    //});

                    var retrievedComments = await _context.TwitterComment.Where((tCom) => (tCom.TwitterContextId == context.Id)).List();
                    var comment = retrievedComments.Result.OrderBy(c => c.CommentDate).First();

                    result.Author = comment.Author;
                    result.Content = comment.Content;
                    result.Date = comment.CommentDate;
                    result.SourceType = SourceTypeEnum.Twitter;

                    return result;
                    //return (GeneralContextPoco)retrievedComments.Result.OrderBy(c => c.Date).ToList().Take(1);                        
                }
            }
        }

        public async Task<List<TwitterContextPoco>> GetAllTwitterContexts()
        {
            List<TwitterContextPoco> result = new List<TwitterContextPoco>();
            using (var _context = new NeverForgetBotDbContext())
            {
                var retrievedContextIds = await _context.TwitterContext.Where(ctx => ctx.IsDeleted == false).List((ctx) => new TwitterContext(){Id = ctx.Id});
;
                foreach (var context in retrievedContextIds.Result.OrderByDescending(c => c.CreatedAt).ToList())
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
                //var retrievedContextIds = await _context.TwitterContext.Where(ctx => ctx.IsDeleted == false).List((ctx) => new TwitterContext()
                //{
                //    Id = ctx.Id,
                //    CreatedAt = ctx.CreatedAt,
                //    RequestTypeId = ctx.RequestTypeId
                //});

                var retrievedContextIds = await _context.TwitterContext.Where(ctx => ctx.IsDeleted == false).List();

                //retrievedContextIds.Result.ToList().OrderByDescending(c => c.CreatedAt).Take(10);

                foreach (var context in retrievedContextIds.Result.ToList().OrderByDescending(c => c.CreatedAt).Take(10))
                {
                    result.Add(GetRecentTwitterContextById(context).Result);
                }
            }           

            return result;
        }
    }
}
