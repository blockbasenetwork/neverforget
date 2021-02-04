using System;

namespace BlockBase.Dapps.NeverForgetBot.Data.Entities.Base
{
    public abstract class AuditEntity
    {
        public DateTime CreatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
