using BlockBase.Dapps.NeverForgetBot.Business.BOs;
using BlockBase.Dapps.NeverForgetBot.Business.BusinessModels;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Dal.DAOs;
using BlockBase.Dapps.NeverForgetBot.Data.Context;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace BlockBase.Dapps.NeverForgetBot.Tests
{
    [TestClass]
    public class BusinessTests
    {
        [TestMethod]
        public void TestInsertAndGetReddit()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase().Result;
                var resultCreate = context.CreateDatabase().Result;
            }

            var redditDAO = new RedditContextDao();
            var mockLogger = new Mock<ILogger<DbOperationExecutor>>();
            var opExecutor = new DbOperationExecutor(mockLogger.Object);
            var redditBO = new RedditContextBo(redditDAO, opExecutor);


            var redditContextBM = new RedditContextBusinessModel { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", CommentPost = "NeverForgetThis", PostingDate = 1270637661, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, DeletedAt = null, IsDeleted = false};
            var redditContextBMDeleted = new RedditContextBusinessModel { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", CommentPost = "NeverForgetThis", PostingDate = 1270637661, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, DeletedAt = null, IsDeleted = true };

            redditBO.InsertAsync(redditContextBM).Wait();
            redditBO.InsertAsync(redditContextBMDeleted).Wait();

            var resGet = redditBO.GetAsync(redditContextBM.Id).Result.Result;
            var resGetDeleted = redditBO.GetAsync(redditContextBMDeleted.Id).Result.Result;



            Assert.IsTrue(resGet != null && resGetDeleted == null);
        }

        [TestMethod]
        public void TestGetAllNonDeletedReddit()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase().Result;
                var resultCreate = context.CreateDatabase().Result;
            }

            var redditDAO = new RedditContextDao();
            var mockLogger = new Mock<ILogger<DbOperationExecutor>>();
            var opExecutor = new DbOperationExecutor(mockLogger.Object);
            var redditBO = new RedditContextBo(redditDAO, opExecutor);


            var redditContextBM = new RedditContextBusinessModel { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", CommentPost = "NeverForgetThis", PostingDate = 1270637661, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditBO.InsertAsync(redditContextBM).Wait();
            var redditContextBM2 = new RedditContextBusinessModel { Id = Guid.NewGuid(), CommentId = "tk2", Author = "Autor", SubReddit = "Testando", CommentPost = "NeverForgetThis", PostingDate = 1270637662, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditBO.InsertAsync(redditContextBM2).Wait();
            var redditContextBM3 = new RedditContextBusinessModel { Id = Guid.NewGuid(), CommentId = "tk3", Author = "Ator", SubReddit = "Testing", CommentPost = "NeverForgetThis", PostingDate = 1270637663, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditBO.InsertAsync(redditContextBM3).Wait();


            var redditContextBMList = redditBO.GetAllAsync().Result.Result;



            Assert.IsTrue(redditContextBMList.Count == 3);
        }

        [TestMethod]
        public void TestUpdateReddit()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase().Result;
                var resultCreate = context.CreateDatabase().Result;
            }

            var redditDAO = new RedditContextDao();
            var mockLogger = new Mock<ILogger<DbOperationExecutor>>();
            var opExecutor = new DbOperationExecutor(mockLogger.Object);
            var redditBO = new RedditContextBo(redditDAO, opExecutor);


            var redditContextBM = new RedditContextBusinessModel { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", CommentPost = "NeverForgetThis", PostingDate = 1270637661, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditBO.InsertAsync(redditContextBM).Wait();
            var redditContextBM2 = new RedditContextBusinessModel { Id = Guid.NewGuid(), CommentId = "tk2", Author = "Autor", SubReddit = "Testando", CommentPost = "NeverForgetThis", PostingDate = 1270637662, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditBO.InsertAsync(redditContextBM2).Wait();
            var redditContextBM3 = new RedditContextBusinessModel { Id = Guid.NewGuid(), CommentId = "tk3", Author = "Ator", SubReddit = "Testing", CommentPost = "NeverForgetThis", PostingDate = 1270637663, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditBO.InsertAsync(redditContextBM3).Wait();


            var resGet = redditBO.GetAsync(redditContextBM.Id).Result.Result;
            resGet.Author = "NovoAutor";
            redditBO.UpdateAsync(resGet).Wait();

            Assert.IsTrue(resGet.Author == "NovoAutor");
        }

        [TestMethod]
        public void TestDeleteReddit()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase().Result;
                var resultCreate = context.CreateDatabase().Result;
            }

            var redditDAO = new RedditContextDao();
            var mockLogger = new Mock<ILogger<DbOperationExecutor>>();
            var opExecutor = new DbOperationExecutor(mockLogger.Object);
            var redditBO = new RedditContextBo(redditDAO, opExecutor);


            var redditContextBM = new RedditContextBusinessModel { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", CommentPost = "NeverForgetThis", PostingDate = 1270637661, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditBO.InsertAsync(redditContextBM).Wait();
            var redditContextBM2 = new RedditContextBusinessModel { Id = Guid.NewGuid(), CommentId = "tk2", Author = "Autor", SubReddit = "Testando", CommentPost = "NeverForgetThis", PostingDate = 1270637662, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditBO.InsertAsync(redditContextBM2).Wait();
            var redditContextBM3 = new RedditContextBusinessModel { Id = Guid.NewGuid(), CommentId = "tk3", Author = "Ator", SubReddit = "Testing", CommentPost = "NeverForgetThis", PostingDate = 1270637663, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditBO.InsertAsync(redditContextBM3).Wait();

            redditBO.DeleteAsync(redditContextBM3).Wait();


            Assert.IsTrue(redditContextBM3.IsDeleted == true);
        }
    }
}
