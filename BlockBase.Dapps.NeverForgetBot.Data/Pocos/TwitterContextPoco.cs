using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;
using System.Collections.Generic;

namespace BlockBase.Dapps.NeverForgetBot.Data.Pocos
{
    public class TwitterContextPoco
    {
        //public Guid TwitterContextId { get; set; }
        //public int RequestTypeId { get; set; }


        //public Guid TwitterCommentId { get; set; }
        //public string TwitterCommentCommentId { get; set; }
        //public string TwitterCommentReplyToId { get; set; }
        //public string TwitterCommentContent { get; set; }
        //public string TwitterCommentAuthor { get; set; }
        //public string TwitterCommentMediaLink { get; set; }
        //public string TwitterCommentLink { get; set; }
        //public DateTime TwitterCommentCommentDate { get; set; }
        //public Guid TwitterContextId { get; set; }
        //public virtual TwitterContext TwitterContext { get; set; }

        public virtual TwitterContext Context { get; set; }
        public virtual List<TwitterComment> Comments { get; set; }
        public virtual TwitterSubmission Submission { get; set; }
    }
}