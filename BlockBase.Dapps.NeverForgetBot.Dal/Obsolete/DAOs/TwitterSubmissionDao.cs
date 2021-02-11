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
//    public class TwitterSubmissionDao : ITwitterSubmissionDao
//    {
//        #region Create
//        public async Task InsertAsync(TwitterSubmission entity)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                await context.TwitterSubmission.Insert(entity);
//            }
//        }
//        #endregion

//        #region Read
//        public async Task<TwitterSubmission> GetAsync(Guid id)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                var result = await context.TwitterSubmission.Get(id);
//                return result.Result;
//            }
//        }

//        public async Task<TwitterSubmission> GetNonDeletedAsync(Guid id)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                var result = await context.TwitterSubmission.Where(e => e.Id == id && e.IsDeleted == false).List();
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
//        public async Task UpdateAsync(TwitterSubmission entity)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                await context.TwitterSubmission.Update(entity);
//            }
//        }
//        #endregion

//        #region Delete
//        public async Task HardDeleteAsync(TwitterSubmission entity)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                await context.TwitterSubmission.Delete(entity);
//            }
//        }

//        public async Task DeleteAsync(TwitterSubmission entity)
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                entity.IsDeleted = true;
//                await context.TwitterSubmission.Update(entity);
//            }
//        }
//        #endregion

//        #region List
//        public async Task<List<TwitterSubmission>> GetAllAsync()
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                var result = await context.TwitterSubmission.List();
//                return result.Result.ToList();
//            }
//        }

//        public async Task<List<TwitterSubmission>> GetAllNonDeletedAsync()
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                var result = await context.TwitterSubmission.Where(e => e.IsDeleted == false).List();
//                if (result.Result == null)
//                {
//                    return new List<TwitterSubmission>();
//                }
//                else
//                {
//                    return result.Result.ToList();
//                }
//            }
//        }

//        public async Task<List<TwitterSubmission>> GetAllDeletedAsync()
//        {
//            using (var context = new NeverForgetBotDbContext())
//            {
//                var result = await context.TwitterSubmission.Where(e => e.IsDeleted == true).List();
//                if (result.Result == null)
//                {
//                    return new List<TwitterSubmission>();
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