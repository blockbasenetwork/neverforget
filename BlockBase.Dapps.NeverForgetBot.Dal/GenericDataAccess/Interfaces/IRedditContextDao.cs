using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;

namespace BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess.Interfaces
{
    public interface IRedditContextDao : IBaseAuditDao<RedditContext, Guid>
    {
    }
}
