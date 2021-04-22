using BlockBase.Dapps.NeverForget.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.NeverForget.Data.Pocos
{
    public class TwitterContextPoco
    {
        public virtual TwitterContext Context { get; set; }
        public virtual List<TwitterComment> Comments { get; set; }
        public virtual TwitterSubmission Submission { get; set; }

        public TwitterContextPoco()
        {
            Comments = new List<TwitterComment>();
        }
    }
}
