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
        #region REDDIT
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
        #endregion


        #region TWITTER
        [TestMethod]
        public void TestInsertAndGetTwitter()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase().Result;
                var resultCreate = context.CreateDatabase().Result;
            }

            var twitterDAO = new TwitterContextDao();
            var opExecutor = new DbOperationExecutor();
            var twitterBO = new TwitterContextBo(twitterDAO, opExecutor);

            var twitterContextBM = new TwitterContextBusinessModel
            {
                Id = Guid.NewGuid(),
                TweetId = "tk1",
                TweetText = "@_NeverForgetBot Tweet1",
                TweetDate = DateTime.UtcNow,
                AuthorId = "Au1",
                Author = "Author1",
                InReplyToTweetId = null,
                InReplyToUserId = null,
                InReplyToUser = null,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DeletedAt = null,
                IsDeleted = false
            };

            var twitterContextBMDeleted = new TwitterContextBusinessModel
            {
                Id = Guid.NewGuid(),
                TweetId = "tk2",
                TweetText = "@_NeverForgetBot Tweet2 #comment",
                TweetDate = DateTime.UtcNow,
                AuthorId = "Au2",
                Author = "Author2",
                InReplyToTweetId = twitterContextBM.TweetId,
                InReplyToUserId = twitterContextBM.AuthorId,
                InReplyToUser = twitterContextBM.Author,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DeletedAt = null,
                IsDeleted = true
            };

            twitterBO.InsertAsync(twitterContextBM).Wait();
            twitterBO.InsertAsync(twitterContextBMDeleted).Wait();

            var resGet = twitterBO.GetAsync(twitterContextBM.Id).Result.Result;
            var resGetDeleted = twitterBO.GetAsync(twitterContextBMDeleted.Id).Result.Result;



            Assert.IsTrue(resGet != null && resGetDeleted == null);
        }

        [TestMethod]
        public void TestGetAllNonDeletedTwitter()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase().Result;
                var resultCreate = context.CreateDatabase().Result;
            }

            var twitterDAO = new TwitterContextDao();
            var opExecutor = new DbOperationExecutor();
            var twitterBO = new TwitterContextBo(twitterDAO, opExecutor);

            var twitterContextBM = new TwitterContextBusinessModel
            {
                Id = Guid.NewGuid(),
                TweetId = "tk1",
                TweetText = "@_NeverForgetBot Tweet1",
                TweetDate = DateTime.UtcNow,
                AuthorId = "Au1",
                Author = "Author1",
                InReplyToTweetId = null,
                InReplyToUserId = null,
                InReplyToUser = null,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            twitterBO.InsertAsync(twitterContextBM).Wait();

            var twitterContextBM2 = new TwitterContextBusinessModel
            {
                Id = Guid.NewGuid(),
                TweetId = "tk2",
                TweetText = "@_NeverForgetBot Tweet2 #comment",
                TweetDate = DateTime.UtcNow,
                AuthorId = "Au2",
                Author = "Author2",
                InReplyToTweetId = twitterContextBM.TweetId,
                InReplyToUserId = twitterContextBM.AuthorId,
                InReplyToUser = twitterContextBM.Author,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            twitterBO.InsertAsync(twitterContextBM2).Wait();

            var twitterContextBM3 = new TwitterContextBusinessModel
            {
                Id = Guid.NewGuid(),
                TweetId = "tk3",
                TweetText = "@_NeverForgetBot Tweet3 #tweet #test",
                TweetDate = DateTime.UtcNow,
                AuthorId = "Au3",
                Author = "Author3",
                InReplyToTweetId = twitterContextBM2.TweetId,
                InReplyToUserId = twitterContextBM2.AuthorId,
                InReplyToUser = twitterContextBM2.Author,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            twitterBO.InsertAsync(twitterContextBM3).Wait();

            var twitterContextBMList = twitterBO.GetAllAsync().Result.Result;

            Assert.IsTrue(twitterContextBMList.Count == 3);
        }

        [TestMethod]
        public void TestUpdateTwitter()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase().Result;
                var resultCreate = context.CreateDatabase().Result;
            }

            var twitterDAO = new TwitterContextDao();
            var opExecutor = new DbOperationExecutor();
            var twitterBO = new TwitterContextBo(twitterDAO, opExecutor);

            var twitterContextBM = new TwitterContextBusinessModel
            {
                Id = Guid.NewGuid(),
                TweetId = "tk1",
                TweetText = "@_NeverForgetBot Tweet1",
                TweetDate = DateTime.UtcNow,
                AuthorId = "Au1",
                Author = "Author1",
                InReplyToTweetId = null,
                InReplyToUserId = null,
                InReplyToUser = null,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            twitterBO.InsertAsync(twitterContextBM).Wait();

            var twitterContextBM2 = new TwitterContextBusinessModel
            {
                Id = Guid.NewGuid(),
                TweetId = "tk2",
                TweetText = "@_NeverForgetBot Tweet2 #comment",
                TweetDate = DateTime.UtcNow,
                AuthorId = "Au2",
                Author = "Author2",
                InReplyToTweetId = twitterContextBM.TweetId,
                InReplyToUserId = twitterContextBM.AuthorId,
                InReplyToUser = twitterContextBM.Author,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            twitterBO.InsertAsync(twitterContextBM2).Wait();

            var twitterContextBM3 = new TwitterContextBusinessModel
            {
                Id = Guid.NewGuid(),
                TweetId = "tk3",
                TweetText = "@_NeverForgetBot Tweet3 #tweet #test",
                TweetDate = DateTime.UtcNow,
                AuthorId = "Au3",
                Author = "Author3",
                InReplyToTweetId = twitterContextBM2.TweetId,
                InReplyToUserId = twitterContextBM2.AuthorId,
                InReplyToUser = twitterContextBM2.Author,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            twitterBO.InsertAsync(twitterContextBM3).Wait();


            var resGet = twitterBO.GetAsync(twitterContextBM.Id).Result.Result;
            resGet.Author = "NewAuthor";
            twitterBO.UpdateAsync(resGet).Wait();

            Assert.IsTrue(resGet.Author == "NewAuthor");
        }

        [TestMethod]
        public void TestDeleteTwitter()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase().Result;
                var resultCreate = context.CreateDatabase().Result;
            }

            var twitterDAO = new TwitterContextDao();
            var opExecutor = new DbOperationExecutor();
            var twitterBO = new TwitterContextBo(twitterDAO, opExecutor);

            var twitterContextBM = new TwitterContextBusinessModel
            {
                Id = Guid.NewGuid(),
                TweetId = "tk1",
                TweetText = "@_NeverForgetBot Tweet1",
                TweetDate = DateTime.UtcNow,
                AuthorId = "Au1",
                Author = "Author1",
                InReplyToTweetId = null,
                InReplyToUserId = null,
                InReplyToUser = null,

                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            twitterBO.InsertAsync(twitterContextBM).Wait();

            var twitterContextBM2 = new TwitterContextBusinessModel
            {
                Id = Guid.NewGuid(),
                TweetId = "tk2",
                TweetText = "@_NeverForgetBot Tweet2 #comment",
                TweetDate = DateTime.UtcNow,
                AuthorId = "Au2",
                Author = "Author2",
                InReplyToTweetId = twitterContextBM.TweetId,
                InReplyToUserId = twitterContextBM.AuthorId,
                InReplyToUser = twitterContextBM.Author,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            twitterBO.InsertAsync(twitterContextBM2).Wait();

            var twitterContextBM3 = new TwitterContextBusinessModel
            {
                Id = Guid.NewGuid(),
                TweetId = "tk3",
                TweetText = "@_NeverForgetBot Tweet3 #tweet #test",
                TweetDate = DateTime.UtcNow,
                AuthorId = "Au3",
                Author = "Author3",
                InReplyToTweetId = twitterContextBM2.TweetId,
                InReplyToUserId = twitterContextBM2.AuthorId,
                InReplyToUser = twitterContextBM2.Author,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            twitterBO.InsertAsync(twitterContextBM3).Wait();

            twitterBO.DeleteAsync(twitterContextBM3).Wait();

            Assert.IsTrue(twitterContextBM3.IsDeleted == true);
        }
        #endregion
    }
}
