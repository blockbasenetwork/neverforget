using BlockBase.Dapps.NeverForgetBot.Common.Enums;
using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using System;
using System.Linq;

namespace BlockBase.Dapps.NeverForgetBot.WebApp.Models
{
    public class RedditContextViewModel
    {
        public Guid Id { get; set; }
        public SourceTypeEnum SourceType { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Subreddit { get; set; }
        public string? Title { get; set; }
        public DateTime Date { get; set; }
        public int RequestTypeId { get; set; }




        public static RedditContextViewModel FromData(RedditContextPoco redditContext)
        {
            RedditContextViewModel rcvm = new RedditContextViewModel();

            rcvm.Id = redditContext.Context.Id;
            rcvm.RequestTypeId = redditContext.Context.RequestTypeId;
            rcvm.SourceType = SourceTypeEnum.Reddit;

            redditContext.Comments.OrderByDescending(c => c.CommentDate).ToList();
            redditContext.Comments.RemoveAt(0);

            if (redditContext.Comments.Count == 0)
            {
                rcvm.Author = redditContext.Submission.Author;
                rcvm.Content = redditContext.Submission.Content;
                rcvm.Id = redditContext.Context.Id;
                rcvm.Date = redditContext.Submission.SubmissionDate;
                rcvm.Subreddit = redditContext.Submission.SubReddit;
                rcvm.Title = redditContext.Submission.Title;
            }
            else
            {
                rcvm.Author = redditContext.Comments[0].Author;
                rcvm.Content = redditContext.Comments[0].Content;
                rcvm.Id = redditContext.Context.Id;
                rcvm.Date = redditContext.Comments[0].CommentDate;
                rcvm.Subreddit = redditContext.Comments[0].SubReddit;
            }
            return rcvm;
        }
    }
}
