﻿using BlockBase.Dapps.NeverForget.Business.BusinessModels;
using BlockBase.Dapps.NeverForget.Business.Interfaces;
using BlockBase.Dapps.NeverForget.Data.Entities;
using BlockBase.Dapps.NeverForget.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;

namespace BlockBase.Dapps.NeverForget.Business.BusinessObjects
{
    public class RedditSubmissionBusinessObject : BaseBusinessObject<RedditSubmission>, IRedditSubmissionBusinessObject
    {
        public RedditSubmissionBusinessObject(IBaseDataAccessObject<RedditSubmission> dataAccessObject, IGenericDataAccessObject genericDataAccessObject, ILogger<BaseBusinessObject> logger) : base(dataAccessObject, genericDataAccessObject, logger)
        {
        }
    }
}
