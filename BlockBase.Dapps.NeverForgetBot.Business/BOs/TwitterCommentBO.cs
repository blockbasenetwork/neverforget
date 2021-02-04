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
    public class TwitterCommentBO : ITwitterCommentBo
    {
        private readonly ITwitterCommentDao _dao;
        private readonly IDbOperationExecutor _opExecutor;

        public TwitterCommentBO(ITwitterCommentDao dao, IDbOperationExecutor opExecutor)
        {
            _dao = dao;
            _opExecutor = opExecutor;
        }

        #region Create
        public async Task<OperationResult> InsertAsync(TwitterCommentBusinessModel twitterComment)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                twitterComment.CreatedAt = DateTime.UtcNow;
                await _dao.InsertAsync(twitterComment.ToData());
            });
        }
        #endregion

        #region Read
        public async Task<OperationResult<TwitterCommentBusinessModel>> GetAsync(Guid id)
        {
            return await _opExecutor.ExecuteOperation<TwitterCommentBusinessModel>(async () =>
            {
                var result = await _dao.GetNonDeletedAsync(id);
                return TwitterCommentBusinessModel.FromData(result);
            });
        }
        #endregion

        #region Update
        public async Task<OperationResult> UpdateAsync(TwitterCommentBusinessModel twitterComment)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                await _dao.UpdateAsync(twitterComment.ToData());
            });
        }
        #endregion

        #region Delete
        public async Task<OperationResult> DeleteAsync(TwitterCommentBusinessModel twitterComment)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                twitterComment.IsDeleted = true;
                twitterComment.DeletedAt = DateTime.UtcNow;
                var twitterCommentModel = await _dao.GetAsync(twitterComment.Id);
                await _dao.DeleteAsync(twitterCommentModel);
            });
        }
        #endregion

        #region List
        public async Task<OperationResult<List<TwitterCommentBusinessModel>>> GetAllAsync()
        {
            return await _opExecutor.ExecuteOperation<List<TwitterCommentBusinessModel>>(async () =>
            {
                var result = await _dao.GetAllNonDeletedAsync();
                return result.Select(comment => TwitterCommentBusinessModel.FromData(comment)).ToList();
            });
        }
        #endregion
    }
}
