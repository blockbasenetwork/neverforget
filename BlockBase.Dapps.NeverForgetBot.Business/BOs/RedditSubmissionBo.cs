using BlockBase.Dapps.NeverForgetBot.Business.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Dal.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
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

        //public async Task<OperationResult> FromApiRedditSubmissionModel(RedditSubmissionModel[] modelArray)
        //{
        //    foreach (RedditSubmissionModel model in modelArray)
        //    {
        //        var boModel = new RedditSubmissionBusinessModel();
        //        boModel.Id = Guid.NewGuid();
        //        boModel.Author = model.Author;
        //        boModel.Content = CleanComment(model.Body);
        //        boModel.SubmissionDate = FromUnixTime(model.Created_Utc);
        //        boModel.SubmissionId = model.Id;
        //        boModel.SubReddit = model.SubReddit;
        //        boModel.CreatedAt = DateTime.UtcNow;

        //        await _dao.InsertAsync(boModel.ToData());
        //    }
        //    return new OperationResult() { Success = true };
        //}

        //#region Process Data
        //private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        //public DateTime FromUnixTime(int unixTime)
        //{
        //    return epoch.AddSeconds(unixTime);
        //}

        //private string CleanComment(string body)
        //{
        //    var unquotedString = Regex.Replace(body, @"\b'\b", "''");
        //    return unquotedString;
        //}
        //#endregion

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
