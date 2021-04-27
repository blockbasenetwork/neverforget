using BlockBase.Dapps.NeverForget.Data.Pocos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.DataAccess.Interfaces
{
    public interface ITwitterContextPocoDao
    {
        Task<TwitterContextPoco> GetTwitterContextById(Guid contextId);
        Task<List<TwitterContextPoco>> GetAllTwitterContexts();
        Task<List<GeneralContextPoco>> GetRecentTwitter();
    }
}