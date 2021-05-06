using BlockBase.Dapps.NeverForget.Data.Pocos;
using System;
using System.Linq;

namespace BlockBase.Dapps.NeverForget.WebApp.Models
{
    public class RedditContextViewModel
    {
        public Guid Id { get; set; }
        public string? Content { get; set; }
        public string Author { get; set; }
        public string Subreddit { get; set; }
        public string Link { get; set; }
        public string? MediaLink { get; set; }
        public string? Title { get; set; }
        public DateTime Date { get; set; }
        public int RequestTypeId { get; set; }

        public static RedditContextViewModel FromData(RedditContextPoco redditContext)
        {
            RedditContextViewModel redditContextViewModel = new RedditContextViewModel
            {
                Id = redditContext.Context.Id,
                RequestTypeId = redditContext.Context.RequestTypeId
            };

            redditContext.Comments.OrderByDescending(c => c.CommentDate).ToList();
            redditContext.Comments.RemoveAt(0);

            if (redditContext.Comments.Count == 0)
            {
                redditContextViewModel.Author = redditContext.Submission.Author;
                redditContextViewModel.Content = redditContext.Submission.Content;
                redditContextViewModel.Id = redditContext.Context.Id;
                redditContextViewModel.Date = redditContext.Submission.SubmissionDate;
                redditContextViewModel.Subreddit = redditContext.Submission.SubReddit;
                redditContextViewModel.Title = redditContext.Submission.Title;
                redditContextViewModel.Link = redditContext.Submission.Link;
                redditContextViewModel.MediaLink = redditContext.Submission.MediaLink;

            }
            else
            {
                redditContextViewModel.Author = redditContext.Comments[0].Author;
                redditContextViewModel.Content = redditContext.Comments[0].Content;
                redditContextViewModel.Id = redditContext.Context.Id;
                redditContextViewModel.Date = redditContext.Comments[0].CommentDate;
                redditContextViewModel.Subreddit = redditContext.Comments[0].SubReddit;
                redditContextViewModel.Link = redditContext.Comments[0].Link;

            }
            return redditContextViewModel;
        }
    }
}
