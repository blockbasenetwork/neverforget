using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess.Interfaces
{
    public interface ITwitterSubmissionDao : IBaseAuditDao<TwitterSubmission, Guid>
    {
    }
}
