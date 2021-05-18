using BlockBase.Dapps.NeverForget.Data.Pocos;
using System;

namespace BlockBase.Dapps.NeverForget.Business.BusinessModels
{
    public class TwitterCommentBusinessModel
    {
        public string Content { get; set; }
        public string Author { get; set; }
        public string MediaLink { get; set; }
        public string Link { get; set; }
        public DateTime CommentDate { get; set; }

        public static TwitterCommentBusinessModel From(TwitterContextPoco model)
        {
            if (model.CommentAuthor == null)
                return null;

            return new TwitterCommentBusinessModel()
            {
                Content = model.CommentContent,
                Author = model.CommentAuthor,
                CommentDate = model.CommentDate,
                Link = model.CommentLink,
                MediaLink = model.CommentMediaLink
            };
        }
    }
}
