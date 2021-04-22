using BlockBase.BBLinq.DataAnnotations;
using System;

namespace BlockBase.Dapps.NeverForget.Data.Entities.Base
{
    public class AuditEntity
    {
        [ComparableDate]
        public DateTime CreatedAt { get; set; }

        public bool IsDeleted { get; set; }

        [ComparableDate]
        public DateTime? DeletedAt { get; set; }
    }
}
