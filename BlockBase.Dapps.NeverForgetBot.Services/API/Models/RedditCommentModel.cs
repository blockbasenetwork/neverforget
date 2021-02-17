using BlockBase.Dapps.NeverForgetBot.Common;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;
using System.Globalization;

namespace BlockBase.Dapps.NeverForgetBot.Services.API.Models
{
    public class RedditCommentResultModel
    {
        public RedditCommentModel[] Data { get; set; }
    }

    public class RedditCommentModel
    {
        public string Author { get; set; }
        public string Body { get; set; }
        public int Created_Utc { get; set; }
        public string Id { get; set; }
        public string SubReddit { get; set; }
        public string Parent_Id { get; set; }
        public string Link_Id { get; set; }
        public string Permalink { get; set; }

        public RedditComment ToData()
        {
            return new RedditComment()
            {
                Id = Guid.NewGuid(),
                Author = Author,
                Content = Helpers.CleanComment(Body),
                CommentDate = Helpers.FromUnixTime(Created_Utc),
                CommentId = Id,
                ParentId = Parent_Id,
                ParentSubmissionId = Link_Id,
                SubReddit = SubReddit,
                Link = Permalink,
                CreatedAt = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)
            };
        }
    }
}
