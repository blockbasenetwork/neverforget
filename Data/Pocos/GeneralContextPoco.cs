using BlockBase.Dapps.NeverForget.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.NeverForget.Data.Pocos
{
    public class GeneralContextPoco
    {
        public Guid Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual string Author { get; set; }
        public virtual string SubReddit { get; set; }
        public virtual string Link { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual SourceTypeEnum SourceType { get; set; }
    }
}
