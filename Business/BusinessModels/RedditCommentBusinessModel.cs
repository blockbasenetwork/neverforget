using BlockBase.Dapps.NeverForget.Data.Pocos;
using System;

namespace BlockBase.Dapps.NeverForget.Business.BusinessModels
{
    public class RedditCommentBusinessModel
    {
        public string Content { get; set; }
        public string Author { get; set; }
        public string Link { get; set; }
        public string SubReddit { get; set; }
        public DateTime CommentDate { get; set; }

        public static RedditCommentBusinessModel From(RedditContextPoco model)
        {
            if (model.CommentAuthor == null)
                return null;

            return new RedditCommentBusinessModel()
            {
                Content = model.CommentContent,
                Author = model.CommentAuthor,
                CommentDate = model.CommentDate,
                Link = model.CommentLink,
                SubReddit = model.CommentSubReddit
            };
        }
    }
}
