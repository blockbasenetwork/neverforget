using System;
namespace BlockBase.BBLinq.Queries.Base
{
    public interface ICreateDatabaseQuery : IQuery
    {
        public Type[] EntityTypes { get; }
        public string DatabaseName { get; }
    }
}
