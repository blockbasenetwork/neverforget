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
            using (var _context = new NeverForgetBotDbContext())
            {
                //var twitterContext = await _context.TwitterContext.Join<TwitterComment>(BlockBaseJoinEnum.Left).Join<TwitterSubmission>(BlockBaseJoinEnum.Left)
                //    .Where((tweetContext, tweetComment, tweetSubmission) => (tweetContext.Id == tweetComment.TwitterContextId) &&
                //                                                            (tweetContext.Id == tweetSubmission.TwitterContextId) &&
                //                                                            (tweetContext.Id == contextId) && (!tweetContext.IsDeleted))
                //    .SelectAsync();

                //var twitterContext = await _context.TwitterContext.Join<TwitterComment>(BlockBaseJoinEnum.Left).Join<TwitterSubmission>(BlockBaseJoinEnum.Left)
                //    .Where((tweetContext, tweetComment, tweetSubmission) => (tweetContext.Id == tweetComment.TwitterContextId) && (tweetContext.Id == tweetSubmission.TwitterContextId))
                //    .SelectAsync();

                var twitterContext = await _context.TwitterContext.Join<TwitterComment>(BlockBaseJoinEnum.Left).Join<TwitterSubmission>(BlockBaseJoinEnum.Left)
                    .SelectAsync((tweetContext, tweetComment, tweetSubmission) => new TwitterContextPoco 
                    {
                        ContextId = tweetContext.Id,
                        RequestTypeId = tweetContext.RequestTypeId,
                        ContextCreatedAt = tweetContext.CreatedAt,
                        CommentId = tweetComment.Id,
                        CommentCommentId = tweetComment.CommentId,
                        CommentReplyToId = tweetComment.ReplyToId,
                        CommentContent = tweetComment.Content,
                        CommentAuthor = tweetComment.Author,
                        CommentLink = tweetComment.Link,
                        CommentMediaLink = tweetComment.MediaLink,
                        CommentDate = tweetComment.CommentDate,
                        SubmissionId = tweetSubmission.Id,
                        SubmissionSubmissionId = tweetSubmission.SubmissionId,
                        SubmissionContent = tweetSubmission.Content,
                        SubmissionAuthor = tweetSubmission.Author,
                        SubmissionLink = tweetSubmission.Link,
                        SubmissionMediaLink = tweetSubmission.MediaLink,
                        SubmissionDate = tweetSubmission.SubmissionDate,
                    });

                TwitterContextPoco result = new TwitterContextPoco();
                return result;
            }
        }


//        using (var _context = new NeverForgetBotDbContext())
//            {
//                var recentTweets = await _context.TwitterContext.Join<TwitterSubmission>(BlockBaseJoinEnum.Left)
//                    .Where((tweetContext, tweetSubmission) => (tweetContext.Id == tweetSubmission.TwitterContextId) && (!tweetContext.IsDeleted))
//                    .SelectAsync((tweetContext, tweetSubmission) => new GeneralContextPoco
//                    {
//                        SourceType = SourceTypeEnum.Twitter,
//                        Date = tweetSubmission.SubmissionDate,
//                        Author = tweetSubmission.Author,
//                        Content = tweetSubmission.Content
//                    });

//                return recentTweets.OrderByDescending((tweet) => tweet.Date).Take(10).ToList();
//}


        public async Task<List<TwitterContextPoco>> GetAllTwitterContexts()
        {
            List<TwitterContextPoco> result = new List<TwitterContextPoco>();
            using (var _context = new NeverForgetBotDbContext())
            {
                var retrievedContextIds = await _context.TwitterContext.Where(tCtx => tCtx.IsDeleted == false).SelectAsync((tCtx) => new TwitterContext() { Id = tCtx.Id });

                foreach (var context in retrievedContextIds)
                {
                    result.Add(GetTwitterContextById(context.Id).Result);
                }
            }
            return result;
        }

        public async Task<List<GeneralContextPoco>> GetRecentTwitter()
        {
            using (var _context = new NeverForgetBotDbContext())
            {
                var recentTweets = await _context.TwitterContext.Join<TwitterSubmission>(BlockBaseJoinEnum.Left)
                    .Where((tweetContext, tweetSubmission) => (tweetContext.Id == tweetSubmission.TwitterContextId) && (!tweetContext.IsDeleted))
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