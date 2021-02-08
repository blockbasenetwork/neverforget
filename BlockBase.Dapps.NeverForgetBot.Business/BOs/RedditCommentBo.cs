using BlockBase.Dapps.NeverForgetBot.Business.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Dal.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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


        public async Task<OperationResult> FromApiRedditCommentModel(RedditCommentModel model, Guid id)
        {
            var dataModel = new RedditComment()
            {
                Id = Guid.NewGuid(),
                Author = model.Author,
                Content = CleanComment(model.Body),
                CommentDate = FromUnixTime(model.Created_Utc),
                CommentId = model.Id,
                ParentId = model.Parent_Id,
                ParentSubmissionId = model.Link_Id,
                SubReddit = model.SubReddit,
                CreatedAt = DateTime.UtcNow,
                RedditContextId = id
            };
            await _dao.InsertAsync(dataModel);
            return new OperationResult() { Success = true };
        }

        #region Process Data
        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public DateTime FromUnixTime(int unixTime)
        {
            return epoch.AddSeconds(unixTime);
        }

        private string CleanComment(string body)
        {
            var unquotedString = Regex.Replace(body, @"\b'\b", "''");
            return unquotedString;
        }
        #endregion

        #region Create
        public async Task<OperationResult> InsertAsync(RedditComment redditComment)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                redditComment.CreatedAt = DateTime.UtcNow;
                await _dao.InsertAsync(redditComment);
            });
        }
        #endregion

        #region Read
        public async Task<OperationResult<RedditComment>> GetAsync(Guid id)
        {
            return await _opExecutor.ExecuteOperation<RedditComment>(async () =>
            {
                var result = await _dao.GetNonDeletedAsync(id);
                return result;
            });
        }
        #endregion

        #region Delete
        public async Task<OperationResult> DeleteAsync(RedditComment redditComment)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                redditComment.IsDeleted = true;
                redditComment.DeletedAt = DateTime.UtcNow;
                var commentDelete = await _dao.GetAsync(redditComment.Id);
                await _dao.DeleteAsync(commentDelete);
            });
        }
        #endregion

        #region List
        public async Task<OperationResult<List<RedditComment>>> GetAllAsync()
        {
            return await _opExecutor.ExecuteOperation<List<RedditComment>>(async () =>
            {
                var result = await _dao.GetAllNonDeletedAsync();
                return result.Select(context => context).ToList();
            });
        }
        #endregion
    }
}
