using BlockBase.BBLinq.Annotations;
using BlockBase.Dapps.NeverForgetBot.Data.Entities.Base;
using BlockBase.Dapps.NeverForgetBot.Data.Interfaces;
using System;
using System.Collections.Generic;

namespace BlockBase.Dapps.NeverForgetBot.Data.Entities
{
    [Table(Name = "TwitterContexts")]
    public class TwitterContext : AuditEntity, IEntity
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public virtual ICollection<TwitterComment> TwitterComments { get; set; }
        public virtual TwitterSubmission TwitterSubmission { get; set; }

        [ForeignKey(Name = "RequestTypes")]
        public int RequestTypeId { get; set; }
    }
}
