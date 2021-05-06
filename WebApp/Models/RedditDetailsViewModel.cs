using BlockBase.Dapps.NeverForget.Data.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlockBase.Dapps.NeverForget.WebApp.Models
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
            RedditDetailsViewModel redditDetailsViewModel = new RedditDetailsViewModel
            {
                Id = context.Context.Id,
                RequestTypeId = context.Context.RequestTypeId
            };

            context.Comments = context.Comments.OrderBy(c => c.CommentDate).ToList();

            foreach (var comment in context.Comments)
            {
                redditDetailsViewModel.Comments.Add(new RedditDetail()
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
                redditDetailsViewModel.Submission.Id = context.Submission.Id;
                redditDetailsViewModel.Submission.Title = context.Submission.Title;
                redditDetailsViewModel.Submission.Subreddit = context.Submission.SubReddit;
                redditDetailsViewModel.Submission.Content = context.Submission.Content;
                redditDetailsViewModel.Submission.Author = context.Submission.Author;
                redditDetailsViewModel.Submission.Date = context.Submission.SubmissionDate;
                redditDetailsViewModel.Submission.Link = context.Submission.Link;
                redditDetailsViewModel.Submission.MediaLink = context.Submission.MediaLink;
            }
            else
            {
                redditDetailsViewModel.Submission = null;
            }

            return redditDetailsViewModel;
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
