using BlockBase.Dapps.NeverForget.Business.BusinessModels;
using BlockBase.Dapps.NeverForget.Business.BusinessModels.OperationResults;
using BlockBase.Dapps.NeverForget.Business.Interfaces;
using BlockBase.Dapps.NeverForget.Data.Entities;
using BlockBase.Dapps.NeverForget.Data.Pocos;
using BlockBase.Dapps.NeverForget.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.Business.BusinessObjects
{
    public class GeneralContextBusinessObject : BaseBusinessObject<GeneralContextPoco>//, IGeneralContextBusinessObject
    {
        private readonly IBaseDataAccessObject<RedditContext> _redditDataAccessObject;
        private readonly IBaseDataAccessObject<TwitterContext> _twitterDataAccessObject;
        private readonly IBaseBusinessObject<GeneralContextPoco> _businessObject;

        public GeneralContextBusinessObject(IBaseDataAccessObject<GeneralContextPoco> dataAccessObject, IGenericDataAccessObject genericDataAccessObject, ILogger<BaseBusinessObject> logger) : base(dataAccessObject, genericDataAccessObject, logger)
        {
        }

        //public async Task<OperationResult<List<GeneralContextPoco>>> GetRecentCalls()
        //{
        //    return await _businessObject.ExecuteOperation<List<GeneralContextPoco>>(async () =>
        //    {
        //        var result = new List<GeneralContextPoco>();
        //        var recentReddits = await _redditDataAccessObject.GetRecentReddit();
        //        var recentTweets = await _twitterDataAccessObject.GetRecentTwitter();

        //        result.AddRange(recentReddits);
        //        result.AddRange(recentTweets);

        //        result = result.OrderByDescending(c => c.Date).Take(10).ToList();

        //        return result;
        //    });
        //}
    }
}
