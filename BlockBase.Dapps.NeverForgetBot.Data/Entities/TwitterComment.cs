using BlockBase.BBLinq.Annotations;
using BlockBase.Dapps.NeverForgetBot.Data.Entities.Base;
using BlockBase.Dapps.NeverForgetBot.Data.Interfaces;
using System;

namespace BlockBase.Dapps.NeverForgetBot.Data.Entities
{
    [Table(Name = "TwitterComments")]
    public class TwitterComment : AuditEntity, IEntity
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public string CommentId { get; set; }
        public string ReplyToId { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime CommentDate { get; set; }

        [ForeignKey(Name = "TwitterContexts")]
        public Guid TwitterContextId { get; set; }
        public virtual TwitterContext TwitterContext { get; set; }
    }
}
