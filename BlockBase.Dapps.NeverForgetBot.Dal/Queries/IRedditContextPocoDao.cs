﻿using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Dal.Queries
{
    public interface IRedditContextPocoDao
    {
        Task<RedditContextPoco> GetRedditContextById(Guid contextId);
        Task<List<RedditContextPoco>> GetAllRedditContexts();
        Task<List<GeneralContextPoco>> GetRecentReddit();
    }
}