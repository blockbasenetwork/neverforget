using BlockBase.Dapps.NeverForgetBot.Dal.DAOs;
using BlockBase.Dapps.NeverForgetBot.Data.Context;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BlockBase.Dapps.NeverForgetBot.Tests
{
    [TestClass]
    public class DataAccessTests
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


            var redditContext = new RedditContext { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", CommentPost = "NeverForgetThis", PostingDate = 1270637661, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditDAO.InsertAsync(redditContext).Wait();

            var resGet = redditDAO.GetAsync(redditContext.Id).Result;



            Assert.IsTrue(resGet != null);
        }

        [TestMethod]
        public void TestGetAllReddit()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase().Result;
                var resultCreate = context.CreateDatabase().Result;
            }

            var redditDAO = new RedditContextDao();

            var redditContext = new RedditContext { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", CommentPost = "NeverForgetThis", PostingDate = 1270637661, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditDAO.InsertAsync(redditContext).Wait();
            var redditContext2 = new RedditContext { Id = Guid.NewGuid(), CommentId = "tk2", Author = "Autor", SubReddit = "Testando", CommentPost = "NeverForgetThis", PostingDate = 1270637662, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditDAO.InsertAsync(redditContext2).Wait();
            var redditContext3 = new RedditContext { Id = Guid.NewGuid(), CommentId = "tk3", Author = "Ator", SubReddit = "Testing", CommentPost = "NeverForgetThis", PostingDate = 1270637663, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditDAO.InsertAsync(redditContext3).Wait();

            var redditContextList = redditDAO.GetAllAsync().Result;

            Assert.IsTrue(redditContextList.Count == 3);
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


            var redditContext = new RedditContext { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", CommentPost = "NeverForgetThis", PostingDate = 1270637661, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditDAO.InsertAsync(redditContext).Wait();


            var resGet = redditDAO.GetAsync(redditContext.Id).Result;
            resGet.Author = "NovoAutor";
            redditDAO.UpdateAsync(resGet).Wait();



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


            var redditContext = new RedditContext { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", CommentPost = "NeverForgetThis", PostingDate = 1270637661, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditDAO.InsertAsync(redditContext).Wait();
            var redditContext2 = new RedditContext { Id = Guid.NewGuid(), CommentId = "tk2", Author = "Autor", SubReddit = "Testando", CommentPost = "NeverForgetThis", PostingDate = 1270637662, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditDAO.InsertAsync(redditContext2).Wait();
            var redditContext3 = new RedditContext { Id = Guid.NewGuid(), CommentId = "tk3", Author = "Ator", SubReddit = "Testing", CommentPost = "NeverForgetThis", PostingDate = 1270637663, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditDAO.InsertAsync(redditContext3).Wait();


            redditDAO.DeleteAsync(redditContext3).Wait();



            Assert.IsTrue(redditContext3.IsDeleted == true);
        }

        [TestMethod]
        public void TestGetNonDeletedReddit()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase().Result;
                var resultCreate = context.CreateDatabase().Result;
            }

            var redditDAO = new RedditContextDao();


            var redditContext = new RedditContext { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", CommentPost = "NeverForgetThis", PostingDate = 1270637661, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditDAO.InsertAsync(redditContext).Wait();
            var redditContext2 = new RedditContext { Id = Guid.NewGuid(), CommentId = "tk2", Author = "Autor", SubReddit = "Testando", CommentPost = "NeverForgetThis", PostingDate = 1270637662, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditDAO.InsertAsync(redditContext2).Wait();
            var redditContext3 = new RedditContext { Id = Guid.NewGuid(), CommentId = "tk3", Author = "Ator", SubReddit = "Testing", CommentPost = "NeverForgetThis", PostingDate = 1270637663, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditDAO.InsertAsync(redditContext3).Wait();


            redditDAO.DeleteAsync(redditContext3).Wait();

            var resGetNonDeleted = redditDAO.GetNonDeletedAsync(redditContext3.Id).Result;
            var resGetNonDeleted2 = redditDAO.GetNonDeletedAsync(redditContext2.Id).Result;


            Assert.IsTrue(resGetNonDeleted == null && resGetNonDeleted2 != null);
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


            var redditContext = new RedditContext { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", CommentPost = "NeverForgetThis", PostingDate = 1270637661, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditDAO.InsertAsync(redditContext).Wait();
            var redditContext2 = new RedditContext { Id = Guid.NewGuid(), CommentId = "tk2", Author = "Autor", SubReddit = "Testando", CommentPost = "NeverForgetThis", PostingDate = 1270637662, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditDAO.InsertAsync(redditContext2).Wait();
            var redditContext3 = new RedditContext { Id = Guid.NewGuid(), CommentId = "tk3", Author = "Ator", SubReddit = "Testing", CommentPost = "NeverForgetThis", PostingDate = 1270637663, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditDAO.InsertAsync(redditContext3).Wait();


            redditDAO.DeleteAsync(redditContext3).Wait();

            var redditContextList = redditDAO.GetAllAsync().Result;

            var redditContextListNonDeleted = redditDAO.GetAllNonDeletedAsync().Result;



            //Assert.IsTrue(redditContextList.Contains);
        }

        [TestMethod]
        public void TestGetAllDeletedReddit()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase().Result;
                var resultCreate = context.CreateDatabase().Result;
            }

            var redditDAO = new RedditContextDao();


            var redditContext = new RedditContext { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", CommentPost = "NeverForgetThis", PostingDate = 1270637661, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditDAO.InsertAsync(redditContext).Wait();
            var redditContext2 = new RedditContext { Id = Guid.NewGuid(), CommentId = "tk2", Author = "Autor", SubReddit = "Testando", CommentPost = "NeverForgetThis", PostingDate = 1270637662, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditDAO.InsertAsync(redditContext2).Wait();
            var redditContext3 = new RedditContext { Id = Guid.NewGuid(), CommentId = "tk3", Author = "Ator", SubReddit = "Testing", CommentPost = "NeverForgetThis", PostingDate = 1270637663, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            redditDAO.InsertAsync(redditContext3).Wait();


            redditDAO.DeleteAsync(redditContext3).Wait();
            var redditContextList = redditDAO.GetAllAsync().Result;
            var redditContextListDeleted = redditDAO.GetAllDeletedAsync().Result;



            Assert.IsTrue(redditContextList.Count == 3 && redditContextListDeleted.Count == 1);
        }
    }
}
