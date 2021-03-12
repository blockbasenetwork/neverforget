using BlockBase.Dapps.NeverForgetBot.Common.Enums;
using System;

namespace BlockBase.Dapps.NeverForgetBot.Data.Pocos
{
    public class GeneralContextPoco
    {
        public virtual string? Title { get; set; }
        public virtual string Content { get; set; }
        public virtual string Author { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual SourceTypeEnum SourceType { get; set; }
    }
}
