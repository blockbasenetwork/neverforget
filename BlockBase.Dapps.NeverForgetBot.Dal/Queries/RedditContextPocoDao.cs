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
                #region Obsolete
                //var retrievedContext = await _context.RedditContext.Where(rCtx => rCtx.Id == contextId && rCtx.IsDeleted == false)
                //                                                    .List(rCtx => new RedditContext()
                //                                                    {
                //                                                        Id = rCtx.Id,
                //                                                        RequestTypeId = rCtx.RequestTypeId
                //                                                    });


                //var retrievedComment = await _context.RedditContext.Join<RedditComment>()
                //                                                    .Where((rCtx, rCom) => rCtx.Id == contextId && rCtx.IsDeleted == false)
                //                                                    .List((rCtx, rCom) => new RedditComment()
                //                                                    {
                //                                                        Id = rCom.Id,
                //                                                        CommentId = rCom.CommentId,
                //                                                        ParentId = rCom.ParentId,
                //                                                        ParentSubmissionId = rCom.ParentSubmissionId,
                //                                                        Content = rCom.Content,
                //                                                        CommentDate = rCom.CommentDate,
                //                                                        Author = rCom.Author,
                //                                                        SubReddit = rCom.SubReddit,
                //                                                        Link = rCom.Link,
                //                                                        RedditContextId = rCom.RedditContextId
                //                                                    });
                //
                //var retrievedSubmission = await _context.RedditContext.Join<RedditSubmission>()
                //                                                      .Where((rCtx, rSub) => rCtx.Id == contextId && rCtx.IsDeleted == false)
                //                                                      .List((rCtx, rSub) => new RedditSubmission()
                //                                                      {
                //                                                          Id = rSub.Id,
                //                                                          SubmissionId = rSub.SubmissionId,
                //                                                          Content = rSub.Content,
                //                                                          SubmissionDate = rSub.SubmissionDate,
                //                                                          Author = rSub.Author,
                //                                                          SubReddit = rSub.SubReddit,
                //                                                          Link = rSub.Link,
                //                                                          MediaLink = rSub.MediaLink,
                //                                                          Title = rSub.Title,
                //                                                          RedditContextId = rSub.RedditContextId
                //                                                      });

                //result.Context = retrievedContext.Result.GetEnumerator().Current;
                ////result.Comments = retrievedComment.Result.GetEnumerator().Current;
                //result.Submission = retrievedSubmission.Result.GetEnumerator().Current;

                //foreach (var comment in retrievedComment.Result)
                //{
                //    if (retrievedComment.Result == null)
                //    {
                //        return new RedditContextPoco();
                //    }
                //    result.Comments.Add(comment);
                //}

                //return result;
                #endregion
                var retrievedContext = await _context.RedditContext.Where((rCtx) => (rCtx.Id == contextId) && (rCtx.IsDeleted == false)).List();

                if (retrievedContext.Result.Count() != 0)
                {
                    var retrievedComments = await _context.RedditComment.Where((rCom) => (rCom.RedditContextId == contextId)).List();
                    var retrievedSubmission = await _context.RedditSubmission.Where((rSub) => (rSub.RedditContextId == contextId)).List();
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

        //public async Task<List<RedditContextPoco>> GetAllRedditContexts()
        //{
        //    List<RedditContextPoco> result = new List<RedditContextPoco>();
        //    using (var _context = new NeverForgetBotDbContext())
        //    {
        //        var retrievedContextIds = await _context.RedditContext.Where(ctx => ctx.IsDeleted == false).List();

        //        foreach (var context in retrievedContextIds.Result)
        //        {
        //            result.Add(GetRedditContextById(context.Id).Result);
        //        }
        //    }
        //    return result;
        //}

        public async Task<GeneralContextPoco> GetRecentRedditContext(RedditContext context)
        {
            GeneralContextPoco result = new GeneralContextPoco();
            using (var _context = new NeverForgetBotDbContext())
            {
                var retrievedSubmission = await _context.RedditSubmission.Where((rSub) => (rSub.RedditContextId == context.Id)).List();

                if (retrievedSubmission.Result.Count() != 0)
                {
                    result.Author = retrievedSubmission.Result.First().Author;
                    result.Content = retrievedSubmission.Result.First().Content;
                    result.Date = retrievedSubmission.Result.First().SubmissionDate;
                    result.SourceType = SourceTypeEnum.Reddit;

                    return result;
                }
                else
                {
                    var retrievedComments = await _context.RedditComment.Where((rCom) => (rCom.RedditContextId == context.Id)).List();
                    var comment = retrievedComments.Result.OrderBy(c => c.CommentDate).First();

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
                var retrievedContextIds = await _context.RedditContext.Where(ctx => ctx.IsDeleted == false).List((ctx) => new RedditContext() { Id = ctx.Id });

                foreach (var context in retrievedContextIds.Result)
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
                var retrievedContextIds = await _context.RedditContext.Where(ctx => ctx.IsDeleted == false).List();

                //var contextIdsList = retrievedContextIds.Result.ToList().OrderByDescending(c => c.CreatedAt).Take(10);

                foreach (var context in retrievedContextIds.Result.ToList().OrderByDescending(c => c.CreatedAt).Take(10))
                {
                    result.Add(GetRecentRedditContext(context).Result);
                }
            }

            return result;
        }
    }
}
