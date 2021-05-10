using BlockBase.Dapps.NeverForget.Business.BusinessModels;
using BlockBase.Dapps.NeverForget.Business.Interfaces;
using BlockBase.Dapps.NeverForget.Data.Entities;
using BlockBase.Dapps.NeverForget.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;

namespace BlockBase.Dapps.NeverForget.Business.BusinessObjects
{
    public class TwitterSubmissionBusinessObject : BaseAuditBusinessObject<TwitterSubmission>, ITwitterSubmissionBusinessObject
    {
        public TwitterSubmissionBusinessObject(ITwitterSubmissionDataAccessObject dataAccessObject, ILogger<BaseBusinessObject> logger) : base(dataAccessObject, logger)
        {
        }
    }
}
