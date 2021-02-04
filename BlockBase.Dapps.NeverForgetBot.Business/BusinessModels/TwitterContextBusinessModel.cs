using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;

namespace BlockBase.Dapps.NeverForgetBot.Business.BusinessModels
{
    public class TwitterContextBusinessModel
    {
        public Guid Id { get; set; }
        public string TweetId { get; set; }
        public string TweetText { get; set; }
        public DateTime TweetDate { get; set; }
        public string AuthorId { get; set; }
        public string Author { get; set; }
        public string? InReplyToTweetId { get; set; }
        public string? InReplyToUserId { get; set; }
        public string? InReplyToUser { get; set; }
        //public string? Hashtags { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public static TwitterContextBusinessModel FromData(TwitterContext twitterContext)
        {
            return new TwitterContextBusinessModel()
            {
                Id = twitterContext.Id,
                TweetId = twitterContext.TweetId,
                TweetText = twitterContext.TweetText,
                TweetDate = twitterContext.TweetDate,
                AuthorId = twitterContext.AuthorId,
                Author = twitterContext.Author,
                InReplyToTweetId = twitterContext.InReplyToTweetId,
                InReplyToUserId = twitterContext.InReplyToUserId,
                InReplyToUser = twitterContext.InReplyToUser,
                //Hashtags = twitterContext.Hashtags,
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
                TweetId = TweetId,
                TweetText = TweetText,
                TweetDate = TweetDate,
                AuthorId = AuthorId,
                Author = Author,
                InReplyToTweetId = InReplyToTweetId,
                InReplyToUserId = InReplyToUserId,
                InReplyToUser = InReplyToUser,
                //Hashtags = Hashtags,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt,
                IsDeleted = IsDeleted,
                DeletedAt = DeletedAt
            };
        }
    }
}

