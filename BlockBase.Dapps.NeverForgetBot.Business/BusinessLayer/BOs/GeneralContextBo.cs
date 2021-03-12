using BlockBase.Dapps.NeverForgetBot.Business.BusinessLayer.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Common.Enums;
using BlockBase.Dapps.NeverForgetBot.Dal.Queries;
using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.BusinessLayer.BOs
{
    public class GeneralContextBo : IGeneralContextBo
    {
        private readonly IRedditContextPocoDao _redditDao;
        private readonly ITwitterContextPocoDao _twitterDao;
        private readonly IDbOperationExecutor _opExecutor;

        public GeneralContextBo(IRedditContextPocoDao redditDao, ITwitterContextPocoDao twitterDao, IDbOperationExecutor opExecutor)
        {
            _redditDao = redditDao;
            _twitterDao = twitterDao;
            _opExecutor = opExecutor;
        }

        public async Task<OperationResult<List<GeneralContextPoco>>> GetRecentCalls()
        {
            return await _opExecutor.ExecuteOperation<List<GeneralContextPoco>>(async () =>
            {
                var result = new List<GeneralContextPoco>();
                var recentReddits = await _redditDao.GetRecentRedditContexts();
                var recentTweets = await _twitterDao.GetRecentTwitterContexts();

                result.AddRange(recentReddits);
                result.AddRange(recentTweets);

                result = result.OrderByDescending(c => c.Date).Take(10).ToList();

                return result;
            });
        }
    }
}
