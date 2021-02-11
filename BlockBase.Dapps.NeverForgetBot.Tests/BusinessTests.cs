using BlockBase.Dapps.NeverForgetBot.Business.GenericBusiness;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Common.Enums;
using BlockBase.Dapps.NeverForgetBot.Dal;
using BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess;
using BlockBase.Dapps.NeverForgetBot.Data.Context;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace BlockBase.Dapps.NeverForgetBot.Tests
{
    [TestClass]
    public class BusinessTests
    {
        #region Reddit
        [TestMethod]
        public void TestInsertAndGetReddit()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase().Result;
                var resultCreate = context.CreateDatabase().Result;
            }
            var _requestTypeDao = new RequestTypeDao();
            RequestType defaultRequest = new RequestType { Id = (int)RequestTypeEnum.Default, Name = "Default" };
            _requestTypeDao.InsertAsync(defaultRequest).Wait();


            var redditContextDAO = new RedditContextDao();
            var redditCommentDAO = new RedditCommentDao();
            var mockLogger = new Mock<ILogger<DbOperationExecutor>>();
            var opExecutor = new DbOperationExecutor(mockLogger.Object);
            var redditContextBO = new RedditContextBo(redditContextDAO, opExecutor);
            var redditCommentBO = new RedditCommentBo(redditCommentDAO, opExecutor);

            var redditContext = new RedditContext { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, RequestTypeId = defaultRequest.Id, RequestType = defaultRequest };
            redditContextBO.InsertAsync(redditContext).Wait();

            var redditComment = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentBO.InsertAsync(redditComment).Wait();

            var resGet = redditCommentBO.GetAsync(redditComment.Id);

            Assert.IsTrue(resGet != null);
        }

        [TestMethod]
        public void TestGetAllNonDeletedReddit()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase().Result;
                var resultCreate = context.CreateDatabase().Result;
            }
            var _requestTypeDao = new RequestTypeDao();
            RequestType defaultRequest = new RequestType { Id = (int)RequestTypeEnum.Default, Name = "Default" };
            _requestTypeDao.InsertAsync(defaultRequest).Wait();

            var redditContextDAO = new RedditContextDao();
            var redditCommentDAO = new RedditCommentDao();
            var mockLogger = new Mock<ILogger<DbOperationExecutor>>();
            var opExecutor = new DbOperationExecutor(mockLogger.Object);
            var redditContextBO = new RedditContextBo(redditContextDAO, opExecutor);
            var redditCommentBO = new RedditCommentBo(redditCommentDAO, opExecutor);

            var redditContext = new RedditContext { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, RequestTypeId = defaultRequest.Id, RequestType = defaultRequest };
            redditContextBO.InsertAsync(redditContext).Wait();

            var redditComment = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentBO.InsertAsync(redditComment).Wait();
            var redditComment2 = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk2", Author = "Autor", SubReddit = "Testando", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentBO.InsertAsync(redditComment2).Wait();
            var redditComment3 = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk3", Author = "Ator", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentBO.InsertAsync(redditComment3).Wait();


            var redditContextBMList = redditCommentBO.GetAllAsync().Result.Result;



            Assert.IsTrue(redditContextBMList.Count == 3);
        }

        [TestMethod]
        public void TestDeleteReddit()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase().Result;
                var resultCreate = context.CreateDatabase().Result;
            }

            var _requestTypeDao = new RequestTypeDao();
            RequestType defaultRequest = new RequestType { Id = (int)RequestTypeEnum.Default, Name = "Default" };
            _requestTypeDao.InsertAsync(defaultRequest).Wait();

            var redditContextDAO = new RedditContextDao();
            var redditCommentDAO = new RedditCommentDao();
            var mockLogger = new Mock<ILogger<DbOperationExecutor>>();
            var opExecutor = new DbOperationExecutor(mockLogger.Object);
            var redditContextBO = new RedditContextBo(redditContextDAO, opExecutor);
            var redditCommentBO = new RedditCommentBo(redditCommentDAO, opExecutor);

            var redditContext = new RedditContext { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, RequestTypeId = defaultRequest.Id, RequestType = defaultRequest };
            redditContextBO.InsertAsync(redditContext).Wait();

            var redditComment = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentBO.InsertAsync(redditComment).Wait();
            var redditComment2 = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk2", Author = "Autor", SubReddit = "Testando", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentBO.InsertAsync(redditComment2).Wait();
            var redditComment3 = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk3", Author = "Ator", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentBO.InsertAsync(redditComment3).Wait();

            redditCommentBO.DeleteAsync(redditComment3).Wait();

            
            Assert.IsTrue(redditComment3.IsDeleted == true);
        }
        #endregion


        #region Twitter
        [TestMethod]
        public void TestInsertAndGetTwitter()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase().Result;
                var resultCreate = context.CreateDatabase().Result;
            }

            var _requestTypeDao = new RequestTypeDao();
            RequestType defaultRequest = new RequestType { Id = (int)RequestTypeEnum.Default, Name = "Default" };
            _requestTypeDao.InsertAsync(defaultRequest).Wait();

            var twitterContextDAO = new TwitterContextDao();
            var twitterCommentDAO = new TwitterCommentDao();
            var mockLogger = new Mock<ILogger<DbOperationExecutor>>();
            var opExecutor = new DbOperationExecutor(mockLogger.Object);
            var twitterContextBO = new TwitterContextBo(twitterContextDAO, opExecutor);
            var twitterCommentBO = new TwitterCommentBo(twitterCommentDAO, opExecutor);

            var twitterContext = new TwitterContext
            {
                Id = Guid.NewGuid(),
                RequestTypeId = 0,
                RequestType = defaultRequest,
                CreatedAt = DateTime.UtcNow,
            };
            twitterContextBO.InsertAsync(twitterContext).Wait();

            var twitterComment = new TwitterComment
            {
                Id = Guid.NewGuid(),
                CommentId = "tk1",
                Content = "@_NeverForgetBot Tweet1",
                CommentDate = DateTime.UtcNow,
                Author = "Author1",
                IsDeleted = false,
                Link = "Link",
                MediaLink = "Link2",
                ReplyToId = "fefe",
                TwitterContextId = twitterContext.Id,
                CreatedAt = DateTime.UtcNow,

            };
            twitterCommentBO.InsertAsync(twitterComment).Wait();

            var resGet = twitterCommentBO.GetAsync(twitterComment.Id).Result.Result;



            Assert.IsTrue(resGet != null);
        }

        [TestMethod]
        public void TestGetAllNonDeletedTwitter()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase().Result;
                var resultCreate = context.CreateDatabase().Result;
            }

            var _requestTypeDao = new RequestTypeDao();
            RequestType defaultRequest = new RequestType { Id = (int)RequestTypeEnum.Default, Name = "Default" };
            _requestTypeDao.InsertAsync(defaultRequest).Wait();

            var twitterContextDAO = new TwitterContextDao();
            var twitterCommentDAO = new TwitterCommentDao();
            var mockLogger = new Mock<ILogger<DbOperationExecutor>>();
            var opExecutor = new DbOperationExecutor(mockLogger.Object);
            var twitterContextBO = new TwitterContextBo(twitterContextDAO, opExecutor);
            var twitterCommentBO = new TwitterCommentBo(twitterCommentDAO, opExecutor);

            var twitterContext = new TwitterContext
            {
                Id = Guid.NewGuid(),
                RequestTypeId = 0,
                RequestType = defaultRequest,
                CreatedAt = DateTime.UtcNow,
            };
            twitterContextBO.InsertAsync(twitterContext).Wait();

            var twitterComment = new TwitterComment
            {
                Id = Guid.NewGuid(),
                CommentId = "tk1",
                Content = "@_NeverForgetBot Tweet1",
                CommentDate = DateTime.UtcNow,
                Author = "Author1",
                IsDeleted = false,
                Link = "Link",
                MediaLink = "Link2",
                ReplyToId = "fefe",
                TwitterContextId = twitterContext.Id,
                CreatedAt = DateTime.UtcNow,

            };
            twitterCommentBO.InsertAsync(twitterComment).Wait();

            var twitterComment2 = new TwitterComment
            {
                Id = Guid.NewGuid(),
                CommentId = "tk2",
                Content = "@_NeverForgetBot Tweet2",
                CommentDate = DateTime.UtcNow,
                Author = "Author2",
                IsDeleted = false,
                Link = "Link",
                MediaLink = "Link2",
                ReplyToId = "fefe",
                TwitterContextId = twitterContext.Id,
                CreatedAt = DateTime.UtcNow,
            };
            twitterCommentBO.InsertAsync(twitterComment2).Wait();

            var twitterComment3 = new TwitterComment
            {
                Id = Guid.NewGuid(),
                CommentId = "tk3",
                Content = "@_NeverForgetBot Tweet3",
                CommentDate = DateTime.UtcNow,
                Author = "Author3",
                IsDeleted = false,
                Link = "Link",
                MediaLink = "Link2",
                ReplyToId = "fefe",
                TwitterContextId = twitterContext.Id,
                CreatedAt = DateTime.UtcNow,
            };
            twitterCommentBO.InsertAsync(twitterComment3).Wait();

            var twitterContextBMList = twitterCommentBO.GetAllAsync().Result.Result;

            Assert.IsTrue(twitterContextBMList.Count == 3);
        }

        [TestMethod]
        public void TestDeleteTwitter()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase().Result;
                var resultCreate = context.CreateDatabase().Result;
            }

            var _requestTypeDao = new RequestTypeDao();
            RequestType defaultRequest = new RequestType { Id = (int)RequestTypeEnum.Default, Name = "Default" };
            _requestTypeDao.InsertAsync(defaultRequest).Wait();

            var twitterContextDAO = new TwitterContextDao();
            var twitterCommentDAO = new TwitterCommentDao();
            var mockLogger = new Mock<ILogger<DbOperationExecutor>>();
            var opExecutor = new DbOperationExecutor(mockLogger.Object);
            var twitterContextBO = new TwitterContextBo(twitterContextDAO, opExecutor);
            var twitterCommentBO = new TwitterCommentBo(twitterCommentDAO, opExecutor);

            var twitterContext = new TwitterContext
            {
                Id = Guid.NewGuid(),
                RequestTypeId = 0,
                RequestType = defaultRequest,
                CreatedAt = DateTime.UtcNow,
            };
            twitterContextBO.InsertAsync(twitterContext).Wait();

            var twitterComment = new TwitterComment
            {
                Id = Guid.NewGuid(),
                CommentId = "tk1",
                Content = "@_NeverForgetBot Tweet1",
                CommentDate = DateTime.UtcNow,
                Author = "Author1",
                IsDeleted = false,
                Link = "Link",
                MediaLink = "Link2",
                ReplyToId = "fefe",
                TwitterContextId = twitterContext.Id,
                CreatedAt = DateTime.UtcNow,

            };
            twitterCommentBO.InsertAsync(twitterComment).Wait();

            var twitterComment2 = new TwitterComment
            {
                Id = Guid.NewGuid(),
                CommentId = "tk2",
                Content = "@_NeverForgetBot Tweet2",
                CommentDate = DateTime.UtcNow,
                Author = "Author2",
                IsDeleted = false,
                Link = "Link",
                MediaLink = "Link2",
                ReplyToId = "fefe",
                TwitterContextId = twitterContext.Id,
                CreatedAt = DateTime.UtcNow,
            };
            twitterCommentBO.InsertAsync(twitterComment2).Wait();

            var twitterComment3 = new TwitterComment
            {
                Id = Guid.NewGuid(),
                CommentId = "tk3",
                Content = "@_NeverForgetBot Tweet3",
                CommentDate = DateTime.UtcNow,
                Author = "Author3",
                IsDeleted = false,
                Link = "Link",
                MediaLink = "Link2",
                ReplyToId = "fefe",
                TwitterContextId = twitterContext.Id,
                CreatedAt = DateTime.UtcNow,
            };
            twitterCommentBO.InsertAsync(twitterComment3).Wait();

            twitterCommentBO.DeleteAsync(twitterComment3).Wait();

            Assert.IsTrue(twitterComment3.IsDeleted == true);
        }
        #endregion
    }
}
