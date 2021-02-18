using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System.Collections.Generic;

namespace BlockBase.Dapps.NeverForgetBot.Data.Pocos
{
    public class TwitterContextPoco
    {
        public virtual TwitterContext Context { get; set; }
        public virtual List<TwitterComment> Comments { get; set; }
        public virtual TwitterSubmission Submission { get; set; }
    }
}