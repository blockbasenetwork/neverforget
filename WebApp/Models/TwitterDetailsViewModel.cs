using BlockBase.Dapps.NeverForget.Business.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlockBase.Dapps.NeverForget.WebApp.Models
{
    public class TwitterDetailsViewModel
    {


        public Guid Id { get; set; }
        public List<TwitterDetail> Comments { get; set; }
        public TwitterDetail Submission { get; set; }

        public TwitterDetailsViewModel()
        {
            Comments = new List<TwitterDetail>();
        }

        public static TwitterDetailsViewModel FromData(TwitterContextBusinessModel model)
        {
            TwitterDetailsViewModel twitterDetailViewModel = new TwitterDetailsViewModel
            {
                Id = model.Id,
                Comments = new List<TwitterDetail>()
            };

            var comments = model.TwitterComments.OrderBy(c => c.CommentDate);
            foreach (var comment in comments)
            {
                twitterDetailViewModel.Comments.Add(new TwitterDetail()
                {
                    Date = comment.CommentDate,
                    Author = comment.Author,
                    Content = comment.Content,
                    Link = comment.Link,
                    MediaLink = comment.MediaLink
                });
            }

            if (model.TwitterSubmission != null)
            {
                twitterDetailViewModel.Submission = new TwitterDetail()
                {
                    Date = model.TwitterSubmission.SubmissionDate,
                    Author = model.TwitterSubmission.Author,
                    Content = model.TwitterSubmission.Content,
                    Link = model.TwitterSubmission.Link,
                    MediaLink = model.TwitterSubmission.MediaLink
                };
            }

            return twitterDetailViewModel;
        }


        public class TwitterDetail
        {
            public DateTime Date { get; set; }
            public string Author { get; set; }
            public string Content { get; set; }
            public string Link { get; set; }
            public string? MediaLink { get; set; }

        }
    }
}