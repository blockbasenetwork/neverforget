using BlockBase.BBLinq.Enumerables;
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
    public class TwitterContextPocoDataAccessObject : ITwitterContextPocoDataAccessObject
    {
        public async Task<TwitterContextPoco> GetTwitterContextById(Guid contextId)
        {
            //using (var _context = new NeverForgetBotDbContext())
            //{
            //    List<TwitterContextPoco> context = new List<TwitterContextPoco>();

            //    var twitterContext = await _context.TwitterContext.Join<TwitterComment>(BlockBaseJoinEnum.Left).Join<TwitterSubmission>(BlockBaseJoinEnum.Left)
            //        .Where((tweetContext, tweetComment, tweetSubmission) => (!tweetContext.IsDeleted) && (tweetContext.Id == contextId))
            //        .SelectAsync((tweetContext, tweetComment, tweetSubmission) => new TwitterContextPoco
            //        {
            //            ContextId = tweetContext.Id,
            //            ContextCreatedAt = tweetContext.CreatedAt,
            //            CommentContent = tweetComment.Content,
            //            CommentAuthor = tweetComment.Author,
            //            CommentLink = tweetComment.Link,
            //            CommentMediaLink = tweetComment.MediaLink,
            //            CommentDate = tweetComment.CommentDate,
            //            SubmissionContent = tweetSubmission.Content,
            //            SubmissionAuthor = tweetSubmission.Author,
            //            SubmissionLink = tweetSubmission.Link,
            //            SubmissionMediaLink = tweetSubmission.MediaLink,
            //            SubmissionDate = tweetSubmission.SubmissionDate,
            //            ParentCommentContent = tweetSubmission.Content,
            //            ParentCommentAuthor = tweetSubmission.Author,
            //            ParentCommentLink = tweetSubmission.Link,
            //            ParentCommentMediaLink = tweetSubmission.MediaLink,
            //            ParentCommentDate = tweetSubmission.SubmissionDate
            //        });

            //    var ids = twitterContext.GroupBy(c => c.ContextId);

            //    foreach (var keys in ids)
            //    {
            //        if (keys.Count() == 1)
            //        {
            //            context.Add(new TwitterContextPoco
            //            {
            //                ContextId = keys.ElementAt(0).ContextId,
            //                ContextCreatedAt = keys.ElementAt(0).ContextCreatedAt,
            //                SubmissionContent = keys.ElementAt(0).SubmissionContent,
            //                SubmissionAuthor = keys.ElementAt(0).SubmissionAuthor,
            //                SubmissionLink = keys.ElementAt(0).SubmissionLink,
            //                SubmissionMediaLink = keys.ElementAt(0).SubmissionMediaLink,
            //                SubmissionDate = keys.ElementAt(0).SubmissionDate,
            //            });
            //        }
            //        else
            //        {
            //            var ctx = keys.OrderBy(i => i.CommentDate).ToList();

            //            context.Add(new TwitterContextPoco
            //            {
            //                ContextId = ctx[0].ContextId,
            //                ContextCreatedAt = ctx[0].ContextCreatedAt,
            //                CommentContent = ctx[0].CommentContent,
            //                CommentAuthor = ctx[0].CommentAuthor,
            //                CommentLink = ctx[0].CommentLink,
            //                CommentMediaLink = ctx[0].CommentMediaLink,
            //                CommentDate = ctx[0].CommentDate,
            //            });
            //        }
            //    }

            //    return context;
            //}
            return new TwitterContextPoco();
        }

        public async Task<List<TwitterContextPoco>> GetAllTwitterContexts()
        {
            using (var _context = new NeverForgetBotDbContext())
            {
                List<TwitterContextPoco> contextList = new List<TwitterContextPoco>();

                var twitterContext = await _context.TwitterContext.Join<TwitterComment>(BlockBaseJoinEnum.Left).Join<TwitterSubmission>(BlockBaseJoinEnum.Left)
                    .Where((tweetContext, tweetComment, tweetSubmission) => (!tweetContext.IsDeleted))
                    .SelectAsync((tweetContext, tweetComment, tweetSubmission) => new TwitterContextPoco
                    {
                        ContextId = tweetContext.Id,
                        ContextCreatedAt = tweetContext.CreatedAt,
                        CommentContent = tweetComment.Content,
                        CommentAuthor = tweetComment.Author,
                        CommentLink = tweetComment.Link,
                        CommentMediaLink = tweetComment.MediaLink,
                        CommentDate = tweetComment.CommentDate,
                        SubmissionContent = tweetSubmission.Content,
                        SubmissionAuthor = tweetSubmission.Author,
                        SubmissionLink = tweetSubmission.Link,
                        SubmissionMediaLink = tweetSubmission.MediaLink,
                        SubmissionDate = tweetSubmission.SubmissionDate,
                    });

                var ids = twitterContext.GroupBy(c => c.ContextId);

                foreach (var keys in ids)
                {
                    if(keys.Count() == 1)
                    {
                        contextList.Add(new TwitterContextPoco
                        {
                            ContextId = keys.ElementAt(0).ContextId,
                            ContextCreatedAt = keys.ElementAt(0).ContextCreatedAt,
                            SubmissionContent = keys.ElementAt(0).SubmissionContent,
                            SubmissionAuthor = keys.ElementAt(0).SubmissionAuthor,
                            SubmissionLink = keys.ElementAt(0).SubmissionLink,
                            SubmissionMediaLink = keys.ElementAt(0).SubmissionMediaLink,
                            SubmissionDate = keys.ElementAt(0).SubmissionDate,
                        });
                    }
                    else
                    {
                        var ctx = keys.OrderBy(i => i.CommentDate).ToList();

                        contextList.Add(new TwitterContextPoco
                        {
                            ContextId = ctx[0].ContextId,
                            ContextCreatedAt = ctx[0].ContextCreatedAt,
                            CommentContent = ctx[0].CommentContent,
                            CommentAuthor = ctx[0].CommentAuthor,
                            CommentLink = ctx[0].CommentLink,
                            CommentMediaLink = ctx[0].CommentMediaLink,
                            CommentDate = ctx[0].CommentDate,
                        });
                    }
                }

                return contextList;
            }
        }

        public async Task<List<GeneralContextPoco>> GetRecentTwitter()
        {
            using (var _context = new NeverForgetBotDbContext())
            {
                var recentTweets = await _context.TwitterContext.Join<TwitterSubmission>(BlockBaseJoinEnum.Left)
                    .Where((tweetContext, tweetSubmission) => (!tweetContext.IsDeleted))
                    .SelectAsync((tweetContext, tweetSubmission) => new GeneralContextPoco 
                    {
                        SourceType = SourceTypeEnum.Twitter,
                        Date = tweetSubmission.SubmissionDate,
                        Author = tweetSubmission.Author,
                        Content = tweetSubmission.Content
                    });

                return recentTweets.OrderByDescending((tweet) => tweet.Date).Take(10).ToList();
            }
        }
    }
}