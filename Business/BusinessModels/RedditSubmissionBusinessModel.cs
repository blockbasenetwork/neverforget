using BlockBase.Dapps.NeverForget.Data.Pocos;
using System;

namespace BlockBase.Dapps.NeverForget.Business.BusinessModels
{
    public class RedditSubmissionBusinessModel
    {
        public string Content { get; set; }
        public string Author { get; set; }
        public string MediaLink { get; set; }
        public string Link { get; set; }
        public string SubReddit { get; set; }
        public string Title { get; set; }
        public DateTime SubmissionDate { get; set; }

        public static RedditSubmissionBusinessModel From(RedditContextPoco model)
        {
            if (model.SubmissionAuthor == null)
                return null;

            return new RedditSubmissionBusinessModel()
            {
                Content = model.SubmissionContent,
                Author = model.SubmissionAuthor,
                SubmissionDate = model.SubmissionDate,
                Link = model.SubmissionLink,
                MediaLink = model.SubmissionMediaLink,
                SubReddit = model.SubmissionSubReddit,
                Title = model.SubmissionTitle
            };
        }
    }
}
