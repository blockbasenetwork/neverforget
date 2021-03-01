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
    }
}
