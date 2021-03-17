using System;
using BlockBase.BBLinq.Model.Base;

namespace BlockBase.BBLinq.Queries.Base
{
    public interface IUpdateQuery
    {
        public Type EntityType { get; }
        public object Record { get; }
        public ExpressionNode Condition { get; }
    }
}
