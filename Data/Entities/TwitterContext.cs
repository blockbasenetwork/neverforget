using BlockBase.BBLinq.DataAnnotations;
using BlockBase.Dapps.NeverForget.Data.Entities.Base;
using BlockBase.Dapps.NeverForget.Data.Interfaces;
using System;
using System.Collections.Generic;

namespace BlockBase.Dapps.NeverForget.Data.Entities
{
    [Table(Name = "TwitterContexts")]
    public class TwitterContext : AuditEntity, IEntity
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public virtual ICollection<TwitterComment> TwitterComments { get; set; }
        public virtual TwitterSubmission TwitterSubmission { get; set; }

        [ForeignKey(Parent = typeof(RequestType))]
        public int RequestTypeId { get; set; }
    }
}