using BlockBase.BBLinq.DataAnnotations;
using BlockBase.Dapps.NeverForget.Data.Entities.Base;
using BlockBase.Dapps.NeverForget.Data.Interfaces;
using System;
using System.Collections.Generic;

namespace BlockBase.Dapps.NeverForget.Data.Entities
{
    [Table(Name = "RedditContexts")]
    public class RedditContext : AuditEntity, IEntity
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public virtual ICollection<RedditComment> RedditComments { get; set; }
        public virtual RedditSubmission RedditSubmission { get; set; }

        [ForeignKey(Parent = typeof(RequestType))]
        public Guid RequestTypeId { get; set; }
    }
}
