using BlockBase.Dapps.NeverForgetBot.Business.BusinessModels;
using BlockBase.Dapps.NeverForgetBot.Business.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.BOs
{
    public class RedditCommentBo : IRedditCommentBo
    {
        private readonly IRedditCommentDao _dao;
        private readonly IDbOperationExecutor _opExecutor;

        public RedditCommentBo(IRedditCommentDao dao, IDbOperationExecutor opExecutor)
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
        public async Task<OperationResult> InsertAsync(RedditCommentBusinessModel redditComment)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                redditComment.CreatedAt = DateTime.UtcNow;
                await _dao.InsertAsync(redditComment.ToData());
            });
        }
        #endregion

        #region Read
        public async Task<OperationResult<RedditCommentBusinessModel>> GetAsync(Guid id)
        {
            return await _opExecutor.ExecuteOperation<RedditCommentBusinessModel>(async () =>
            {
                var result = await _dao.GetNonDeletedAsync(id);
                return RedditCommentBusinessModel.FromData(result);
            });
        }
        #endregion

        #region Delete
        public async Task<OperationResult> DeleteAsync(RedditCommentBusinessModel redditComment)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                redditComment.IsDeleted = true;
                redditComment.DeletedAt = DateTime.UtcNow;
                var redditCommentModel = await _dao.GetAsync(redditComment.Id);
                await _dao.DeleteAsync(redditCommentModel);
            });
        }
        #endregion

        #region List
        public async Task<OperationResult<List<RedditCommentBusinessModel>>> GetAllAsync()
        {
            return await _opExecutor.ExecuteOperation<List<RedditCommentBusinessModel>>(async () =>
            {
                var result = await _dao.GetAllNonDeletedAsync();
                return result.Select(context => RedditCommentBusinessModel.FromData(context)).ToList();
            });
        }
        #endregion
    }
}
