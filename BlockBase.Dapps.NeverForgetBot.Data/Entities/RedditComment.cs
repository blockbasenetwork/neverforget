using BlockBase.BBLinq.Annotations;
using BlockBase.Dapps.NeverForgetBot.Data.Entities.Base;
using BlockBase.Dapps.NeverForgetBot.Data.Interfaces;
using System;

namespace BlockBase.Dapps.NeverForgetBot.Data.Entities
{
    [Table(Name = "RedditComments")]
    public class RedditComment : AuditEntity, IEntity
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public string CommentId { get; set; }
        public string ParentId { get; set; }
        public string ParentSubmissionId { get; set; }
        public string Text { get; set; }
        public DateTime CommentDate { get; set; }
        public string Author { get; set; }
        public string SubReddit { get; set; }

        [ForeignKey(Name = "RedditContexts")]
        public Guid RedditContextId { get; set; }
        public virtual RedditContext RedditContext { get; set; }
    }
}
