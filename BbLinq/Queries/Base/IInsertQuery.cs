using System;

namespace BlockBase.BBLinq.Queries.Base
{
    public interface IInsertQuery : IQuery
    {
        public Type EntityType {get;}
        public object[] Records { get; }
    }
}
