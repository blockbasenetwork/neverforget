using BlockBase.Dapps.NeverForget.Common;
using BlockBase.Dapps.NeverForget.Data.Entities;
using System;

namespace BlockBase.Dapps.NeverForget.Services.API.Models
{
    public class TweetModel
    {
        public string Id { get; set; }
        public string Full_text { get; set; }
        public TweetAuthor User { get; set; }
        public TweetEntity? Entities { get; set; }
        public DateTime Created_at { get; set; }
        public string? In_reply_to_status_id_str { get; set; }
        public string? In_reply_to_user_id_str { get; set; }
        public string? In_reply_to_screen_name { get; set; }


        public TwitterComment ToComment()
        {
            return new TwitterComment()
            {
                Id = Guid.NewGuid(),
                TwitterContextId = Guid.Empty,
                CommentId = Id,
                ReplyToId = In_reply_to_status_id_str,
                Content = Helpers.CleanComment(Full_text),
                Author = User.Screen_name,
                MediaLink = Entities.Media != null ? Entities.Media[0].media_url : null,
                Link = $"http://www.twitter.com/{User.Screen_name}/status/{Id}",
                CommentDate = Created_at,
                CreatedAt = DateTime.UtcNow
            };
        }

        public TwitterSubmission ToSubmission()
        {
            return new TwitterSubmission()
            {
                Id = Guid.NewGuid(),
                TwitterContextId = Guid.Empty,
                SubmissionId = Id,
                Content = Helpers.CleanComment(Full_text),
                Author = User.Screen_name,
                MediaLink = Entities.Media != null ? Entities.Media[0].media_url : null,
                Link = $"http://www.twitter.com/{User.Screen_name}/status/{Id}",
                SubmissionDate = Created_at,
                CreatedAt = DateTime.UtcNow
            };
        }
    }

    public class TweetAuthor
    {
        public string Screen_name { get; set; }
        public string Id_str { get; set; }
    }
    public class TweetEntity
    {
        public TweetMedia[]? Media { get; set; }
    }
    public class TweetMedia
    {
        public string media_url { get; set; }
    }
}
