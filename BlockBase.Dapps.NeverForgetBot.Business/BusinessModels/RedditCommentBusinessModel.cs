using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;

namespace BlockBase.Dapps.NeverForgetBot.Business.BusinessModels
{
    public class RedditCommentBusinessModel
    {
        public Guid Id { get; set; }
        public Guid RedditContextId { get; set; }
        public string CommentId { get; set; }
        public string ParentId { get; set; }
        public string ParentSubmissionId { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public string SubReddit { get; set; }
        public DateTime CommentDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public static RedditCommentBusinessModel FromData(RedditComment redditComment)
        {
            return new RedditCommentBusinessModel()
            {
                Id = redditComment.Id,
                RedditContextId = redditComment.RedditContextId,
                CommentId = redditComment.CommentId,
                ParentId = redditComment.ParentId,
                ParentSubmissionId = redditComment.ParentSubmissionId,
                Text = redditComment.Text,
                Author = redditComment.Author,
                SubReddit = redditComment.SubReddit,
                CommentDate = redditComment.CommentDate,
                CreatedAt = redditComment.CreatedAt,
                IsDeleted = redditComment.IsDeleted,
                DeletedAt = redditComment.DeletedAt
            };
        }

        public RedditComment ToData()
        {
            return new RedditComment()
            {
                Id = Id,
                RedditContextId = RedditContextId,
                CommentId = CommentId,
                ParentId = ParentId,
                ParentSubmissionId = ParentSubmissionId,
                Text = Text,
                Author = Author,
                SubReddit = SubReddit,
                CommentDate = CommentDate,
                CreatedAt = CreatedAt,
                IsDeleted = IsDeleted,
                DeletedAt = DeletedAt
            };
        }
    }
}
