using BlockBase.Dapps.NeverForgetBot.Common.Enums;
using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using System;
using System.Linq;

namespace BlockBase.Dapps.NeverForgetBot.WebApp.Models
{
    public class TwitterContextViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string Link { get; set; }
        public string? MediaLink { get; set; }
        public int RequestTypeId { get; set; }

        public static TwitterContextViewModel FromData(TwitterContextPoco twitterContext)
        {
            TwitterContextViewModel tcvm = new TwitterContextViewModel();

            tcvm.Id = twitterContext.Context.Id;
            tcvm.RequestTypeId = twitterContext.Context.RequestTypeId;

            twitterContext.Comments.OrderByDescending(c => c.CommentDate).ToList();
            twitterContext.Comments.RemoveAt(0);

            if (twitterContext.Comments.Count == 0)
            {
                tcvm.Author = twitterContext.Submission.Author;
                tcvm.Content = twitterContext.Submission.Content;
                tcvm.Date = twitterContext.Submission.SubmissionDate;
                tcvm.Link = twitterContext.Submission.Link;
                tcvm.MediaLink = twitterContext.Submission.MediaLink;
            }
            else
            {
                tcvm.Author = twitterContext.Comments[0].Author;
                tcvm.Content = twitterContext.Comments[0].Content;
                tcvm.Date = twitterContext.Comments[0].CommentDate;
                tcvm.Link = twitterContext.Comments[0].Link;
                tcvm.MediaLink = twitterContext.Comments[0].MediaLink;
            }
            return tcvm;
        }
    }
}