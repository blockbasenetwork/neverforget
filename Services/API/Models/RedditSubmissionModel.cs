﻿using BlockBase.Dapps.NeverForget.Common;
using BlockBase.Dapps.NeverForget.Data.Entities;
using System;

namespace BlockBase.Dapps.NeverForget.Services.API.Models
{
    public class RedditSubmissionModel
    {
        public string Author { get; set; }
        public string SelfText { get; set; }
        public DateTime Created_Utc { get; set; }
        public string Id { get; set; }
        public string SubReddit { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Full_Link { get; set; }

        public RedditSubmission ToData()
        {
            return new RedditSubmission()
            {
                Id = Guid.NewGuid(),
                Title = Title,
                Author = Author,
                Content = Helpers.CleanComment(SelfText),
                SubmissionDate = Created_Utc,
                SubmissionId = Id,
                SubReddit = SubReddit,
                MediaLink = Url,
                Link = Full_Link,
                CreatedAt = DateTime.UtcNow
            };
        }
    }

    public class RedditSubmissionResultModel
    {
        public RedditSubmissionModel[] Data { get; set; }
    }
}
