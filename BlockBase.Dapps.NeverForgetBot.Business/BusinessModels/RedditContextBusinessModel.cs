using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;

namespace BlockBase.Dapps.NeverForgetBot.Business.BusinessModels
{
    public class RedditContextBusinessModel
    {
        public Guid Id { get; set; }
        public string CommentId { get; set; }
        public string CommentPost { get; set; }
        public int PostingDate { get; set; }
        public string Author { get; set; }
        public string SubReddit { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public static RedditContextBusinessModel FromData(RedditContext redditContext)
        {
            return new RedditContextBusinessModel()
            {
                Id = redditContext.Id,
                CommentId = redditContext.CommentId,
                CommentPost = redditContext.CommentPost,
                PostingDate = redditContext.PostingDate,
                Author = redditContext.Author,
                SubReddit = redditContext.SubReddit,
                CreatedAt = redditContext.CreatedAt,
                UpdatedAt = redditContext.UpdatedAt,
                IsDeleted = redditContext.IsDeleted,
                DeletedAt = redditContext.DeletedAt
            };
        }

        public RedditContext ToData()
        {
            return new RedditContext()
            {
                Id = Id,
                CommentId = CommentId,
                CommentPost = CommentPost,
                PostingDate = PostingDate,
                Author = Author,
                SubReddit = SubReddit,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt,
                IsDeleted = IsDeleted,
                DeletedAt = DeletedAt
            };
        }
    }
}
