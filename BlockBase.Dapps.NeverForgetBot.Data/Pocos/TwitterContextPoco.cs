using BlockBase.Dapps.NeverForgetBot.Data.Entities;

namespace BlockBase.Dapps.NeverForgetBot.Data.Pocos
{
    public class TwitterContextPoco
    {
        public virtual TwitterComment ContentComment { get; set; }
        public virtual TwitterComment AuthorComment { get; set; }
        public virtual TwitterComment MediaLinkComment { get; set; }
        public virtual TwitterComment LinkComment { get; set; }
        public virtual TwitterComment CommentDateComment { get; set; }
        public virtual TwitterSubmission ContentSubmission { get; set; }
        public virtual TwitterSubmission AuthorSubmission { get; set; }
        public virtual TwitterSubmission MediaLinkSubmission { get; set; }
        public virtual TwitterSubmission LinkSubmission { get; set; }
        public virtual TwitterSubmission SubmissionDateSubmission { get; set; }
    }
}