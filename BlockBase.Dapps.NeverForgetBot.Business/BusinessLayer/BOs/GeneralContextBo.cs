using BlockBase.Dapps.NeverForgetBot.Business.BusinessLayer.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Business.Pocos;
using BlockBase.Dapps.NeverForgetBot.Common.Enums;
using BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Dal.Queries;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using System;
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



        //#region Create
        //public async Task<OperationResult> InsertAsync(RedditComment redditComment)
        //{
        //    return await _opExecutor.ExecuteOperation(async () =>
        //    {
        //        redditComment.CreatedAt = DateTime.UtcNow;
        //        await _dao.InsertAsync(redditComment);
        //    });
        //}
        //#endregion
    }
}
