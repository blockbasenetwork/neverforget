using BlockBase.Dapps.NeverForgetBot.Business.BusinessModels;
using BlockBase.Dapps.NeverForgetBot.Business.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        //public async Task<OperationResult> FromApiRedditModel(RedditModel[] modelArray)
        //{
        //    foreach (RedditModel model in modelArray)
        //    {
        //        var boModel = new RedditSubmissionBusinessModel();
        //        boModel.Id = Guid.NewGuid();
        //        boModel.Author = model.Author;
        //        boModel.CommentPost = CleanComment(model.Body);
        //        boModel.PostingDate = FromUnixTime(model.Created_Utc);
        //        boModel.CommentId = model.Id;
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


        ////public bool CheckIfExists (RedditSubmissionBusinessModel model)
        ////{
        ////    var modelList = _dao.GetAllNonDeletedAsync().Result;
        ////    modelList.Where(m => m.CommentId == model.CommentId) ? true : false;
        ////}


        //private string CleanComment(string body)
        //{
        //    var unquotedString = Regex.Replace(body, @"\b'\b", "''");
        //    return unquotedString;
        //}
        //#endregion

        #region Create
        public async Task<OperationResult> InsertAsync(RedditSubmissionBusinessModel redditSubmission)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                redditSubmission.CreatedAt = DateTime.UtcNow;
                await _dao.InsertAsync(redditSubmission.ToData());
            });
        }
        #endregion

        #region Read
        public async Task<OperationResult<RedditSubmissionBusinessModel>> GetAsync(Guid id)
        {
            return await _opExecutor.ExecuteOperation<RedditSubmissionBusinessModel>(async () =>
            {
                var result = await _dao.GetNonDeletedAsync(id);
                return RedditSubmissionBusinessModel.FromData(result);
            });
        }
        #endregion
     
        #region Delete
        public async Task<OperationResult> DeleteAsync(RedditSubmissionBusinessModel redditSubmission)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                redditSubmission.IsDeleted = true;
                redditSubmission.DeletedAt = DateTime.UtcNow;
                var redditSubmissionModel = await _dao.GetAsync(redditSubmission.Id);
                await _dao.DeleteAsync(redditSubmissionModel);
            });
        }
        #endregion

        #region List
        public async Task<OperationResult<List<RedditSubmissionBusinessModel>>> GetAllAsync()
        {
            return await _opExecutor.ExecuteOperation<List<RedditSubmissionBusinessModel>>(async () =>
            {
                var result = await _dao.GetAllNonDeletedAsync();
                return result.Select(context => RedditSubmissionBusinessModel.FromData(context)).ToList();
            });
        }
        #endregion
    }
}
