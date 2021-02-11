//using BlockBase.Dapps.NeverForgetBot.Dal.Interfaces;
//using BlockBase.Dapps.NeverForgetBot.Data.Context;
//using BlockBase.Dapps.NeverForgetBot.Data.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace BlockBase.Dapps.NeverForgetBot.Dal.DAOs
//{
//    public class RedditContextDao : IRedditContextDao
//    {
//        #region Create
//        public async Task InsertAsync(RedditContext entity)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                await context.RedditContext.Insert(entity);
//            }
//        }
//        #endregion

//        #region Read
//        public async Task<RedditContext> GetAsync(Guid id)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                var result = await context.RedditContext.Get(id);
//                return result.Result;
//            }
//        }

//        public async Task<RedditContext> GetNonDeletedAsync(Guid id)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                var result = await context.RedditContext.Where(e => e.Id == id && e.IsDeleted == false).List();
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
//        public async Task UpdateAsync(RedditContext entity)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                await context.RedditContext.Update(entity);
//            }
//        }
//        #endregion

//        #region Delete
//        public async Task HardDeleteAsync(RedditContext entity)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                await context.RedditContext.Delete(entity);
//            }
//        }

//        public async Task DeleteAsync(RedditContext entity)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                entity.IsDeleted = true;
//                await context.RedditContext.Update(entity);
//            }
//        }
//        #endregion

//        #region List
//        public async Task<List<RedditContext>> GetAllAsync()
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                var result = await context.RedditContext.List();
//                return result.Result.ToList();
//            }
//        }

//        public async Task<List<RedditContext>> GetAllNonDeletedAsync()
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                var result = await context.RedditContext.Where(e => e.IsDeleted == false).List();
//                if (result.Result == null)
//                {
//                    return new List<RedditContext>();
//                }
//                else
//                {
//                    return result.Result.ToList();
//                }
//            }
//        }

//        public async Task<List<RedditContext>> GetAllDeletedAsync()
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                var result = await context.RedditContext.Where(e => e.IsDeleted == true).List();
//                if (result.Result == null)
//                {
//                    return new List<RedditContext>();
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
