using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.NeverForgetBot.Data.Pocos
{
    public class RedditJoinResult
    {
        public virtual RedditContext Context { get; set; }
        public virtual RedditComment Comment { get; set; }
        public virtual RedditSubmission Submission { get; set; }
    }
}
