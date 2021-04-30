﻿using BlockBase.Dapps.NeverForget.Business.BusinessModels;
using BlockBase.Dapps.NeverForget.Business.BusinessModels.OperationResults;
using BlockBase.Dapps.NeverForget.Business.Interfaces;
using BlockBase.Dapps.NeverForget.Data.Pocos;
using BlockBase.Dapps.NeverForget.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.Business.BusinessObjects
{
    public class GeneralContextBusinessObject : BaseBusinessObject<GeneralContextPoco>, IGeneralContextBusinessObject
    {
        private readonly IRedditContextPocoDataAccessObject _redditDataAccessObject;
        private readonly ITwitterContextPocoDataAccessObject _twitterDataAccessObject;

        public GeneralContextBusinessObject(IRedditContextPocoDataAccessObject redditDataAccessObject, ITwitterContextPocoDataAccessObject twitterDataAccessObject, IBaseDataAccessObject<GeneralContextPoco> dataAccessObject, IGenericDataAccessObject genericDataAccessObject, ILogger<BaseBusinessObject> logger) : base(dataAccessObject, genericDataAccessObject, logger)
        {
            _redditDataAccessObject = redditDataAccessObject;
            _twitterDataAccessObject = twitterDataAccessObject;
        }

        public async Task<OperationResult<List<GeneralContextPoco>>> GetRecentCalls()
        {
            return await ExecuteOperation(async () =>
            {
                var result = new List<GeneralContextPoco>();
                var recentReddits = await _redditDataAccessObject.GetRecentReddit();
                var recentTweets = await _twitterDataAccessObject.GetRecentTwitter();

                result.AddRange(recentReddits);
                result.AddRange(recentTweets);

                result = result.OrderByDescending(c => c.Date).Take(10).ToList();

                return result;
            });
        }
    }
}
