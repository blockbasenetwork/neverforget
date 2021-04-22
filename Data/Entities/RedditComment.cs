using BlockBase.BBLinq.DataAnnotations;
using BlockBase.Dapps.NeverForget.Data.Entities.Base;
using BlockBase.Dapps.NeverForget.Data.Interfaces;
using System;

namespace BlockBase.Dapps.NeverForget.Data.Entities
{
    [Table(Name = "RedditComments")]
    public class RedditComment : AuditEntity, IEntity
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public string CommentId { get; set; }
        public string ParentId { get; set; }
        public string ParentSubmissionId { get; set; }
        public string Content { get; set; }
        [ComparableDate]
        public DateTime CommentDate { get; set; }
        public string Author { get; set; }
        public string SubReddit { get; set; }
        public string Link { get; set; }

        [ForeignKey(Parent = typeof(RedditContext))]
        public Guid RedditContextId { get; set; }
    }
}
