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

        public static TwitterContextViewModel FromData(TwitterContextPoco twitterContext)
        {
            TwitterContextViewModel twitterContextViewModel = new TwitterContextViewModel
            {
                Id = twitterContext.ContextId
            };

            if (twitterContext.SubmissionLink == null)
            {
                twitterContextViewModel.Date = twitterContext.CommentDate;
                twitterContextViewModel.Author = twitterContext.CommentAuthor;
                twitterContextViewModel.Content = twitterContext.CommentContent;
                twitterContextViewModel.Link = twitterContext.CommentLink;
                twitterContextViewModel.MediaLink = twitterContext.CommentMediaLink;
            }
            else
            {
                twitterContextViewModel.Date = twitterContext.SubmissionDate;
                twitterContextViewModel.Author = twitterContext.SubmissionAuthor;
                twitterContextViewModel.Content = twitterContext.SubmissionContent;
                twitterContextViewModel.Link = twitterContext.SubmissionLink;
                twitterContextViewModel.MediaLink = twitterContext.SubmissionMediaLink;
            }

            return twitterContextViewModel;
        }
    }
}