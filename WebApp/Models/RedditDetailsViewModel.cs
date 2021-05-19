using BlockBase.Dapps.NeverForget.Business.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlockBase.Dapps.NeverForget.WebApp.Models
{
    public class RedditDetailsViewModel
    {
        public Guid Id { get; set; }
        public List<RedditDetail> Comments { get; set; }
        public RedditDetail Submission { get; set; }

        public RedditDetailsViewModel()
        {
            Comments = new List<RedditDetail>();
        }

        public static RedditDetailsViewModel FromData(RedditContextBusinessModel model)
        {
            RedditDetailsViewModel redditDetailViewModel = new RedditDetailsViewModel
            {
                Id = model.Id,
                Comments = new List<RedditDetail>()
            };

            var comments = model.RedditComments.OrderBy(c => c.CommentDate);
            foreach (var comment in comments)
            {
                redditDetailViewModel.Comments.Add(new RedditDetail()
                {
                    Date = comment.CommentDate,
                    Author = comment.Author,
                    Content = HttpUtility.HtmlDecode(comment.Content),
                    Link = comment.Link,
                    SubReddit = comment.SubReddit
                });
            }

            if (model.RedditSubmission != null)
            {
                redditDetailViewModel.Submission = new RedditDetail()
                {
                    Date = model.RedditSubmission.SubmissionDate,
                    Author = model.RedditSubmission.Author,
                    Content = HttpUtility.HtmlDecode(model.RedditSubmission.Content),
                    Link = model.RedditSubmission.Link,
                    MediaLink = model.RedditSubmission.MediaLink,
                    SubReddit = model.RedditSubmission.SubReddit,
                    Title = HttpUtility.HtmlDecode(model.RedditSubmission.Title)
                };
            }

            return redditDetailViewModel;
        }
    }

    public class RedditDetail
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string SubReddit { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string Link { get; set; }
        public string MediaLink { get; set; }
    }
}
