//using BlockBase.Dapps.NeverForgetBot.Dal.Interfaces;
//using BlockBase.Dapps.NeverForgetBot.Data.Context;
//using BlockBase.Dapps.NeverForgetBot.Data.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace BlockBase.Dapps.NeverForgetBot.Dal.DAOs
//{
//    public class TwitterContextDao : ITwitterContextDao
//    {
//        #region Create
//        public async Task InsertAsync(TwitterContext entity)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                await context.TwitterContext.Insert(entity);
//            }
//        }
//        #endregion

//        #region Read
//        public async Task<TwitterContext> GetAsync(Guid id)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                var result = await context.TwitterContext.Get(id);
//                return result.Result;
//            }
//        }

//        public async Task<TwitterContext> GetNonDeletedAsync(Guid id)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                var result = await context.TwitterContext.Where(e => e.Id == id && e.IsDeleted == false).List();
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
//        public async Task UpdateAsync(TwitterContext entity)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                await context.TwitterContext.Update(entity);
//            }
//        }
//        #endregion

//        #region Delete
//        public async Task HardDeleteAsync(TwitterContext entity)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                await context.TwitterContext.Delete(entity);
//            }
//        }

//        public async Task DeleteAsync(TwitterContext entity)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                entity.IsDeleted = true;
//                await context.TwitterContext.Update(entity);
//            }
//        }
//        #endregion

//        #region List
//        public async Task<List<TwitterContext>> GetAllAsync()
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                var result = await context.TwitterContext.List();
//                return result.Result.ToList();
//            }
//        }

//        public async Task<List<TwitterContext>> GetAllNonDeletedAsync()
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                var result = await context.TwitterContext.Where(e => e.IsDeleted == false).List();
//                if(result.Result == null)
//                {
//                    return new List<TwitterContext>();
//                }
//                else
//                {
//                    return result.Result.ToList();
//                }
//            }
//        }

//        public async Task<List<TwitterContext>> GetAllDeletedAsync()
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                var result = await context.TwitterContext.Where(e => e.IsDeleted == true).List();
//                if(result.Result == null)
//                {
//                    return new List<TwitterContext>();
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
