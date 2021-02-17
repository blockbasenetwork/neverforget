using BlockBase.Dapps.NeverForgetBot.Business.BusinessLayer.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.BusinessLayer.BOs
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
                var result = await _dao.GetAsync(id);
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
                var result = await _dao.GetAllAsync();
                return result.Select(context => context).ToList();
            });
        }
        #endregion
    }
}
