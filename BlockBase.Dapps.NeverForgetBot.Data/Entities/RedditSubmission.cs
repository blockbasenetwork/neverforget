using BlockBase.BBLinq.Annotations;
using BlockBase.Dapps.NeverForgetBot.Data.Entities.Base;
using BlockBase.Dapps.NeverForgetBot.Data.Interfaces;
using System;

namespace BlockBase.Dapps.NeverForgetBot.Data.Entities
{
    [Table(Name = "RedditSubmissions")]
    public class RedditSubmission : AuditEntity, IEntity
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public string SubmissionId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public string SubReddit { get; set; }
        public DateTime CommentDate { get; set; }

        [ForeignKey(Name = "RedditContext")]
        public Guid RedditContextId { get; set; }
        public virtual RedditContext RedditContext { get; set; }
    }
}
