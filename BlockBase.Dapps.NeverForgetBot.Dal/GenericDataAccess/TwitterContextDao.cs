using BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Data.Context;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess
{
    public class TwitterContextDao : BaseAuditDao<TwitterContext, Guid>, ITwitterContextDao
    {
        public async Task<List<TweetModel>> GetUniqueComments(TweetModel[] tweetList)
        {
            var resultList = new List<TweetModel>();
            using (var _context = new NeverForgetBotDbContext())
            {
                foreach (var tweet in tweetList)
                {
                    var result = await _context.TwitterComment.Where((tCom) => (tCom.CommentId == tweet.Id)).List();

                    if (result.Result.Count() == 0)
                    {
                        resultList.Add(tweet);
                    }
                }
            }
            return resultList;
        }
    }
}
