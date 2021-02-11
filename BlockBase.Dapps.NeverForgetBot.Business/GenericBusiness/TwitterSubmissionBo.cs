using BlockBase.Dapps.NeverForgetBot.Business.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;

namespace BlockBase.Dapps.NeverForgetBot.Business.GenericBusiness
{
    public class TwitterSubmissionBo : BaseBo<TwitterSubmission>, ITwitterSubmissionBo
    {
        public TwitterSubmissionBo(IBaseAuditDao<TwitterSubmission, Guid> dao, IDbOperationExecutor opExecutor) : base(dao, opExecutor)
        {
        }
    }
}