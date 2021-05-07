using BlockBase.Dapps.NeverForget.Data.Pocos;
using System;
using System.Linq;

namespace BlockBase.Dapps.NeverForget.WebApp.Models
{
    public class TwitterContextViewModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public string Link { get; set; }
        public string? MediaLink { get; set; }
        public int RequestTypeId { get; set; }

        public static TwitterContextViewModel FromData(TwitterContextPoco twitterContext)
        {
            TwitterContextViewModel twitterContextViewModel = new TwitterContextViewModel
            {
                Id = twitterContext.Context.Id,
                RequestTypeId = twitterContext.Context.RequestTypeId
            };

            twitterContext.Comments.OrderByDescending(c => c.CommentDate).ToList();
            twitterContext.Comments.RemoveAt(0);

            if (twitterContext.Comments.Count == 0)
            {
                twitterContextViewModel.Date = twitterContext.Submission.SubmissionDate;
                twitterContextViewModel.Author = twitterContext.Submission.Author;
                twitterContextViewModel.Content = twitterContext.Submission.Content;
                twitterContextViewModel.Link = twitterContext.Submission.Link;
                twitterContextViewModel.MediaLink = twitterContext.Submission.MediaLink;
            }
            else
            {
                twitterContextViewModel.Date = twitterContext.Comments[0].CommentDate;
                twitterContextViewModel.Author = twitterContext.Comments[0].Author;
                twitterContextViewModel.Content = twitterContext.Comments[0].Content;
                twitterContextViewModel.Link = twitterContext.Comments[0].Link;
                twitterContextViewModel.MediaLink = twitterContext.Comments[0].MediaLink;
            }

            return twitterContextViewModel;
        }
    }
}