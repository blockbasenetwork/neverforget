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
            /*var redditContext =  (from ctx in _context.RedditContext
                                  where ctx.Id == contextId
                                  select new GeneralContextPoco
                                  {
                                      
                                  }*/
            var _context = new NeverForgetBotDbContext();

            var redditContext = from comment in _context.RedditComment
                                where comment.RedditContextId == contextId
                                select new GeneralContextPoco
                                {
                                    AuthorComment = comment.c
                                };

            return redditContext;
        }
    }
}
