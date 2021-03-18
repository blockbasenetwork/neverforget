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
    public class TwitterContextDao : BaseAuditDao<TwitterContext>, ITwitterContextDao
    {
        public async Task<List<TweetModel>> GetUniqueComments(TweetModel[] tweetList)
        {
            var resultList = new List<TweetModel>();
            using (var _context = new NeverForgetBotDbContext())
            {
                foreach (var tweet in tweetList)
                {
                    if (tweet.User.Screen_name != "_NeverForgetBot")
                    {
                        var resultComment = await _context.TwitterComment.Where((tCom) => (tCom.CommentId == tweet.Id)).SelectAsync();

                        //var resultComment = await _context.TwitterComment.SelectAsync();

                        if (resultComment.Count() == 0)
                        {
                            resultList.Add(tweet);
                        }
                    }
                }
                return resultList;
            }
        }

        public async Task<bool> IsContextPresent(Guid contextId)
        {
            using (var _context = new NeverForgetBotDbContext())
            {
                var result = await _context.TwitterContext.Where((tCtx) => (tCtx.Id == contextId)).SelectAsync();

                if (result.Count() != 0)
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
                var resultComment = await _context.TwitterComment.Where((tCom) => (tCom.TwitterContextId == contextId)).SelectAsync();

                if (resultComment.Count() != 0)
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
                var resultSubmission = await _context.TwitterSubmission.Where((tSub) => (tSub.TwitterContextId == contextId)).SelectAsync();

                if (resultSubmission.Count() != 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
