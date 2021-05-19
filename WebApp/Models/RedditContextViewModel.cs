using BlockBase.Dapps.NeverForget.Business.BusinessModels;
using System;
using System.Linq;
using System.Web;

namespace BlockBase.Dapps.NeverForget.WebApp.Models
{
    public class RedditContextViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string SubReddit { get; set; }
        public string Link { get; set; }
        public string MediaLink { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }

        public static RedditContextViewModel FromData(RedditContextBusinessModel model)
        {
            RedditContextViewModel redditContextViewModel = new RedditContextViewModel
            {
                Id = model.Id
            };

            if (model.RedditComments.Count == 2)
            {
                var comments = model.RedditComments.OrderBy(c => c.CommentDate);
                redditContextViewModel.Date = comments.ElementAt(0).CommentDate;
                redditContextViewModel.Author = comments.ElementAt(0).Author;
                redditContextViewModel.Content = HttpUtility.HtmlDecode(comments.ElementAt(0).Content);
                redditContextViewModel.Link = comments.ElementAt(0).Link;
                redditContextViewModel.SubReddit = comments.ElementAt(0).SubReddit;
            }
            else
            {
                redditContextViewModel.Date = model.RedditSubmission.SubmissionDate;
                redditContextViewModel.Author = model.RedditSubmission.Author;
                redditContextViewModel.Content = HttpUtility.HtmlDecode(model.RedditSubmission.Content);
                redditContextViewModel.Link = model.RedditSubmission.Link;
                redditContextViewModel.MediaLink = model.RedditSubmission.MediaLink;
                redditContextViewModel.SubReddit = model.RedditSubmission.SubReddit;
                redditContextViewModel.Title = HttpUtility.HtmlDecode(model.RedditSubmission.Title);
            }

            return redditContextViewModel;
        }
    }
}
