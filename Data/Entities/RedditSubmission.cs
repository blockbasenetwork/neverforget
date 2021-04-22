using BlockBase.BBLinq.DataAnnotations;
using BlockBase.Dapps.NeverForget.Data.Entities.Base;
using BlockBase.Dapps.NeverForget.Data.Interfaces;
using System;

namespace BlockBase.Dapps.NeverForget.Data.Entities
{
    [Table(Name = "RedditSubmissions")]
    public class RedditSubmission : AuditEntity, IEntity
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public string SubmissionId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string SubReddit { get; set; }
        public string? MediaLink { get; set; }
        public string Link { get; set; }
        [ComparableDate]
        public DateTime SubmissionDate { get; set; }

        [ForeignKey(Parent = typeof(RedditContext))]
        public Guid RedditContextId { get; set; }
    }
}
