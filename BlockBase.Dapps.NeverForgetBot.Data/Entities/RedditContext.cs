using BlockBase.BBLinq.DataAnnotations;
using BlockBase.Dapps.NeverForgetBot.Data.Entities.Base;
using BlockBase.Dapps.NeverForgetBot.Data.Interfaces;
using System;
using System.Collections.Generic;

namespace BlockBase.Dapps.NeverForgetBot.Data.Entities
{
    [Table("RedditContexts")]
    public class RedditContext : AuditEntity, IEntity
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public virtual ICollection<RedditComment> RedditComments { get; set; }
        public virtual RedditSubmission RedditSubmission { get; set; }

        [ForeignKey(typeof(RequestType))]
        public int RequestTypeId { get; set; }
    }
}
