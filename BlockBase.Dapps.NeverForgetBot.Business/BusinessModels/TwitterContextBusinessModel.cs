using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;

namespace BlockBase.Dapps.NeverForgetBot.Business.BusinessModels
{
    public class TwitterContextBusinessModel
    {
        public Guid Id { get; set; }
        public long CommentId { get; set; }
        public DateTime PostingDate { get; set; }
        public string Author { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public static TwitterContextBusinessModel FromData(TwitterContext twitterContext)
        {
            return new TwitterContextBusinessModel()
            {
                Id = twitterContext.Id,
                CommentId = twitterContext.CommentId,
                PostingDate = twitterContext.PostingDate,
                Author = twitterContext.Author,
                CreatedAt = twitterContext.CreatedAt,
                UpdatedAt = twitterContext.UpdatedAt,
                IsDeleted = twitterContext.IsDeleted,
                DeletedAt = twitterContext.DeletedAt
            };
        }

        public TwitterContext ToData()
        {
            return new TwitterContext()
            {
                Id = Id,
                CommentId = CommentId,
                PostingDate = PostingDate,
                Author = Author,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt,
                IsDeleted = IsDeleted,
                DeletedAt = DeletedAt
            };
        }
    }
}

