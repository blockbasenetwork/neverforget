using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System.Collections.Generic;

namespace BlockBase.Dapps.NeverForgetBot.Data.Pocos
{
    public class RedditContextPoco
    {
        public virtual RedditContext Context { get; set; }
        public virtual RedditSubmission Submission { get; set; }
        public virtual List<RedditComment> Comments { get; set; }

        public RedditContextPoco()
        {
            Comments = new List<RedditComment>();
        }
    }
}
