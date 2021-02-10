using BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess
{
    public class TwitterSubmissionDao : BaseAuditDao<TwitterSubmission, Guid>, ITwitterSubmissionDao
    {
    }
}
