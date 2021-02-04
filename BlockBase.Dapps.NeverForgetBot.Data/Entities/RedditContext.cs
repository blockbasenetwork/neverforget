using BlockBase.BBLinq.Annotations;
using BlockBase.Dapps.NeverForgetBot.Data.Entities.Base;
using BlockBase.Dapps.NeverForgetBot.Data.Interfaces;
using System;
using System.Collections.Generic;

namespace BlockBase.Dapps.NeverForgetBot.Data.Entities
{
    [Table(Name = "RedditContexts")]
    public class RedditContext : AuditEntity, IEntity
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public virtual ICollection<RedditComment> RedditComments { get; set; }
        public virtual ICollection<RedditSubmission> RedditSubmissions { get; set; }

        //[ForeignKey(Name = "RequestTypes")]
        //public Guid RequestTypeId { get; set; }
        //public virtual RequestType RequestType { get; set; }
    }
}
