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
    public class RedditContextDao : BaseAuditDao<RedditContext, Guid>, IRedditContextDao
    {
        public async Task<List<RedditCommentModel>> GetUniqueComments(RedditCommentModel[] commentArray)
        {
            var resultList = new List<RedditCommentModel>();
            using (var _context = new NeverForgetBotDbContext())
            {
                foreach (var comment in commentArray)
                {
                    var result = await _context.RedditComment.Where(c => c.CommentId == comment.Id).List();

                    if (result.Result.Count() == 0)
                    {
                        resultList.Add(comment);
                    }
                }
            }
            return resultList;
        }


        public async Task<bool> IsContextPresent(Guid contextId)
        {
            using (var _context = new NeverForgetBotDbContext())
            {
                var result = await _context.RedditContext.Where((rCtx) => (rCtx.Id == contextId)).List();

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
                var resultComment = await _context.RedditComment.Where((rCom) => (rCom.RedditContextId == contextId)).List();

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
                var resultSubmission = await _context.RedditSubmission.Where((rSub) => (rSub.RedditContextId == contextId)).List();

                if (resultSubmission.Result != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
