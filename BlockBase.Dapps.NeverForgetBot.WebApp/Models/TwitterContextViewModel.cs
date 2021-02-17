using BlockBase.Dapps.NeverForgetBot.Common.Enums;
using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using System;
using System.Linq;

namespace BlockBase.Dapps.NeverForgetBot.WebApp.Models
{
    public class TwitterContextViewModel
    {
        public Guid Id { get; set; }
        public SourceTypeEnum SourceType { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public int RequestTypeId { get; set; }



        public static TwitterContextViewModel FromData(TwitterContextPoco twitterContext)
        {
            TwitterContextViewModel tcvm = new TwitterContextViewModel();

            tcvm.Id = twitterContext.Context.Id;
            tcvm.RequestTypeId = twitterContext.Context.RequestTypeId;
            tcvm.SourceType = SourceTypeEnum.Reddit;

            twitterContext.Comments.OrderByDescending(c => c.CommentDate).ToList();
            twitterContext.Comments.RemoveAt(0);

            if (twitterContext.Comments.Count == 0)
            {
                tcvm.Author = twitterContext.Submission.Author;
                tcvm.Content = twitterContext.Submission.Content;
                tcvm.Id = twitterContext.Context.Id;
                tcvm.Date = twitterContext.Submission.SubmissionDate;
            }
            else
            {
                tcvm.Author = twitterContext.Comments[0].Author;
                tcvm.Content = twitterContext.Comments[0].Content;
                tcvm.Id = twitterContext.Context.Id;
                tcvm.Date = twitterContext.Comments[0].CommentDate;
            }
            return tcvm;
        }
    }
}
