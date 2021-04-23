using BlockBase.BBLinq.DataAnnotations;
using System;

namespace BlockBase.Dapps.NeverForget.Data.Entities
{
    [Table(Name = "TwitterSubmissions")]
    public class TwitterSubmission
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public string SubmissionId { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string? MediaLink { get; set; }
        public string Link { get; set; }
        [ComparableDate]
        public DateTime SubmissionDate { get; set; }
        [ForeignKey(Parent = typeof(TwitterContext))]
        public Guid TwitterContextId { get; set; }
    }
}
