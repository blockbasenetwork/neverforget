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
    public class GeneralContextPocoDao
    {
        public async Task<GeneralContextPoco> GetRedditGeneralByContextId(Guid contextId)
        {
            GeneralContextPoco redditContext = new GeneralContextPoco();
            string parentId = string.Empty;

            using (var _context = new NeverForgetBotDbContext())
            {
                var redditComments = await _context.RedditComment.Where(rc => (rc.RedditContextId == contextId) && (rc.IsDeleted == false)).List();

                foreach (var comment in redditComments.Result)
                {
                    parentId = comment.ParentId;

                    var mostRecent = redditComments.Result.OrderByDescending(c => c.CommentDate).FirstOrDefault();
                    var listOrdered = redditComments.Result.OrderByDescending(c => c.CommentDate).ToList();
                    listOrdered.Remove(mostRecent);

                    if (listOrdered.Count > 0)
                    {
                        var parentComment = listOrdered.FirstOrDefault();

                        return RedditGeneralPocoFromComment(redditContext, parentComment);
                    }
                }

                var redditSubmission = await _context.RedditSubmission.Where(rs => (rs.RedditContextId == contextId) && (rs.SubmissionId == parentId) && (rs.IsDeleted == false)).List();

                return RedditGeneralPocoFromSubmission(redditContext, redditSubmission.Result.FirstOrDefault());
            }
        }

        public async Task<List<GeneralContextPoco>> GetRedditGeneral()
        {
            List<GeneralContextPoco> redditContexts = new List<GeneralContextPoco>();

            using (var _context = new NeverForgetBotDbContext())
            {
                var redditContextIds = await _context.RedditContext.Where(ctx => (ctx.IsDeleted == false)).List();

                foreach (var ctx in redditContextIds.Result)
                {
                    redditContexts.Add(GetRedditGeneralByContextId(ctx.Id).Result);
                }
            }
            return redditContexts;
        }

        public async Task<GeneralContextPoco> GetTwitterGeneralByContextId(Guid contextId)
        {
            GeneralContextPoco twitterContext = new GeneralContextPoco();
            string parentId = string.Empty;

            using (var _context = new NeverForgetBotDbContext())
            {
                var twitterComments = await _context.TwitterComment.Where(rc => (rc.TwitterContextId == contextId) && (rc.IsDeleted == false)).List();

                foreach (var comment in twitterComments.Result)
                {
                    parentId = comment.ReplyToId;

                    var mostRecent = twitterComments.Result.OrderByDescending(c => c.CommentDate).FirstOrDefault();
                    var listOrdered = twitterComments.Result.OrderByDescending(c => c.CommentDate).ToList();
                    listOrdered.Remove(mostRecent);

                    if (listOrdered.Count > 0)
                    {
                        var parentComment = listOrdered.FirstOrDefault();

                        return TwitterGeneralPocoFromComment(twitterContext, parentComment);
                    }
                }

                var twitterSubmission = await _context.TwitterSubmission.Where(rs => (rs.TwitterContextId == contextId) && (rs.SubmissionId == parentId) && (rs.IsDeleted == false)).List();

                twitterContext = TwitterGeneralPocoFromSubmission(twitterContext, twitterSubmission.Result.FirstOrDefault());

                return twitterContext;
            }
        }

        public async Task<List<GeneralContextPoco>> GetTwitterGeneral()
        {
            List<GeneralContextPoco> twitterContexts = new List<GeneralContextPoco>();

            using (var _context = new NeverForgetBotDbContext())
            {
                var twitterContextIds = await _context.TwitterContext.Where(ctx => (ctx.IsDeleted == false)).List();

                foreach (var ctx in twitterContextIds.Result)
                {
                    twitterContexts.Add(GetTwitterGeneralByContextId(ctx.Id).Result);
                }
            }
            return twitterContexts;
        }

        private GeneralContextPoco RedditGeneralPocoFromComment(GeneralContextPoco redditContext, RedditComment redditComment)
        {
            redditContext.ContextId = redditComment.Id;
            redditContext.ContentComment = redditComment.Content;
            redditContext.AuthorComment = redditComment.Author;
            redditContext.LinkComment = redditComment.Link;
            redditContext.SubRedditComment = redditComment.SubReddit;
            redditContext.DateComment = redditComment.CommentDate;
            redditContext.PostType = PostTypeEnum.Comment;
            redditContext.SourceType = SourceTypeEnum.Reddit;

            return redditContext;
        }

        private GeneralContextPoco RedditGeneralPocoFromSubmission(GeneralContextPoco redditContext, RedditSubmission redditSubmission)
        {
            redditContext.ContextId = redditSubmission.Id;
            redditContext.TitleSubmission = redditSubmission.Title;
            redditContext.ContentSubmission = redditSubmission.Content;
            redditContext.AuthorSubmission = redditSubmission.Author;
            redditContext.MediaLinkSubmission = redditSubmission.MediaLink;
            redditContext.LinkSubmission = redditSubmission.Link;
            redditContext.SubRedditSubmission = redditSubmission.SubReddit;
            redditContext.DateSubmission = redditSubmission.SubmissionDate;
            redditContext.PostType = PostTypeEnum.Submission;
            redditContext.SourceType = SourceTypeEnum.Reddit;

            return redditContext;
        }

        private GeneralContextPoco TwitterGeneralPocoFromComment(GeneralContextPoco twitterContext, TwitterComment twitterComment)
        {
            twitterContext.ContextId = twitterComment.Id;
            twitterContext.ContentComment = twitterComment.Content;
            twitterContext.AuthorComment = twitterComment.Author;
            twitterContext.MediaLinkComment = twitterComment.MediaLink;
            twitterContext.LinkComment = twitterComment.Link;
            twitterContext.DateComment = twitterComment.CommentDate;
            twitterContext.PostType = PostTypeEnum.Comment;
            twitterContext.SourceType = SourceTypeEnum.Twitter;

            return twitterContext;
        }

        private GeneralContextPoco TwitterGeneralPocoFromSubmission(GeneralContextPoco twitterContext, TwitterSubmission twitterSubmission)
        {
            twitterContext.ContextId = twitterSubmission.Id;
            twitterContext.ContentSubmission = twitterSubmission.Content;
            twitterContext.AuthorSubmission = twitterSubmission.Author;
            twitterContext.MediaLinkSubmission = twitterSubmission.MediaLink;
            twitterContext.LinkSubmission = twitterSubmission.Link;
            twitterContext.DateSubmission = twitterSubmission.SubmissionDate;
            twitterContext.PostType = PostTypeEnum.Submission;
            twitterContext.SourceType = SourceTypeEnum.Reddit;

            return twitterContext;
        }
    }
}