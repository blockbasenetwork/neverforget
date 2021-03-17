using BlockBase.BBLinq.DataAnnotations;
using BlockBase.Dapps.NeverForgetBot.Data.Entities.Base;
using BlockBase.Dapps.NeverForgetBot.Data.Interfaces;
using System;

namespace BlockBase.Dapps.NeverForgetBot.Data.Entities
{
    [Table("TwitterComments")]
    public class TwitterComment : AuditEntity, IEntity
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public string CommentId { get; set; }
        public string? ReplyToId { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string? MediaLink { get; set; }
        public string Link { get; set; }
        public DateTime CommentDate { get; set; }

        [ForeignKey(typeof(TwitterContext))]
        public Guid TwitterContextId { get; set; }
    }
}
