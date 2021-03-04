using BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Data.Context;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    var resultComment = await _context.TwitterComment.Where((tCom) => (tCom.CommentId == tweet.Id)).List();

                    if (resultComment.Result.Count() == 0)
                    {
                        resultList.Add(tweet);
                    }
                }
            }
            return resultList;
        }

        public async Task<bool> IsContextPresent(Guid contextId)
        {
            using (var _context = new NeverForgetBotDbContext())
            {
                var result = await _context.TwitterContext.Where((tCtx) => (tCtx.Id == contextId)).List();

                if (result.Result != null)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> IsCommentPresent(Guid contextId)
        {
            using (var _context = new NeverForgetBotDbContext())
            {
                var resultComment = await _context.TwitterComment.Where((tCom) => (tCom.TwitterContextId == contextId)).List();

                if (resultComment.Result != null)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> IsSubmissionPresent(Guid contextId)
        {
            using (var _context = new NeverForgetBotDbContext())
            {
                var resultSubmission = await _context.TwitterSubmission.Where((tSub) => (tSub.TwitterContextId == contextId)).List();

                if (resultSubmission.Result != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
