﻿using BlockBase.Dapps.NeverForget.Data.Entities;
using BlockBase.Dapps.NeverForget.DataAccess.DataAccessModels;
using BlockBase.Dapps.NeverForget.DataAccess.Interfaces;

namespace BlockBase.Dapps.NeverForget.DataAccess.DataAccessObjects
{
    public class RedditSubmissionDao : BaseAuditDao<RedditSubmission>, IRedditSubmissionDao
    {
    }
}