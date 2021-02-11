using BlockBase.Dapps.NeverForgetBot.Business.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.NeverForgetBot.Business.GenericBusiness
{
    public class RedditContextBo : BaseBo<RedditContext>, IRedditContextBo
    {
        public RedditContextBo(IBaseAuditDao<RedditContext, Guid> dao, IDbOperationExecutor opExecutor) : base(dao, opExecutor)
        {
        }
    }
}
