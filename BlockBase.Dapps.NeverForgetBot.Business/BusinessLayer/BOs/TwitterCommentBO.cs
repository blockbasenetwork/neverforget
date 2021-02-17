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
    public class TwitterCommentBo : ITwitterCommentBo
    {
        private readonly ITwitterCommentDao _dao;
        private readonly IDbOperationExecutor _opExecutor;

        public TwitterCommentBo(ITwitterCommentDao dao, IDbOperationExecutor opExecutor)
        {
            _dao = dao;
            _opExecutor = opExecutor;
        }

        #region Create
        public async Task<OperationResult> InsertAsync(TwitterComment twitterComment)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                twitterComment.CreatedAt = DateTime.UtcNow;
                await _dao.InsertAsync(twitterComment);
            });
        }
        #endregion

        #region Read
        public async Task<OperationResult<TwitterComment>> GetAsync(Guid id)
        {
            return await _opExecutor.ExecuteOperation<TwitterComment>(async () =>
            {
                var result = await _dao.GetAsync(id);
                return result;
            });
        }
        #endregion

        #region Delete
        public async Task<OperationResult> DeleteAsync(TwitterComment twitterComment)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                twitterComment.IsDeleted = true;
                twitterComment.DeletedAt = DateTime.UtcNow;
                var commentDelete = await _dao.GetAsync(twitterComment.Id);
                await _dao.DeleteAsync(commentDelete);
            });
        }
        #endregion

        #region List
        public async Task<OperationResult<List<TwitterComment>>> GetAllAsync()
        {
            return await _opExecutor.ExecuteOperation<List<TwitterComment>>(async () =>
            {
                var result = await _dao.GetAllAsync();
                return result.Select(comment => comment).ToList();
            });
        }
        #endregion
    }
}
