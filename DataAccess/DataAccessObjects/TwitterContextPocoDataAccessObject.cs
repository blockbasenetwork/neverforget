using BlockBase.Dapps.NeverForget.Common.Enums;
using BlockBase.Dapps.NeverForget.Data.Context;
using BlockBase.Dapps.NeverForget.Data.Entities;
using BlockBase.Dapps.NeverForget.Data.Pocos;
using BlockBase.Dapps.NeverForget.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.DataAccess.DataAccessObjects
{
    public class TwitterContextPocoDataAccessObject : ITwitterContextPocoDataAccessObject
    {
        public async Task<TwitterContextPoco> GetTwitterContextById(Guid contextId)
        {
            TwitterContextPoco result = new TwitterContextPoco();

            using (var _context = new NeverForgetBotDbContext())
            {
                var retrievedContext = await _context.TwitterContext.Where((tCtx) => (tCtx.Id == contextId) && (tCtx.IsDeleted == false)).SelectAsync();

                var retrievedComments = await _context.TwitterComment.Where((tCom) => (tCom.TwitterContextId == contextId)).SelectAsync();
                var retrievedSubmission = await _context.TwitterSubmission.Where((tSub) => (tSub.TwitterContextId == contextId)).SelectAsync();

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

                return result;
            }
        }

        public async Task<List<TwitterContextPoco>> GetAllTwitterContexts()
        {
            List<TwitterContextPoco> result = new List<TwitterContextPoco>();
            using (var _context = new NeverForgetBotDbContext())
            {
                var retrievedContextIds = await _context.TwitterContext.Where(tCtx => tCtx.IsDeleted == false).SelectAsync((tCtx) => new TwitterContext() { Id = tCtx.Id });

                foreach (var context in retrievedContextIds)
                {
                    result.Add(GetTwitterContextById(context.Id).Result);
                }
            }
            return result;
        }

        public async Task<List<GeneralContextPoco>> GetRecentTwitter()
        {
            List<GeneralContextPoco> result = new List<GeneralContextPoco>();

            using (var _context = new NeverForgetBotDbContext())
            {
                var retrievedSubmissions = await _context.TwitterSubmission.Where((tSub) => !tSub.IsDeleted).Take(10).SelectAsync((tSub) => new GeneralContextPoco
                {
                    Author = tSub.Author,
                    Content = tSub.Content,
                    Date = tSub.SubmissionDate,
                    SourceType = SourceTypeEnum.Twitter
                });

                foreach (var submission in retrievedSubmissions)
                {
                    result.Add(submission);
                }
            }

            return result;
        }
    }
}