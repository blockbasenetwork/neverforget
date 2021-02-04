using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;

namespace BlockBase.Dapps.NeverForgetBot.Business.BusinessModels
{
    public class RedditSubmissionBusinessModel
    {
        public Guid Id { get; set; }
        public Guid RedditContextId { get; set; }
        public string SubmissionId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public string SubReddit { get; set; }
        public DateTime SubmissionDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public static RedditSubmissionBusinessModel FromData(RedditSubmission redditSubmission)
        {
            return new RedditSubmissionBusinessModel()
            {
                Id = redditSubmission.Id,
                RedditContextId = redditSubmission.RedditContextId,
                SubmissionId = redditSubmission.SubmissionId,
                Title = redditSubmission.Title,
                Text = redditSubmission.Text,
                Author = redditSubmission.Author,
                SubReddit = redditSubmission.SubReddit,
                SubmissionDate = redditSubmission.SubmissionDate,
                CreatedAt = redditSubmission.CreatedAt,
                IsDeleted = redditSubmission.IsDeleted,
                DeletedAt = redditSubmission.DeletedAt
            };
        }

        public RedditSubmission ToData()
        {
            return new RedditSubmission()
            {
                Id = Id,
                RedditContextId = RedditContextId,
                SubmissionId = SubmissionId,
                Title = Title,
                Text = Text,
                Author = Author,
                SubReddit = SubReddit,
                SubmissionDate = SubmissionDate,
                CreatedAt = CreatedAt,
                IsDeleted = IsDeleted,
                DeletedAt = DeletedAt
            };
        }
    }
}
