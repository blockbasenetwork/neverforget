using BlockBase.Dapps.NeverForget.Data.Entities;
using System.Collections.Generic;

namespace BlockBase.Dapps.NeverForget.Data.Pocos
{
    public class RedditContextPoco
    {
        public virtual RedditContextPoco Context { get; set; }
        public virtual RedditSubmission Submission { get; set; }
        public virtual List<RedditComment> Comments { get; set; }

        public RedditContextPoco()
        {
            Comments = new List<RedditComment>();
        }
    }
}
