using System;

namespace BlockBase.Dapps.NeverForgetBot.Data.Pocos
{
    public class GeneralContextPoco
    {
        public virtual string ContentComment { get; set; }
        public virtual string AuthorComment { get; set; }
        public virtual string? MediaLinkComment { get; set; }
        public virtual string LinkComment { get; set; }
        public virtual string? SubRedditComment { get; set; }

        public virtual DateTime CommentDateComment { get; set; }
        public virtual string? TitleSubmission { get; set; }
        public virtual string ContentSubmission { get; set; }
        public virtual string AuthorSubmission { get; set; }
        public virtual string MediaLinkSubmission { get; set; }
        public virtual string LinkSubmission { get; set; }
        public virtual DateTime SubmissionDateSubmission { get; set; }

        //public virtual SourceType SourceType { get; set; }
    }
}
