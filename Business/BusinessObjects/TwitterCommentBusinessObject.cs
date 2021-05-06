﻿using BlockBase.Dapps.NeverForget.Business.BusinessModels;
using BlockBase.Dapps.NeverForget.Business.Interfaces;
using BlockBase.Dapps.NeverForget.Data.Entities;
using BlockBase.Dapps.NeverForget.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;

namespace BlockBase.Dapps.NeverForget.Business.BusinessObjects
{
    public class TwitterCommentBusinessObject : BaseAuditBusinessObject<TwitterComment>, ITwitterCommentBusinessObject
    {
        public TwitterCommentBusinessObject(ITwitterCommentDataAccessObject dataAccessObject, IGenericDataAccessObject genericDataAccessObject, ILogger<BaseBusinessObject> logger) : base(dataAccessObject, genericDataAccessObject, logger)
        {
        }
    }
}