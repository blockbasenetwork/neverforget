using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using System;
using System.Linq;

namespace BlockBase.Dapps.NeverForgetBot.WebApp.Models
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
            RedditContextViewModel rcvm = new RedditContextViewModel();

            rcvm.Id = redditContext.Context.Id;
            rcvm.RequestTypeId = redditContext.Context.RequestTypeId;

            redditContext.Comments.OrderByDescending(c => c.CommentDate).ToList();

            if (redditContext.Comments.Count > 0 && redditContext.Submission != null)
            {
                redditContext.Comments.RemoveAt(0);
            }

            if (redditContext.Comments.Count == 0)
            {
                rcvm.Author = redditContext.Submission.Author;
                rcvm.Content = redditContext.Submission.Content;
                rcvm.Id = redditContext.Context.Id;
                rcvm.Date = redditContext.Submission.SubmissionDate;
                rcvm.Subreddit = redditContext.Submission.SubReddit;
                rcvm.Title = redditContext.Submission.Title;
                rcvm.Link = redditContext.Submission.Link;
                rcvm.MediaLink = redditContext.Submission.MediaLink;

            }
            else
            {
                rcvm.Author = redditContext.Comments[0].Author;
                rcvm.Content = redditContext.Comments[0].Content;
                rcvm.Id = redditContext.Context.Id;
                rcvm.Date = redditContext.Comments[0].CommentDate;
                rcvm.Subreddit = redditContext.Comments[0].SubReddit;
                rcvm.Link = redditContext.Comments[0].Link;

            }
            return rcvm;
        }
    }
}
