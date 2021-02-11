using BlockBase.Dapps.NeverForgetBot.Data.Context;
using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using System;
using System.Linq;

namespace BlockBase.Dapps.NeverForgetBot.Dal.Queries
{
    public class GeneralContextPocoDao
    {
        //private readonly NeverForgetBotDbContext _context;
        //public GeneralContextPocoDao(NeverForgetBotDbContext context)
        //{
        //    _context = context;
        //}

        public GeneralContextPoco GetRedditContext(Guid contextId)
        {
            var redditContext = new GeneralContextPoco();

            using (var _context = new NeverForgetBotDbContext())
            {
                redditContext = _context.RedditComment.Where(rc => rc.RedditContextId == contextId).List(rc => new GeneralContextPoco()
                {
                    AuthorComment = rc.Author,
                });
            }

            return redditContext;
        }
    }
}

//dao->where-> .List();
