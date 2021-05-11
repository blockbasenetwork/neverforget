﻿using System;

namespace BlockBase.Dapps.NeverForget.Data.Pocos
{
    public class TwitterContextPoco
    {
        public virtual Guid ContextId { get; set; }
        public virtual DateTime ContextCreatedAt { get; set; }
        public virtual string CommentContent { get; set; }
        public virtual string CommentAuthor { get; set; }
        public virtual string CommentLink { get; set; }
        public virtual string? CommentMediaLink { get; set; }
        public virtual DateTime CommentDate { get; set; }
        public virtual string SubmissionContent { get; set; }
        public virtual string SubmissionAuthor { get; set; }
        public virtual string SubmissionLink { get; set; }
        public virtual string? SubmissionMediaLink { get; set; }
        public virtual DateTime SubmissionDate { get; set; }
    }
}
