using BlockBase.BBLinq.Annotations;
using BlockBase.Dapps.NeverForgetBot.Data.Entities.Base;

namespace BlockBase.Dapps.NeverForgetBot.Data.Entities
{
    [Table(Name = "RequestTypes")]
    public class RequestType : AuditEntity
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual RedditContext RedditContext { get; set; }
        public virtual TwitterContext TwitterContext { get; set; }


    }
}
