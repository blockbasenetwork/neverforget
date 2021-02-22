using BlockBase.Dapps.NeverForgetBot.Common.Enums;
using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlockBase.Dapps.NeverForgetBot.WebApp.Models
{
    public class RedditDetailsViewModel
    {
        public Guid Id { get; set; }
        public List<RedditDetail> Comments { get; set; }
        public RedditDetail Submission { get; set; }
        public int RequestTypeId { get; set; }

        public RedditDetailsViewModel()
        {
            Comments = new List<RedditDetail>();
            Submission = new RedditDetail();
        }

        public static RedditDetailsViewModel FromData(RedditContextPoco context)
        {
            RedditDetailsViewModel detailView = new RedditDetailsViewModel();

            detailView.Id = context.Context.Id;
            detailView.RequestTypeId = context.Context.RequestTypeId;

            context.Comments.OrderByDescending(c => c.CommentDate).ToList();

            foreach (var comment in context.Comments)
            {
                detailView.Comments.Add(new RedditDetail()
                {
                    Id = comment.Id,
                    Subreddit = comment.SubReddit,
                    Content = comment.Content,
                    Author = comment.Author,
                    Date = comment.CommentDate,
                    Link = comment.Link
                });
            }

            if (context.Submission != null)
            {
                detailView.Submission.Id = context.Submission.Id;
                detailView.Submission.Title = context.Submission.Title;
                detailView.Submission.Subreddit = context.Submission.SubReddit;
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

            return detailView;
        }
    }

    public class RedditDetail
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Subreddit { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string Link { get; set; }
        public string? MediaLink { get; set; }
    }
}
