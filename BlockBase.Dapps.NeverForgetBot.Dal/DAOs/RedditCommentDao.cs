using BlockBase.Dapps.NeverForgetBot.Dal.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Data.Context;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Dal.DAOs
{
    public class RedditCommentDao : IRedditCommentDao
    {
        #region Create
        public async Task InsertAsync(RedditComment entity)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                await context.RedditComment.Insert(entity);
            }
        }
        #endregion

        #region Read
        public async Task<RedditComment> GetAsync(Guid id)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var result = await context.RedditComment.Get(id);
                return result.Result;
            }
        }

        public async Task<RedditComment> GetNonDeletedAsync(Guid id)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var result = await context.RedditComment.Where(e => e.Id == id && e.IsDeleted == false).List();
                if (result.Result == null)
                {
                    return null;
                }
                else
                {
                    return result.Result.ToList().FirstOrDefault();
                }
            }
        }
        #endregion

        #region Update
        public async Task UpdateAsync(RedditComment entity)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                await context.RedditComment.Update(entity);
            }
        }
        #endregion

        #region Delete
        public async Task HardDeleteAsync(RedditComment entity)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                await context.RedditComment.Delete(entity);
            }
        }

        public async Task DeleteAsync(RedditComment entity)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                entity.IsDeleted = true;
                await context.RedditComment.Update(entity);
            }
        }
        #endregion

        #region List
        public async Task<List<RedditComment>> GetAllAsync()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var result = await context.RedditComment.List();
                return result.Result.ToList();
            }
        }

        public async Task<List<RedditComment>> GetAllNonDeletedAsync()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var result = await context.RedditComment.Where(e => e.IsDeleted == false).List();
                if (result.Result == null)
                {
                    return new List<RedditComment>();
                }
                else
                {
                    return result.Result.ToList();
                }
            }
        }

        public async Task<List<RedditComment>> GetAllDeletedAsync()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var result = await context.RedditComment.Where(e => e.IsDeleted == true).List();
                if (result.Result == null)
                {
                    return new List<RedditComment>();
                }
                else
                {
                    return result.Result.ToList();
                }
            }
        }
        #endregion
    }
}
