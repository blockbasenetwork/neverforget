﻿using BlockBase.BBLinq.Enumerables;
using BlockBase.Dapps.NeverForget.Common.Enums;
using BlockBase.Dapps.NeverForget.Data.Context;
using BlockBase.Dapps.NeverForget.Data.Entities;
using BlockBase.Dapps.NeverForget.Data.Pocos;
using BlockBase.Dapps.NeverForget.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.DataAccess.DataAccessObjects
{
    public class RedditContextPocoDataAccessObject : IRedditContextPocoDataAccessObject
    {
        public async Task<RedditContextPoco> GetRedditContextById(Guid contextId)
        {
            RedditContextPoco result = new RedditContextPoco();
            using (var _context = new NeverForgetBotDbContext())
            {
                var retrievedContext = await _context.RedditContext.Where((rCtx) => (rCtx.Id == contextId) && (rCtx.IsDeleted == false)).SelectAsync();

                if (retrievedContext.Count() != 0)
                {
                    var retrievedComments = await _context.RedditComment.Where((rCom) => (rCom.RedditContextId == contextId)).SelectAsync();
                    var retrievedSubmission = await _context.RedditSubmission.Where((rSub) => (rSub.RedditContextId == contextId)).SelectAsync();
                    foreach (var item in retrievedContext)
                    {
                        result.Context = item;
                    }
                    foreach (var item in retrievedSubmission)
                    {
                        result.Submission = item;
                    }
                    foreach (var item in retrievedComments)
                    {
                        result.Comments.Add(item);
                    }
                }
                return result;
            }
        }

        public async Task<List<RedditContextPoco>> GetAllRedditContexts()
        {
            List<RedditContextPoco> result = new List<RedditContextPoco>();
            using (var _context = new NeverForgetBotDbContext())
            {
                var retrievedContextIds = await _context.RedditContext.Where(rCtx => rCtx.IsDeleted == false).SelectAsync((rCtx) => new RedditContext() { Id = rCtx.Id });

                foreach (var context in retrievedContextIds)
                {
                    result.Add(GetRedditContextById(context.Id).Result);
                }
            }
            return result;
        }

        public async Task<List<GeneralContextPoco>> GetRecentReddit()
        {
            using (var _context = new NeverForgetBotDbContext())
            {
                var recentReddits = await _context.RedditContext.Join<RedditSubmission>(BlockBaseJoinEnum.Left)
                     .Where((redditContext, redditSubmission) => (redditContext.Id == redditSubmission.RedditContextId) && (!redditContext.IsDeleted))
                     .SelectAsync((redditContext, redditSubmission) => new GeneralContextPoco
                     {
                         SourceType = SourceTypeEnum.Reddit,
                         Date = redditSubmission.SubmissionDate,
                         Author = redditSubmission.Author,
                         Title = redditSubmission.Title,
                         Content = redditSubmission.Content
                     });

                return recentReddits.OrderByDescending((reddit) => reddit.Date).Take(10).ToList();
            }
        }

    }
}