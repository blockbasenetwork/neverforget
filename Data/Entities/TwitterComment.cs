using BlockBase.BBLinq.DataAnnotations;
using BlockBase.Dapps.NeverForget.Data.Entities.Base;
using BlockBase.Dapps.NeverForget.Data.Interfaces;
using System;

namespace BlockBase.Dapps.NeverForget.Data.Entities
{
    [Table(Name = "TwitterComments")]
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
        [ComparableDate]
        public DateTime CommentDate { get; set; }
        [ForeignKey(Parent = typeof(TwitterContext))]
        public Guid TwitterContextId { get; set; }
    }
}