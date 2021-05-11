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
    public class RedditContextPocoDataAccessObject : IRedditContextPocoDataAccessObject
    {
        public async Task<List<RedditContextPoco>> GetRedditContextById(Guid contextId)
        {
            using (var _context = new NeverForgetBotDbContext())
            {
                List<RedditContextPoco> context = new List<RedditContextPoco>();

                var redditContext = await _context.RedditContext.Join<RedditComment>(BlockBaseJoinEnum.Left).Join<RedditSubmission>(BlockBaseJoinEnum.Left)
                    .Where((redditContext, redditComment, redditSubmission) => (!redditContext.IsDeleted) && (redditContext.Id == contextId))
                    .SelectAsync((redditContext, redditComment, redditSubmission) => new RedditContextPoco
                    {
                        ContextId = redditContext.Id,
                        ContextCreatedAt = redditContext.CreatedAt,
                        CommentContent = redditComment.Content,
                        CommentAuthor = redditComment.Author,
                        CommentLink = redditComment.Link,
                        CommentSubReddit = redditComment.SubReddit,
                        CommentDate = redditComment.CommentDate,
                        SubmissionContent = redditSubmission.Content,
                        SubmissionAuthor = redditSubmission.Author,
                        SubmissionLink = redditSubmission.Link,
                        SubmissionMediaLink = redditSubmission.MediaLink,
                        SubmissionSubReddit = redditSubmission.SubReddit,
                        SubmissionTitle = redditSubmission.Title,
                        SubmissionDate = redditSubmission.SubmissionDate,
                    });

                return redditContext.ToList();
            }
        }

        public async Task<List<RedditContextPoco>> GetAllRedditContexts()
        {
            using (var _context = new NeverForgetBotDbContext())
            {
                List<RedditContextPoco> contextList = new List<RedditContextPoco>();

                var redditContext = await _context.RedditContext.Join<RedditComment>(BlockBaseJoinEnum.Left).Join<RedditSubmission>(BlockBaseJoinEnum.Left)
                    .Where((redditContext, redditComment, redditSubmission) => (!redditContext.IsDeleted))
                    .SelectAsync((redditContext, redditComment, redditSubmission) => new RedditContextPoco
                    {
                        ContextId = redditContext.Id,
                        ContextCreatedAt = redditContext.CreatedAt,
                        CommentContent = redditComment.Content,
                        CommentAuthor = redditComment.Author,
                        CommentLink = redditComment.Link,
                        CommentSubReddit = redditComment.SubReddit,
                        CommentDate = redditComment.CommentDate,
                        SubmissionContent = redditSubmission.Content,
                        SubmissionAuthor = redditSubmission.Author,
                        SubmissionLink = redditSubmission.Link,
                        SubmissionMediaLink = redditSubmission.MediaLink,
                        SubmissionSubReddit = redditSubmission.SubReddit,
                        SubmissionTitle = redditSubmission.Title,
                        SubmissionDate = redditSubmission.SubmissionDate,
                    });

                return redditContext.ToList();
            }
        }

        public async Task<List<GeneralContextPoco>> GetRecentReddit()
        {
            using (var _context = new NeverForgetBotDbContext())
            {
                var recentPosts = await _context.RedditContext.Join<RedditSubmission>(BlockBaseJoinEnum.Left)
                    .Where((redditContext, redditSubmission) => (!redditContext.IsDeleted))
                    .SelectAsync((redditContext, redditSubmission) => new GeneralContextPoco
                    {
                        SourceType = SourceTypeEnum.Reddit,
                        Date = redditSubmission.SubmissionDate,
                        Author = redditSubmission.Author,
                        Content = redditSubmission.Content,
                        Title = redditSubmission.Title
                    });

                return recentPosts.OrderByDescending((reddit) => reddit.Date).Take(10).ToList();
            }
        }
    }
}