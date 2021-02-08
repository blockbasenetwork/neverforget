using BlockBase.Dapps.NeverForgetBot.Business.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Common;
using BlockBase.Dapps.NeverForgetBot.Dal.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.BOs
{
    public class RedditSubmissionBo : IRedditSubmissionBo
    {
        private readonly IRedditSubmissionDao _dao;
        private readonly IDbOperationExecutor _opExecutor;

        public RedditSubmissionBo(IRedditSubmissionDao dao, IDbOperationExecutor opExecutor)
        {
            _dao = dao;
            _opExecutor = opExecutor;

        }

        public async Task<OperationResult> FromApiRedditSubmissionModel(RedditSubmissionModel model, Guid id)
        {
            var dataModel = new RedditSubmission()
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Author = model.Author,
                Content = Helpers.CleanComment(model.SelfText),
                SubmissionDate = Helpers.FromUnixTime(model.Created_Utc),
                SubmissionId = model.Id,
                SubReddit = model.SubReddit,
                CreatedAt = DateTime.UtcNow,
                RedditContextId = id
            };
            await _dao.InsertAsync(dataModel);

            return new OperationResult() { Success = true };
        }

        #region Create
        public async Task<OperationResult> InsertAsync(RedditSubmission redditSubmission)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                redditSubmission.CreatedAt = DateTime.UtcNow;
                await _dao.InsertAsync(redditSubmission);
            });
        }
        #endregion

        #region Read
        public async Task<OperationResult<RedditSubmission>> GetAsync(Guid id)
        {
            return await _opExecutor.ExecuteOperation<RedditSubmission>(async () =>
            {
                var result = await _dao.GetNonDeletedAsync(id);
                return result;
            });
        }
        #endregion

        #region Delete
        public async Task<OperationResult> DeleteAsync(RedditSubmission redditSubmission)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                redditSubmission.IsDeleted = true;
                redditSubmission.DeletedAt = DateTime.UtcNow;
                var submissionDelete = await _dao.GetAsync(redditSubmission.Id);
                await _dao.DeleteAsync(submissionDelete);
            });
        }
        #endregion

        #region List
        public async Task<OperationResult<List<RedditSubmission>>> GetAllAsync()
        {
            return await _opExecutor.ExecuteOperation<List<RedditSubmission>>(async () =>
            {
                var result = await _dao.GetAllNonDeletedAsync();
                return result.Select(context => context).ToList();
            });
        }
        #endregion
    }
}
