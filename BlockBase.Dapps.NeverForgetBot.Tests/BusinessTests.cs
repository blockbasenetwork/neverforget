using BlockBase.Dapps.NeverForgetBot.Business.BOs;
using BlockBase.Dapps.NeverForgetBot.Business.BusinessModels;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Dal.DAOs;
using BlockBase.Dapps.NeverForgetBot.Data.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BlockBase.Dapps.NeverForgetBot.Tests
{
    [TestClass]
    public class BusinessTests
    {
        [TestMethod]
        public void TestInsertAndGetRedditContextAsync()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase().Result;
                var resultCreate = context.CreateDatabase().Result;
            }

            var redditDAO = new RedditContextDao();
            var opExecutor = new DbOperationExecutor();
            var redditBO = new RedditContextBo(redditDAO, opExecutor);


            var redditContextBM = new RedditContextBusinessModel { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", CommentPost = "NeverForgetThis", PostingDate = 1270637661, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditBO.InsertAsync(redditContextBM).Wait();

            var resGet = redditDAO.GetAsync(redditContextBM.Id).Result;



            Assert.IsTrue(resGet != null);
        }

        [TestMethod]
        public void TestGetListRedditContextAsync()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase().Result;
                var resultCreate = context.CreateDatabase().Result;
            }

            var redditDAO = new RedditContextDao();
            var opExecutor = new DbOperationExecutor();
            var redditBO = new RedditContextBo(redditDAO, opExecutor);


            var redditContextBM = new RedditContextBusinessModel { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", CommentPost = "NeverForgetThis", PostingDate = 1270637661, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditBO.InsertAsync(redditContextBM).Wait();
            var redditContextBM2 = new RedditContextBusinessModel { Id = Guid.NewGuid(), CommentId = "tk2", Author = "Autor", SubReddit = "Testando", CommentPost = "NeverForgetThis", PostingDate = 1270637662, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditBO.InsertAsync(redditContextBM2).Wait();
            var redditContextBM3 = new RedditContextBusinessModel { Id = Guid.NewGuid(), CommentId = "tk3", Author = "Ator", SubReddit = "Testing", CommentPost = "NeverForgetThis", PostingDate = 1270637663, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditBO.InsertAsync(redditContextBM3).Wait();


            var redditContextBMList = redditBO.GetAllAsync().Result.Result;



            Assert.IsTrue(redditContextBMList[2] != null);
        }
    }
}
