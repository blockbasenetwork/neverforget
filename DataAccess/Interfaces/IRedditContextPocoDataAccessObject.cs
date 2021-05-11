using BlockBase.Dapps.NeverForget.Data.Pocos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.DataAccess.Interfaces
{
    public interface IRedditContextPocoDataAccessObject
    {
        Task<List<RedditContextPoco>> GetRedditContextById(Guid contextId);
        Task<List<RedditContextPoco>> GetAllRedditContexts();
        Task<List<GeneralContextPoco>> GetRecentReddit();
    }
}
