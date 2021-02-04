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

            var twitterContext = new TwitterContext
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
            twitterDAO.InsertAsync(twitterContext).Wait();

            var resGet = twitterDAO.GetAsync(twitterContext.Id).Result;

            Assert.IsTrue(resGet != null);
        }

        [TestMethod]
        public void TestGetAllTwitter()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase().Result;
                var resultCreate = context.CreateDatabase().Result;
            }

            var twitterDAO = new TwitterContextDao();

            var twitterContext = new TwitterContext
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
            twitterDAO.InsertAsync(twitterContext).Wait();

            var twitterContext2 = new TwitterContext
            {
                Id = Guid.NewGuid(),
                TweetId = "tk2",
                TweetText = "@_NeverForgetBot Tweet2 #comment",
                TweetDate = DateTime.UtcNow,
                AuthorId = "Au2",
                Author = "Author2",
                InReplyToTweetId = twitterContext.TweetId,
                InReplyToUserId = twitterContext.AuthorId,
                InReplyToUser = twitterContext.Author,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            twitterDAO.InsertAsync(twitterContext2).Wait();

            var twitterContext3 = new TwitterContext
            {
                Id = Guid.NewGuid(),
                TweetId = "tk3",
                TweetText = "@_NeverForgetBot Tweet3 #tweet",
                TweetDate = DateTime.UtcNow,
                AuthorId = "Au3",
                Author = "Author3",
                InReplyToTweetId = twitterContext2.TweetId,
                InReplyToUserId = twitterContext2.AuthorId,
                InReplyToUser = twitterContext2.Author,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            twitterDAO.InsertAsync(twitterContext3).Wait();

            var twitterContextList = twitterDAO.GetAllAsync().Result;

            Assert.IsTrue(twitterContextList.Count == 3);
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

            var twitterContext = new TwitterContext
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
            twitterDAO.InsertAsync(twitterContext).Wait();


            var resGet = twitterDAO.GetAsync(twitterContext.Id).Result;
            resGet.Author = "NewAuthor";
            twitterDAO.UpdateAsync(resGet).Wait();

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

            var twitterContext = new TwitterContext
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
            twitterDAO.InsertAsync(twitterContext).Wait();

            var twitterContext2 = new TwitterContext
            {
                Id = Guid.NewGuid(),
                TweetId = "tk2",
                TweetText = "@_NeverForgetBot Tweet2 #comment",
                TweetDate = DateTime.UtcNow,
                AuthorId = "Au2",
                Author = "Author2",
                InReplyToTweetId = twitterContext.TweetId,
                InReplyToUserId = twitterContext.AuthorId,
                InReplyToUser = twitterContext.Author,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            twitterDAO.InsertAsync(twitterContext2).Wait();

            var twitterContext3 = new TwitterContext
            {
                Id = Guid.NewGuid(),
                TweetId = "tk3",
                TweetText = "@_NeverForgetBot Tweet3 #tweet #test",
                TweetDate = DateTime.UtcNow,
                AuthorId = "Au3",
                Author = "Author3",
                InReplyToTweetId = twitterContext2.TweetId,
                InReplyToUserId = twitterContext2.AuthorId,
                InReplyToUser = twitterContext2.Author,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            twitterDAO.InsertAsync(twitterContext3).Wait();

            twitterDAO.DeleteAsync(twitterContext3).Wait();

            Assert.IsTrue(twitterContext3.IsDeleted == true);
        }

        [TestMethod]
        public void TestGetNonDeletedTwitter()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase().Result;
                var resultCreate = context.CreateDatabase().Result;
            }

            var twitterDAO = new TwitterContextDao();

            var twitterContext = new TwitterContext
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
            twitterDAO.InsertAsync(twitterContext).Wait();

            var twitterContext2 = new TwitterContext
            {
                Id = Guid.NewGuid(),
                TweetId = "tk2",
                TweetText = "@_NeverForgetBot Tweet2 #comment",
                TweetDate = DateTime.UtcNow,
                AuthorId = "Au2",
                Author = "Author2",
                InReplyToTweetId = twitterContext.TweetId,
                InReplyToUserId = twitterContext.AuthorId,
                InReplyToUser = twitterContext.Author,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            twitterDAO.InsertAsync(twitterContext2).Wait();

            var twitterContext3 = new TwitterContext
            {
                Id = Guid.NewGuid(),
                TweetId = "tk3",
                TweetText = "@_NeverForgetBot Tweet3 #tweet #test",
                TweetDate = DateTime.UtcNow,
                AuthorId = "Au3",
                Author = "Author3",
                InReplyToTweetId = twitterContext2.TweetId,
                InReplyToUserId = twitterContext2.AuthorId,
                InReplyToUser = twitterContext2.Author,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            twitterDAO.InsertAsync(twitterContext3).Wait();

            twitterDAO.DeleteAsync(twitterContext3).Wait();

            var resGetNonDeleted = twitterDAO.GetNonDeletedAsync(twitterContext3.Id).Result;
            var resGetNonDeleted2 = twitterDAO.GetNonDeletedAsync(twitterContext2.Id).Result;

            Assert.IsTrue(resGetNonDeleted == null && resGetNonDeleted2 != null);
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

            var twitterContext = new TwitterContext
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
            twitterDAO.InsertAsync(twitterContext).Wait();

            var twitterContext2 = new TwitterContext
            {
                Id = Guid.NewGuid(),
                TweetId = "tk2",
                TweetText = "@_NeverForgetBot Tweet2 #comment",
                TweetDate = DateTime.UtcNow,
                AuthorId = "Au2",
                Author = "Author2",
                InReplyToTweetId = twitterContext.TweetId,
                InReplyToUserId = twitterContext.AuthorId,
                InReplyToUser = twitterContext.Author,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            twitterDAO.InsertAsync(twitterContext2).Wait();

            var twitterContext3 = new TwitterContext
            {
                Id = Guid.NewGuid(),
                TweetId = "tk3",
                TweetText = "@_NeverForgetBot Tweet3 #tweet #test",
                TweetDate = DateTime.UtcNow,
                AuthorId = "Au3",
                Author = "Author3",
                InReplyToTweetId = twitterContext2.TweetId,
                InReplyToUserId = twitterContext2.AuthorId,
                InReplyToUser = twitterContext2.Author,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            twitterDAO.InsertAsync(twitterContext3).Wait();

            twitterDAO.DeleteAsync(twitterContext3).Wait();

            var twitterContextList = twitterDAO.GetAllAsync().Result;

            var twitterContextListNonDeleted = twitterDAO.GetAllNonDeletedAsync().Result;

            //Assert.IsTrue(twitterContextList.Contains);
            Assert.IsTrue(twitterContextList.Count == 3 && twitterContextListNonDeleted.Count == 2);
        }

        [TestMethod]
        public void TestGetAllDeletedTwitter()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase().Result;
                var resultCreate = context.CreateDatabase().Result;
            }

            var twitterDAO = new TwitterContextDao();

            var twitterContext = new TwitterContext
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
            twitterDAO.InsertAsync(twitterContext).Wait();

            var twitterContext2 = new TwitterContext
            {
                Id = Guid.NewGuid(),
                TweetId = "tk2",
                TweetText = "@_NeverForgetBot Tweet2 #comment",
                TweetDate = DateTime.UtcNow,
                AuthorId = "Au2",
                Author = "Author2",
                InReplyToTweetId = twitterContext.TweetId,
                InReplyToUserId = twitterContext.AuthorId,
                InReplyToUser = twitterContext.Author,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            twitterDAO.InsertAsync(twitterContext2).Wait();

            var twitterContext3 = new TwitterContext
            {
                Id = Guid.NewGuid(),
                TweetId = "tk3",
                TweetText = "@_NeverForgetBot Tweet3 #tweet #test",
                TweetDate = DateTime.UtcNow,
                AuthorId = "Au3",
                Author = "Author3",
                InReplyToTweetId = twitterContext2.TweetId,
                InReplyToUserId = twitterContext2.AuthorId,
                InReplyToUser = twitterContext2.Author,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            twitterDAO.InsertAsync(twitterContext3).Wait();

            twitterDAO.DeleteAsync(twitterContext3).Wait();

            var twitterContextList = twitterDAO.GetAllAsync().Result;
            var twitterContextListDeleted = twitterDAO.GetAllDeletedAsync().Result;

            Assert.IsTrue(twitterContextList.Count == 3 && twitterContextListDeleted.Count == 1);
        }
        #endregion
    }
}
