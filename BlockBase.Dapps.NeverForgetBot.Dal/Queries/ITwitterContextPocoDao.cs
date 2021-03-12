using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Dal.Queries
{
    public interface ITwitterContextPocoDao
    {
        Task<TwitterContextPoco> GetTwitterContextById(Guid contextId);
        Task<List<TwitterContextPoco>> GetAllTwitterContexts();
        Task<GeneralContextPoco> GetRecentTwitterContextById(TwitterContext context);
        Task<List<GeneralContextPoco>> GetRecentTwitterContexts();
    }
}