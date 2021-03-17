using BlockBase.BBLinq.DataAnnotations;
using System.Collections.Generic;

namespace BlockBase.Dapps.NeverForgetBot.Data.Entities
{
    [Table("RequestTypes")]
    public class RequestType
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RedditContext> RedditContexts { get; set; }
        public virtual ICollection<TwitterContext> TwitterContexts { get; set; }
    }
}
