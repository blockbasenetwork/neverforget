using BlockBase.Dapps.NeverForget.Business.BusinessModels;
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

        public static TwitterContextViewModel FromData(TwitterContextBusinessModel model)
        {
            TwitterContextViewModel twitterContextViewModel = new TwitterContextViewModel
            {
                Id = model.Id
            };

            if (model.TwitterComments.Count == 2)
            {
                var comments = model.TwitterComments.OrderBy(c => c.CommentDate);
                twitterContextViewModel.Date = comments.ElementAt(0).CommentDate;
                twitterContextViewModel.Author = comments.ElementAt(0).Author;
                twitterContextViewModel.Content = comments.ElementAt(0).Content;
                twitterContextViewModel.Link = comments.ElementAt(0).Link;
                twitterContextViewModel.MediaLink = comments.ElementAt(0).MediaLink;
            }
            else
            {
                twitterContextViewModel.Date = model.TwitterSubmission.SubmissionDate;
                twitterContextViewModel.Author = model.TwitterSubmission.Author;
                twitterContextViewModel.Content = model.TwitterSubmission.Content;
                twitterContextViewModel.Link = model.TwitterSubmission.Link;
                twitterContextViewModel.MediaLink = model.TwitterSubmission.MediaLink;
            }

            return twitterContextViewModel;
        }
    }
}