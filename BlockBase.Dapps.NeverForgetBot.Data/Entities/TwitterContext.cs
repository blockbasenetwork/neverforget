using BlockBase.BBLinq.DataAnnotations;
using BlockBase.Dapps.NeverForgetBot.Data.Entities.Base;
using BlockBase.Dapps.NeverForgetBot.Data.Interfaces;
using System;
using System.Collections.Generic;

namespace BlockBase.Dapps.NeverForgetBot.Data.Entities
{
    [Table("TwitterContexts")]
    public class TwitterContext : AuditEntity, IEntity
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public virtual ICollection<TwitterComment> TwitterComments { get; set; }
        public virtual TwitterSubmission TwitterSubmission { get; set; }

        [ForeignKey(typeof(RequestType))]
        public int RequestTypeId { get; set; }
    }
}
