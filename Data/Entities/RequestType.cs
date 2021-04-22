using BlockBase.BBLinq.DataAnnotations;
using System.Collections.Generic;

namespace BlockBase.Dapps.NeverForget.Data.Entities
{
    [Table(Name = "RequestTypes")]
    public class RequestType
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RedditContext> RedditContext { get; set; }
        public virtual ICollection<TwitterContext> TwitterContext { get; set; }
    }
}
