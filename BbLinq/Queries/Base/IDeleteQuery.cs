using System;
using BlockBase.BBLinq.Model.Base;

namespace BlockBase.BBLinq.Queries.Base
{
    public interface IDeleteQuery : IQuery
    {
        public Type EntityType {get;}
        public ExpressionNode Condition {get;}
    }
}
