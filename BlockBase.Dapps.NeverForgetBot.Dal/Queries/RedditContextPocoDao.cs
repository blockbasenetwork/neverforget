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
                var retrievedContext = await _context.RedditContext.Join<RedditComment>().Join<RedditSubmission>()
                                                                    .Where((rCtx, rCom, rSub) => rCtx.Id == contextId && rCtx.IsDeleted == false)
                                                                    .List((rCtx, rCom, rSub) => new RedditJoinResult()
                                                                    {
                                                                        Context = new RedditContext()
                                                                        {
                                                                            Id = rCtx.Id,
                                                                            RequestTypeId = rCtx.RequestTypeId
                                                                        },
                                                                        Comment = new RedditComment()
                                                                        {
                                                                            Id = rCom.Id,
                                                                            CommentId = rCom.CommentId,
                                                                            ParentId = rCom.ParentId,
                                                                            ParentSubmissionId = rCom.ParentSubmissionId,
                                                                            Content = rCom.Content,
                                                                            CommentDate = rCom.CommentDate,
                                                                            Author = rCom.Author,
                                                                            SubReddit = rCom.SubReddit,
                                                                            Link = rCom.Link,
                                                                            RedditContextId = rCom.RedditContextId
                                                                        },
                                                                        Submission = new RedditSubmission()
                                                                        {
                                                                            Id = rSub.Id,
                                                                            SubmissionId = rSub.SubmissionId,
                                                                            Content = rSub.Content,
                                                                            SubmissionDate = rSub.SubmissionDate,
                                                                            Author = rSub.Author,
                                                                            SubReddit = rSub.SubReddit,
                                                                            Link = rSub.Link,
                                                                            MediaLink = rSub.MediaLink,
                                                                            RedditContextId = rSub.RedditContextId
                                                                        }
                                                                    });

                result.Context = retrievedContext.Result.GetEnumerator().Current.Context;
                result.Submission = retrievedContext.Result.GetEnumerator().Current.Submission;

                foreach (var context in retrievedContext.Result)
                {
                    result.Comments.Add(context.Comment);
                }

                return result;
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
