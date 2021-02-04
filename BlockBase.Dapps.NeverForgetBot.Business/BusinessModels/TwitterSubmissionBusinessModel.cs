using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.NeverForgetBot.Business.BusinessModels
{
    public class TwitterSubmissionBusinessModel
    {
        public Guid Id { get; set; }
        public Guid TwitterContextId { get; set; }
        public TwitterContext TwitterContext { get; set; }
        public string SubmissionId { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime SubmissionDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public static TwitterSubmissionBusinessModel FromData(TwitterSubmission twitterSubmission)
        {
            return new TwitterSubmissionBusinessModel()
            {
                Id = twitterSubmission.Id,
                TwitterContextId = twitterSubmission.TwitterContextId,
                TwitterContext = twitterSubmission.TwitterContext,
                SubmissionId = twitterSubmission.SubmissionId,
                Text = twitterSubmission.Text,
                Author = twitterSubmission.Author,
                SubmissionDate = twitterSubmission.SubmissionDate,
                CreatedAt = twitterSubmission.CreatedAt,
                IsDeleted = twitterSubmission.IsDeleted,
                DeletedAt = twitterSubmission.DeletedAt
            };
        }

        public TwitterSubmission ToData()
        {
            return new TwitterSubmission()
            {
                Id = Id,
                TwitterContextId = TwitterContextId,
                TwitterContext = TwitterContext,
                SubmissionId = SubmissionId,
                Text = Text,
                Author = Author,
                SubmissionDate = SubmissionDate,
                CreatedAt = CreatedAt,
                IsDeleted = IsDeleted,
                DeletedAt = DeletedAt
            };
        }
    }
}