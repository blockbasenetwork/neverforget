using BlockBase.Dapps.NeverForgetBot.Data.Entities;

namespace BlockBase.Dapps.NeverForgetBot.Data.Pocos
{
    public class GeneralContextPoco
    {
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

        //public virtual TwitterComment ContentComment { get; set; }
        //public virtual TwitterComment AuthorComment { get; set; }
        //public virtual TwitterComment LinkComment { get; set; }
        //public virtual TwitterComment MediaComment { get; set; }
        //public virtual TwitterComment CommentDateComment { get; set; }

        //public virtual TwitterSubmission ContentSubmission { get; set; }
        //public virtual TwitterSubmission AuthorSubmission { get; set; }
        //public virtual TwitterSubmission LinkSubmission { get; set; }
        //public virtual TwitterSubmission MediaLinkSubmission { get; set; }
        //public virtual TwitterSubmission SubmissionDateSubmission { get; set; }




    }
}
