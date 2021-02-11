﻿using BlockBase.Dapps.NeverForgetBot.Data.Context;
using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Dal.Queries
{
    public class GeneralContextPocoDao
    {
        //private readonly NeverForgetBotDbContext _context;
        //public GeneralContextPocoDao(NeverForgetBotDbContext context)
        //{
        //    _context = context;
        //}

        public async Task<GeneralContextPoco> GetRedditContext(Guid contextId)
        {
            var redditContextq = new GeneralContextPoco();

            using (var _context = new NeverForgetBotDbContext())
            {
                var redditContext = await _context.RedditComment.Where(rc => rc.RedditContextId == contextId).List(rc => new GeneralContextPoco()
                {
                    AuthorComment = rc.Author,
                });
            }

            return redditContextq;
        }
    }
}

//dao->where-> .List();
