using BlockBase.BBLinq.Annotations;
using System.Collections.Generic;

namespace BlockBase.Dapps.NeverForgetBot.Data.Entities
{
    [Table(Name = "RequestTypes")]
    public class RequestType
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RedditContext> RedditContexts { get; set; }
        public virtual ICollection<TwitterContext> TwitterContexts { get; set; }



    }
}
