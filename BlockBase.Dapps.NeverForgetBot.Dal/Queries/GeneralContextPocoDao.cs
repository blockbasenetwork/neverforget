using BlockBase.Dapps.NeverForgetBot.Common.Enums;
using BlockBase.Dapps.NeverForgetBot.Data.Context;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
                var redditComments = await _context.RedditComment.Where(rc => (rc.IsDeleted == false)).List();
                foreach (var comment in redditComments.Result)
                {
                    var content = comment.Content;
                    if (Regex.IsMatch(content, @"(!neverforgetbot+ +post)", RegexOptions.IgnoreCase) ||
                        comment.Content.ToLower().Contains("!neverforgetbot post") ||
                        comment.Content.ToLower().Contains("!neverforgetbot thread"))
                    {
                        GeneralContextPoco redditContext = new GeneralContextPoco();

                        foreach (var c in redditComments.Result)
                        {
                            if (c.CommentId == comment.ParentId)
                            {
                                //redditContext.ContentComment = c.Content;
                                //redditContext.AuthorComment = c.Author;
                                //redditContext.LinkComment = c.Link;
                                //redditContext.SubRedditComment = c.SubReddit;
                                //redditContext.CommentDateComment = c.CommentDate;
                                //redditContext.PostType = PostTypeEnum.Comment;
                                //redditContext.SourceType = SourceTypeEnum.Reddit;

                                //redditContexts.Add(redditContext);

                                //redditContext = RedditGeneralPocoFromComment(redditContext, c);
                                redditContexts.Add(RedditGeneralPocoFromComment(redditContext, c));
                            }
                        }

                        if (redditContext.PostType != PostTypeEnum.Comment)
                        {
                            var redditSubmission = await _context.RedditSubmission.Where(rs => (rs.SubmissionId == comment.ParentId) && (rs.IsDeleted == false)).List();

                            //redditContext.TitleSubmission = redditSubmission.Result.FirstOrDefault().Title;
                            //redditContext.ContentSubmission = redditSubmission.Result.FirstOrDefault().Content;
                            //redditContext.AuthorSubmission = redditSubmission.Result.FirstOrDefault().Author;
                            //redditContext.MediaLinkSubmission = redditSubmission.Result.FirstOrDefault().MediaLink;
                            //redditContext.LinkSubmission = redditSubmission.Result.FirstOrDefault().Link;
                            //redditContext.SubmissionDateSubmission = redditSubmission.Result.FirstOrDefault().SubmissionDate;
                            //redditContext.PostType = PostTypeEnum.Submission;
                            //redditContext.SourceType = SourceTypeEnum.Reddit;

                            //redditContexts.Add(redditContext);

                            //redditContext = RedditGeneralPocoFromSubmission(redditContext, redditSubmission.Result.FirstOrDefault());
                            redditContexts.Add(RedditGeneralPocoFromSubmission(redditContext, redditSubmission.Result.FirstOrDefault()));
                        }
                    }
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
                    if (comment.Content.ToLower().Contains("@_neverforgetbot comment") ||
                        comment.Content.ToLower().Contains("@_neverforgetbot post") ||
                        comment.Content.ToLower().Contains("@_neverforgetbot thread"))
                    {
                        foreach (var c in twitterComments.Result)
                        {
                            if (c.CommentId == comment.ReplyToId)
                            {
                                twitterContext = TwitterGeneralPocoFromComment(twitterContext, c);

                                return twitterContext;
                            }
                        }
                        parentId = comment.ReplyToId;
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
                var twitterComents = await _context.TwitterComment.Where(rc => (rc.IsDeleted == false)).List();
                foreach (var comment in twitterComents.Result)
                {
                    if (comment.Content.ToLower().Contains("@_neverforgetbot comment") ||
                        comment.Content.ToLower().Contains("@_neverforgetbot post") ||
                        comment.Content.ToLower().Contains("@_neverforgetbot thread"))
                    {
                        GeneralContextPoco twitterContext = new GeneralContextPoco();

                        foreach (var c in twitterComents.Result)
                        {
                            if (c.ReplyToId == comment.ReplyToId)
                            {
                                twitterContext = TwitterGeneralPocoFromComment(twitterContext, c);
                                twitterContexts.Add(twitterContext);
                            }
                        }

                        if (twitterContext.PostType != PostTypeEnum.Comment)
                        {
                            var twitterSubmission = await _context.TwitterSubmission.Where(rs => (rs.SubmissionId == comment.ReplyToId) && (rs.IsDeleted == false)).List();

                            twitterContext = TwitterGeneralPocoFromSubmission(twitterContext, twitterSubmission.Result.FirstOrDefault());
                            twitterContexts.Add(twitterContext);
                        }
                    }
                }
            }
            return twitterContexts;
        }

        private GeneralContextPoco RedditGeneralPocoFromComment(GeneralContextPoco redditContext, RedditComment redditComment)
        {
            redditContext.ContentComment = redditComment.Content;
            redditContext.AuthorComment = redditComment.Author;
            redditContext.LinkComment = redditComment.Link;
            redditContext.SubRedditComment = redditComment.SubReddit;
            redditContext.CommentDateComment = redditComment.CommentDate;
            redditContext.PostType = PostTypeEnum.Comment;
            redditContext.SourceType = SourceTypeEnum.Reddit;

            return redditContext;
        }

        private GeneralContextPoco RedditGeneralPocoFromSubmission(GeneralContextPoco redditContext, RedditSubmission redditSubmission)
        {
            redditContext.TitleSubmission = redditSubmission.Title;
            redditContext.ContentSubmission = redditSubmission.Content;
            redditContext.AuthorSubmission = redditSubmission.Author;
            redditContext.MediaLinkSubmission = redditSubmission.MediaLink;
            redditContext.LinkSubmission = redditSubmission.Link;
            redditContext.SubmissionDateSubmission = redditSubmission.SubmissionDate;
            redditContext.PostType = PostTypeEnum.Submission;
            redditContext.SourceType = SourceTypeEnum.Reddit;

            return redditContext;
        }

        private GeneralContextPoco TwitterGeneralPocoFromComment(GeneralContextPoco twitterContext, TwitterComment twitterComment)
        {
            twitterContext.ContentComment = twitterComment.Content;
            twitterContext.AuthorComment = twitterComment.Author;
            twitterContext.MediaLinkComment = twitterComment.MediaLink;
            twitterContext.LinkComment = twitterComment.Link;
            twitterContext.CommentDateComment = twitterComment.CommentDate;
            twitterContext.PostType = PostTypeEnum.Comment;
            twitterContext.SourceType = SourceTypeEnum.Twitter;

            return twitterContext;
        }

        private GeneralContextPoco TwitterGeneralPocoFromSubmission(GeneralContextPoco twitterContext, TwitterSubmission twitterSubmission)
        {
            twitterContext.ContentSubmission = twitterSubmission.Content;
            twitterContext.AuthorSubmission = twitterSubmission.Author;
            twitterContext.MediaLinkSubmission = twitterSubmission.MediaLink;
            twitterContext.LinkSubmission = twitterSubmission.Link;
            twitterContext.SubmissionDateSubmission = twitterSubmission.SubmissionDate;
            twitterContext.PostType = PostTypeEnum.Submission;
            twitterContext.SourceType = SourceTypeEnum.Reddit;

            return twitterContext;
        }
    }
}

//dao->where-> .List();