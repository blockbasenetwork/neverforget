using BlockBase.Dapps.NeverForgetBot.Common.Enums;
using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlockBase.Dapps.NeverForgetBot.WebApp.Models
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

        public static TwitterDetailsViewModel FromData(TwitterContextPoco context)
        {
            TwitterDetailsViewModel detailView = new TwitterDetailsViewModel();

            detailView.Id = context.Context.Id;
            detailView.RequestTypeId = context.Context.RequestTypeId;

            context.Comments.OrderByDescending(c => c.CommentDate).ToList();

            foreach (var comment in context.Comments)
            {
                //TwitterDetail c = new TwitterDetail();

                //c.Id = comment.Id;
                //c.Content = comment.Content;
                //c.Author = comment.Author;
                //c.Date = comment.CommentDate;
                //c.Link = comment.Link;
                //c.MediaLink = comment.MediaLink;

                detailView.Comments.Add(new TwitterDetail()
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    Author = comment.Author,
                    Date = comment.CommentDate,
                    Link = comment.Link,
                    MediaLink = comment.MediaLink
                });
            }

            if (context.Submission != null)
            {
                detailView.Submission.Id = context.Submission.Id;
                detailView.Submission.Content = context.Submission.Content;
                detailView.Submission.Author = context.Submission.Author;
                detailView.Submission.Date = context.Submission.SubmissionDate;
                detailView.Submission.Link = context.Submission.Link;
                detailView.Submission.MediaLink = context.Submission.MediaLink;
            }
            else
            {
                detailView.Submission = null;
            }

            detailView.Comments = detailView.Comments.OrderBy(c => c.Date).ToList();

            return detailView;
        }
    }

    public class TwitterDetail
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string Link { get; set; }
        public string? MediaLink { get; set; }
    }
}
