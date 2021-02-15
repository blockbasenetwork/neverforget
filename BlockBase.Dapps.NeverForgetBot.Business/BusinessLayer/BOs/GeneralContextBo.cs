using BlockBase.Dapps.NeverForgetBot.Business.BusinessLayer.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Dal.Queries;

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

        //public async Task<OperationResult<List<GeneralContextPoco>> GetRecentCalls() 
        //{
        ////    return await _opExecutor.ExecuteOperation<List<GeneralContextPoco>>(async () =>
        ////    {
        ////        //_twitterDao.;

        ////});
        //}
    }
}
