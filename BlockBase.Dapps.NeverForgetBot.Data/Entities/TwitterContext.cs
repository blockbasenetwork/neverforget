using BlockBase.BBLinq.Annotations;
using BlockBase.Dapps.NeverForgetBot.Data.Entities.Base;
using BlockBase.Dapps.NeverForgetBot.Data.Interfaces;
using System;

namespace BlockBase.Dapps.NeverForgetBot.Data.Entities
{
    [Table(Name = "TwitterContexts")]
    public class TwitterContext : AuditEntity, IEntity
    {
        [PrimaryKey]
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
    }
}
