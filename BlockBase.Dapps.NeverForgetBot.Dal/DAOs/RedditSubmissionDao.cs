//using BlockBase.Dapps.NeverForgetBot.Dal.Interfaces;
//using BlockBase.Dapps.NeverForgetBot.Data.Context;
//using BlockBase.Dapps.NeverForgetBot.Data.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace BlockBase.Dapps.NeverForgetBot.Dal.DAOs
//{
//    public class RedditSubmissionDao : IRedditSubmissionDao
//    {
//        #region Create
//        public async Task InsertAsync(RedditSubmission entity)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                await context.RedditSubmission.Insert(entity);
//            }
//        }
//        #endregion

//        #region Read
//        public async Task<RedditSubmission> GetAsync(Guid id)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                var result = await context.RedditSubmission.Get(id);
//                return result.Result;
//            }
//        }

//        public async Task<RedditSubmission> GetNonDeletedAsync(Guid id)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                var result = await context.RedditSubmission.Where(e => e.Id == id && e.IsDeleted == false).List();
//                if (result.Result == null)
//                {
//                    return null;
//                }
//                else
//                {
//                    return result.Result.ToList().FirstOrDefault();
//                }
//            }
//        }
//        #endregion

//        #region Update
//        public async Task UpdateAsync(RedditSubmission entity)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                await context.RedditSubmission.Update(entity);
//            }
//        }
//        #endregion

//        #region Delete
//        public async Task HardDeleteAsync(RedditSubmission entity)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                await context.RedditSubmission.Delete(entity);
//            }
//        }

//        public async Task DeleteAsync(RedditSubmission entity)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                entity.IsDeleted = true;
//                await context.RedditSubmission.Update(entity);
//            }
//        }
//        #endregion

//        #region List
//        public async Task<List<RedditSubmission>> GetAllAsync()
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                var result = await context.RedditSubmission.List();
//                return result.Result.ToList();
//            }
//        }

//        public async Task<List<RedditSubmission>> GetAllNonDeletedAsync()
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                var result = await context.RedditSubmission.Where(e => e.IsDeleted == false).List();
//                if (result.Result == null)
//                {
//                    return new List<RedditSubmission>();
//                }
//                else
//                {
//                    return result.Result.ToList();
//                }
//            }
//        }

//        public async Task<List<RedditSubmission>> GetAllDeletedAsync()
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                var result = await context.RedditSubmission.Where(e => e.IsDeleted == true).List();
//                if (result.Result == null)
//                {
//                    return new List<RedditSubmission>();
//                }
//                else
//                {
//                    return result.Result.ToList();
//                }
//            }
//        }
//        #endregion
//    }
//}
