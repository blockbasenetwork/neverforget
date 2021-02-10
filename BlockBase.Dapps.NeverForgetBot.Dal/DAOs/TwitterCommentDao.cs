//using BlockBase.Dapps.NeverForgetBot.Dal.Interfaces;
//using BlockBase.Dapps.NeverForgetBot.Data.Context;
//using BlockBase.Dapps.NeverForgetBot.Data.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BlockBase.Dapps.NeverForgetBot.Dal.DAOs
//{
//    public class TwitterCommentDao : ITwitterCommentDao
//    {
//        #region Create
//        public async Task InsertAsync(TwitterComment entity)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                await context.TwitterComment.Insert(entity);
//            }
//        }
//        #endregion

//        #region Read
//        public async Task<TwitterComment> GetAsync(Guid id)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                var result = await context.TwitterComment.Get(id);
//                return result.Result;
//            }
//        }

//        public async Task<TwitterComment> GetNonDeletedAsync(Guid id)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                var result = await context.TwitterComment.Where(e => e.Id == id && e.IsDeleted == false).List();
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
//        public async Task UpdateAsync(TwitterComment entity)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                await context.TwitterComment.Update(entity);
//            }
//        }
//        #endregion

//        #region Delete
//        public async Task HardDeleteAsync(TwitterComment entity)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                await context.TwitterComment.Delete(entity);
//            }
//        }

//        public async Task DeleteAsync(TwitterComment entity)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                entity.IsDeleted = true;
//                await context.TwitterComment.Update(entity);
//            }
//        }
//        #endregion

//        #region List
//        public async Task<List<TwitterComment>> GetAllAsync()
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                var result = await context.TwitterComment.List();
//                return result.Result.ToList();
//            }
//        }

//        public async Task<List<TwitterComment>> GetAllNonDeletedAsync()
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                var result = await context.TwitterComment.Where(e => e.IsDeleted == false).List();
//                if (result.Result == null)
//                {
//                    return new List<TwitterComment>();
//                }
//                else
//                {
//                    return result.Result.ToList();
//                }
//            }
//        }

//        public async Task<List<TwitterComment>> GetAllDeletedAsync()
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                var result = await context.TwitterComment.Where(e => e.IsDeleted == true).List();
//                if (result.Result == null)
//                {
//                    return new List<TwitterComment>();
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