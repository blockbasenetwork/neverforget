using BlockBase.Dapps.NeverForget.Data.Pocos;
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
        public int RequestTypeId { get; set; }

        public TwitterDetailsViewModel()
        {
            Comments = new List<TwitterDetail>();
            Submission = new TwitterDetail();
        }

        public static TwitterDetailsViewModel FromData(TwitterContextPoco twitterContext)
        {
            TwitterDetailsViewModel twitterDetailViewModel = new TwitterDetailsViewModel
            {
                Id = twitterContext.Context.Id,
                RequestTypeId = twitterContext.Context.RequestTypeId
            };

            twitterContext.Comments.OrderByDescending(comments => comments.CommentDate).ToList();

            foreach (var comment in twitterContext.Comments)
            {
                twitterDetailViewModel.Comments.Add(new TwitterDetail()
                {
                    Id = comment.Id,
                    Date = comment.CommentDate,
                    Author = comment.Author,
                    Content = comment.Content,
                    Link = comment.Link,
                    MediaLink = comment.MediaLink
                });
            }

            if (twitterContext.Submission != null)
            {
                twitterDetailViewModel.Submission.Id = twitterContext.Submission.Id;
                twitterDetailViewModel.Submission.Date = twitterContext.Submission.SubmissionDate;
                twitterDetailViewModel.Submission.Author = twitterContext.Submission.Author;
                twitterDetailViewModel.Submission.Content = twitterContext.Submission.Content;
                twitterDetailViewModel.Submission.Link = twitterContext.Submission.Link;
                twitterDetailViewModel.Submission.MediaLink = twitterContext.Submission.MediaLink;
            }
            else
            {
                twitterDetailViewModel.Submission = null;
            }

            return twitterDetailViewModel;
        }

        public class TwitterDetail
        {
            public Guid Id { get; set; }
            public DateTime Date { get; set; }
            public string Author { get; set; }
            public string Content { get; set; }
            public string Link { get; set; }
            public string? MediaLink { get; set; }

        }
    }
}