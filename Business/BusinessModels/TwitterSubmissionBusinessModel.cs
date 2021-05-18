using BlockBase.Dapps.NeverForget.Data.Pocos;
using System;

namespace BlockBase.Dapps.NeverForget.Business.BusinessModels
{
    public class TwitterSubmissionBusinessModel
    {
        public string Content { get; set; }
        public string Author { get; set; }
        public string MediaLink { get; set; }
        public string Link { get; set; }
        public DateTime SubmissionDate { get; set; }

        public static TwitterSubmissionBusinessModel From(TwitterContextPoco model)
        {
            if (model.SubmissionAuthor == null)
                return null;

            return new TwitterSubmissionBusinessModel()
            {
                Content = model.SubmissionContent,
                Author = model.SubmissionAuthor,
                SubmissionDate = model.SubmissionDate,
                Link = model.SubmissionLink,
                MediaLink = model.SubmissionMediaLink
            };
        }
    }
}

