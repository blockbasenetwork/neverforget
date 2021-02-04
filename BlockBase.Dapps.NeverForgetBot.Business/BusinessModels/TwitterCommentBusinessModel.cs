using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.NeverForgetBot.Business.BusinessModels
{
    public class TwitterCommentBusinessModel
    {
        public Guid Id { get; set; }
        public Guid TwitterContextId { get; set; }
        public TwitterContext TwitterContext { get; set; }
        public string CommentId { get; set; }
        public string ReplyToId { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime CommentDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public static TwitterCommentBusinessModel FromData(TwitterComment twitterComment)
        {
            return new TwitterCommentBusinessModel()
            {
                Id = twitterComment.Id,
                TwitterContextId = twitterComment.TwitterContextId,
                TwitterContext = twitterComment.TwitterContext,
                CommentId = twitterComment.CommentId,
                ReplyToId = twitterComment.ReplyToId,
                Text = twitterComment.Text,
                Author = twitterComment.Author,
                CommentDate = twitterComment.CommentDate,
                CreatedAt = twitterComment.CreatedAt,
                IsDeleted = twitterComment.IsDeleted,
                DeletedAt = twitterComment.DeletedAt
            };
        }

        public TwitterComment ToData()
        {
            return new TwitterComment()
            {
                Id = Id,
                TwitterContextId = TwitterContextId,
                TwitterContext = TwitterContext,
                CommentId = CommentId,
                ReplyToId = ReplyToId,
                Text = Text,
                Author = Author,
                CommentDate = CommentDate,
                CreatedAt = CreatedAt,
                IsDeleted = IsDeleted,
                DeletedAt = DeletedAt
            };
        }
    }
}