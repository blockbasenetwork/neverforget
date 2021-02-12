using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;

namespace BlockBase.Dapps.NeverForgetBot.Data.Pocos
{
    public class RedditContextPoco
    {
        public virtual Guid ContextId { get; set; }

        public virtual RedditComment ContentComment { get; set; }
        public virtual RedditComment AuthorComment { get; set; }
        public virtual RedditComment SubredditComment { get; set; }
        public virtual RedditComment LinkComment { get; set; }
        public virtual RedditComment CommentDateComment { get; set; }

        public virtual RedditSubmission TitleSubmission { get; set; }
        public virtual RedditSubmission ContentSubmission { get; set; }
        public virtual RedditSubmission AuthorSubmission { get; set; }
        public virtual RedditSubmission SubredditSubmission { get; set; }
        public virtual RedditSubmission LinkSubmission { get; set; }
        public virtual RedditSubmission MediaLinkSubmission { get; set; }
        public virtual RedditSubmission SubmissionDateSubmission { get; set; }

    }
}
