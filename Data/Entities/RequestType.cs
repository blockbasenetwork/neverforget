using BlockBase.BBLinq.DataAnnotations;
using BlockBase.Dapps.NeverForget.Common.Enums;
using System;
using System.Collections.Generic;

namespace BlockBase.Dapps.NeverForget.Data.Entities
{
    [Table(Name = "RequestTypes")]
    public class RequestType
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RedditContext> RedditContext { get; set; }
        public virtual ICollection<TwitterContext> TwitterContext { get; set; }
        public RequestTypeEnum Type { get; set; }
    }
}
