using BlockBase.Dapps.NeverForget.Business.BusinessModels;
using BlockBase.Dapps.NeverForget.Business.BusinessObjects;
using BlockBase.Dapps.NeverForget.Common.Enums;
using BlockBase.Dapps.NeverForget.Data.Context;
using BlockBase.Dapps.NeverForget.Data.Entities;
using BlockBase.Dapps.NeverForget.DataAccess.DataAccessObjects;
using BlockBase.Dapps.NeverForget.Services.API;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace BlockBase.Dapps.NeverForget.Tests
{
    [TestClass]
    public class BusinessTests
    {
        #region Reddit
        [TestMethod]
        public void TestInsertAndGetReddit()
        {
            #region Recreate Database
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase();
                var resultCreate = context.CreateDatabase();
            }

            var _requestTypeDao = new RequestTypeDataAccessObject();

            #region Build RequestType Table
            RequestType defaultRequest = new RequestType { Id = (int)RequestTypeEnum.Default, Name = "Default" };
            RequestType commentRequest = new RequestType { Id = (int)RequestTypeEnum.Comment, Name = "Comment" };
            RequestType postRequest = new RequestType { Id = (int)RequestTypeEnum.Post, Name = "Post" };

            _requestTypeDao.InsertAsync(defaultRequest).Wait();
            _requestTypeDao.InsertAsync(commentRequest).Wait();
            _requestTypeDao.InsertAsync(postRequest).Wait();
            #endregion
            #endregion


            var redditContextDAO = new RedditContextDataAccessObject();
            var redditCommentDAO = new RedditCommentDataAccessObject();
            var redditSubmissionDAO = new RedditSubmissionDataAccessObject();
            var redditCollector = new RedditCollector();
            var redditPoco = new RedditContextPocoDataAccessObject();
            var logger = new Mock<ILogger<BaseBusinessObject>>();
            var redditContextBO = new RedditContextBusinessObject(redditCommentDAO, redditSubmissionDAO, redditPoco, redditCollector, redditContextDAO, logger.Object);
            var redditCommentBO = new RedditCommentBusinessObject(redditCommentDAO, logger.Object);
            var redditSubmissionBO = new RedditSubmissionBusinessObject(redditSubmissionDAO, logger.Object);

            var redditContext = new RedditContext { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, RequestTypeId = defaultRequest.Id };
            redditContextBO.InsertAsync(redditContext).Wait();

            var redditComment = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentBO.InsertAsync(redditComment).Wait();

            var redditSubmission = new RedditSubmission { Id = Guid.NewGuid(), SubmissionId = "t3_qualquercoisa", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", SubmissionDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", RedditContextId = redditContext.Id, Title = "Test" };
            redditSubmissionBO.InsertAsync(redditSubmission).Wait();

            var resGet = redditCommentBO.GetAsync(redditComment.Id);

            Assert.IsTrue(resGet != null);
        }

        [TestMethod]
        public void TestGetAllReddit()
        {
            #region Recreate Database
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase();
                var resultCreate = context.CreateDatabase();
            }

            var _requestTypeDao = new RequestTypeDataAccessObject();

            #region Build RequestType Table
            RequestType defaultRequest = new RequestType { Id = (int)RequestTypeEnum.Default, Name = "Default" };
            RequestType commentRequest = new RequestType { Id = (int)RequestTypeEnum.Comment, Name = "Comment" };
            RequestType postRequest = new RequestType { Id = (int)RequestTypeEnum.Post, Name = "Post" };

            _requestTypeDao.InsertAsync(defaultRequest).Wait();
            _requestTypeDao.InsertAsync(commentRequest).Wait();
            _requestTypeDao.InsertAsync(postRequest).Wait();
            #endregion
            #endregion

            var redditContextDAO = new RedditContextDataAccessObject();
            var redditCommentDAO = new RedditCommentDataAccessObject();
            var redditSubmissionDAO = new RedditSubmissionDataAccessObject();
            var redditCollector = new RedditCollector();
            var redditPoco = new RedditContextPocoDataAccessObject();
            var logger = new Mock<ILogger<BaseBusinessObject>>();
            var redditContextBO = new RedditContextBusinessObject(redditCommentDAO, redditSubmissionDAO, redditPoco, redditCollector, redditContextDAO, logger.Object);
            var redditCommentBO = new RedditCommentBusinessObject(redditCommentDAO, logger.Object);
            var redditSubmissionBO = new RedditSubmissionBusinessObject(redditSubmissionDAO, logger.Object);

            var redditContext = new RedditContext { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, RequestTypeId = defaultRequest.Id };
            redditContextBO.InsertAsync(redditContext).Wait();

            var redditComment = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentBO.InsertAsync(redditComment).Wait();
            var redditComment2 = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk2", Author = "Autor", SubReddit = "Testando", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentBO.InsertAsync(redditComment2).Wait();
            var redditComment3 = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk3", Author = "Ator", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentBO.InsertAsync(redditComment3).Wait();

            var redditSubmission = new RedditSubmission { Id = Guid.NewGuid(), SubmissionId = "t3_qualquercoisa", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", SubmissionDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", RedditContextId = redditContext.Id, Title = "Test" };
            redditSubmissionBO.InsertAsync(redditSubmission).Wait();


            redditCommentBO.DeleteAsync(redditComment3).Wait();
            var redditContextBMList = redditCommentBO.ListAsync().Result.Result;



            Assert.IsTrue(redditContextBMList.Count() == 2);
        }

        [TestMethod]
        public void TestUpdateReddit()
        {
            #region Recreate Database
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase();
                var resultCreate = context.CreateDatabase();
            }

            var _requestTypeDao = new RequestTypeDataAccessObject();

            #region Build RequestType Table
            RequestType defaultRequest = new RequestType { Id = (int)RequestTypeEnum.Default, Name = "Default" };
            RequestType commentRequest = new RequestType { Id = (int)RequestTypeEnum.Comment, Name = "Comment" };
            RequestType postRequest = new RequestType { Id = (int)RequestTypeEnum.Post, Name = "Post" };

            _requestTypeDao.InsertAsync(defaultRequest).Wait();
            _requestTypeDao.InsertAsync(commentRequest).Wait();
            _requestTypeDao.InsertAsync(postRequest).Wait();
            #endregion
            #endregion

            var redditContextDAO = new RedditContextDataAccessObject();
            var redditCommentDAO = new RedditCommentDataAccessObject();
            var redditSubmissionDAO = new RedditSubmissionDataAccessObject();
            var redditCollector = new RedditCollector();
            var redditPoco = new RedditContextPocoDataAccessObject();
            var logger = new Mock<ILogger<BaseBusinessObject>>();
            var redditContextBO = new RedditContextBusinessObject(redditCommentDAO, redditSubmissionDAO, redditPoco, redditCollector, redditContextDAO, logger.Object);
            var redditCommentBO = new RedditCommentBusinessObject(redditCommentDAO, logger.Object);
            var redditSubmissionBO = new RedditSubmissionBusinessObject(redditSubmissionDAO, logger.Object);

            var redditContext = new RedditContext { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, RequestTypeId = defaultRequest.Id };
            redditContextBO.InsertAsync(redditContext).Wait();

            var redditComment = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentBO.InsertAsync(redditComment).Wait();
            var redditComment2 = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk2", Author = "Autor", SubReddit = "Testando", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentBO.InsertAsync(redditComment2).Wait();
            var redditComment3 = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk3", Author = "Ator", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentBO.InsertAsync(redditComment3).Wait();

            var redditSubmission = new RedditSubmission { Id = Guid.NewGuid(), SubmissionId = "t3_qualquercoisa", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", SubmissionDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", RedditContextId = redditContext.Id, Title = "Test" };
            redditSubmissionBO.InsertAsync(redditSubmission).Wait();
            ;

            var resGet = redditCommentBO.GetAsync(redditComment.Id);
            resGet.Result.Result.Author = "NovoAutor";
            redditCommentBO.UpdateAsync(resGet.Result.Result).Wait();



            Assert.IsTrue(resGet.Result.Result.Author == "NovoAutor");
        }

        [TestMethod]
        public void TestDeleteReddit()
        {
            #region Recreate Database
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase();
                var resultCreate = context.CreateDatabase();
            }

            var _requestTypeDao = new RequestTypeDataAccessObject();

            #region Build RequestType Table
            RequestType defaultRequest = new RequestType { Id = (int)RequestTypeEnum.Default, Name = "Default" };
            RequestType commentRequest = new RequestType { Id = (int)RequestTypeEnum.Comment, Name = "Comment" };
            RequestType postRequest = new RequestType { Id = (int)RequestTypeEnum.Post, Name = "Post" };

            _requestTypeDao.InsertAsync(defaultRequest).Wait();
            _requestTypeDao.InsertAsync(commentRequest).Wait();
            _requestTypeDao.InsertAsync(postRequest).Wait();
            #endregion
            #endregion

            var redditContextDAO = new RedditContextDataAccessObject();
            var redditCommentDAO = new RedditCommentDataAccessObject();
            var redditSubmissionDAO = new RedditSubmissionDataAccessObject();
            var redditCollector = new RedditCollector();
            var redditPoco = new RedditContextPocoDataAccessObject();
            var logger = new Mock<ILogger<BaseBusinessObject>>();
            var redditContextBO = new RedditContextBusinessObject(redditCommentDAO, redditSubmissionDAO, redditPoco, redditCollector, redditContextDAO, logger.Object);
            var redditCommentBO = new RedditCommentBusinessObject(redditCommentDAO, logger.Object);
            var redditSubmissionBO = new RedditSubmissionBusinessObject(redditSubmissionDAO, logger.Object);

            var redditContext = new RedditContext { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, RequestTypeId = defaultRequest.Id };
            redditContextBO.InsertAsync(redditContext).Wait();

            var redditComment = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentBO.InsertAsync(redditComment).Wait();
            var redditComment2 = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk2", Author = "Autor", SubReddit = "Testando", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentBO.InsertAsync(redditComment2).Wait();
            var redditComment3 = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk3", Author = "Ator", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentBO.InsertAsync(redditComment3).Wait();

            var redditSubmission = new RedditSubmission { Id = Guid.NewGuid(), SubmissionId = "t3_qualquercoisa", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", SubmissionDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", RedditContextId = redditContext.Id, Title = "Test" };
            redditSubmissionBO.InsertAsync(redditSubmission).Wait();

            var resGet = redditCommentBO.GetAsync(redditComment.Id).Result.Result;
            redditCommentBO.DeleteAsync(resGet).Wait();

            Assert.IsTrue(resGet.IsDeleted == true);
        }

        #endregion


        #region Twitter

        [TestMethod]
        public void TestInsertAndGetTwitter()
        {
            #region Recreate Database
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase();
                var resultCreate = context.CreateDatabase();
            }

            var _requestTypeDao = new RequestTypeDataAccessObject();

            #region Build RequestType Table
            RequestType defaultRequest = new RequestType { Id = (int)RequestTypeEnum.Default, Name = "Default" };
            RequestType commentRequest = new RequestType { Id = (int)RequestTypeEnum.Comment, Name = "Comment" };
            RequestType postRequest = new RequestType { Id = (int)RequestTypeEnum.Post, Name = "Post" };

            _requestTypeDao.InsertAsync(defaultRequest).Wait();
            _requestTypeDao.InsertAsync(commentRequest).Wait();
            _requestTypeDao.InsertAsync(postRequest).Wait();
            #endregion
            #endregion

            var twitterContextDAO = new TwitterContextDataAccessObject();
            var twitterCommentDAO = new TwitterCommentDataAccessObject();
            var twitterSubmissionDAO = new TwitterSubmissionDataAccessObject();
            var twitterCollector = new TwitterCollector();
            var twitterPoco = new TwitterContextPocoDataAccessObject();
            var logger = new Mock<ILogger<BaseBusinessObject>>();
            var twitterContextBO = new TwitterContextBusinessObject(twitterContextDAO, twitterCommentDAO, twitterSubmissionDAO, twitterPoco, twitterCollector, logger.Object);
            var twitterCommentBO = new TwitterCommentBusinessObject(twitterCommentDAO, logger.Object);
            var twitterSubmissionBO = new TwitterSubmissionBusinessObject(twitterSubmissionDAO, logger.Object);

            var twitterContext = new TwitterContext
            {
                Id = Guid.NewGuid(),
                RequestTypeId = defaultRequest.Id,
                CreatedAt = DateTime.UtcNow,
            };

            var twitterComment = new TwitterComment
            {
                Id = Guid.NewGuid(),
                CommentId = "tk1",
                Content = "@_NeverForgetBot Tweet1",
                CommentDate = DateTime.UtcNow,
                Author = "Author1",
                Link = "Link",
                MediaLink = "Link2",
                ReplyToId = "s1",
                TwitterContextId = twitterContext.Id,
                CreatedAt = DateTime.UtcNow,
            };

            var twitterSubmission = new TwitterSubmission
            {
                Id = Guid.NewGuid(),
                SubmissionId = "s1",
                Content = "Main Tweet",
                SubmissionDate = DateTime.UtcNow,
                Author = "Tweeter1",
                Link = "Link",
                MediaLink = "Link2",
                TwitterContextId = twitterContext.Id,
                CreatedAt = DateTime.UtcNow,
            };

            twitterContextBO.InsertAsync(twitterContext).Wait();
            twitterCommentBO.InsertAsync(twitterComment).Wait();
            twitterSubmissionBO.InsertAsync(twitterSubmission).Wait();

            var resGetCon = twitterContextBO.GetAsync(twitterContext.Id);
            var resGetCom = twitterCommentBO.GetAsync(twitterComment.Id);
            var resGetSub = twitterSubmissionBO.GetAsync(twitterSubmission.Id);

            Assert.IsTrue(resGetCon != null && resGetCom != null && resGetSub != null);
        }

        [TestMethod]
        public void TestGetAllTwitter()
        {
            #region Recreate Database
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase();
                var resultCreate = context.CreateDatabase();
            }

            var _requestTypeDao = new RequestTypeDataAccessObject();

            #region Build RequestType Table
            RequestType defaultRequest = new RequestType { Id = (int)RequestTypeEnum.Default, Name = "Default" };
            RequestType commentRequest = new RequestType { Id = (int)RequestTypeEnum.Comment, Name = "Comment" };
            RequestType postRequest = new RequestType { Id = (int)RequestTypeEnum.Post, Name = "Post" };

            _requestTypeDao.InsertAsync(defaultRequest).Wait();
            _requestTypeDao.InsertAsync(commentRequest).Wait();
            _requestTypeDao.InsertAsync(postRequest).Wait();
            #endregion
            #endregion

            var twitterContextDAO = new TwitterContextDataAccessObject();
            var twitterCommentDAO = new TwitterCommentDataAccessObject();
            var twitterSubmissionDAO = new TwitterSubmissionDataAccessObject();
            var twitterCollector = new TwitterCollector();
            var twitterPoco = new TwitterContextPocoDataAccessObject();
            var logger = new Mock<ILogger<BaseBusinessObject>>();
            var twitterContextBO = new TwitterContextBusinessObject(twitterContextDAO, twitterCommentDAO, twitterSubmissionDAO, twitterPoco, twitterCollector, logger.Object);
            var twitterCommentBO = new TwitterCommentBusinessObject(twitterCommentDAO, logger.Object);
            var twitterSubmissionBO = new TwitterSubmissionBusinessObject(twitterSubmissionDAO, logger.Object);

            var twitterContext = new TwitterContext
            {
                Id = Guid.NewGuid(),
                RequestTypeId = defaultRequest.Id,
                CreatedAt = DateTime.UtcNow,
            };

            var twitterComment = new TwitterComment
            {
                Id = Guid.NewGuid(),
                CommentId = "tk1",
                Content = "@_NeverForgetBot Tweet1",
                CommentDate = DateTime.UtcNow,
                Author = "Author1",
                Link = "Link",
                MediaLink = "Link2",
                ReplyToId = "s1",
                TwitterContextId = twitterContext.Id,
                CreatedAt = DateTime.UtcNow,
            };

            var twitterSubmission = new TwitterSubmission
            {
                Id = Guid.NewGuid(),
                SubmissionId = "s1",
                Content = "Main Tweet",
                SubmissionDate = DateTime.UtcNow,
                Author = "Tweeter1",
                Link = "Link",
                MediaLink = "Link2",
                TwitterContextId = twitterContext.Id,
                CreatedAt = DateTime.UtcNow,
            };

            twitterContextBO.InsertAsync(twitterContext).Wait();
            twitterCommentBO.InsertAsync(twitterComment).Wait();
            twitterSubmissionBO.InsertAsync(twitterSubmission).Wait();

            var resGetCon = twitterContextBO.ListAsync().Result.Result;
            var resGetCom = twitterCommentBO.ListAsync().Result.Result;
            var resGetSub = twitterSubmissionBO.ListAsync().Result.Result;

            Assert.IsTrue(resGetCon.Count() == 1 && resGetCom.Count() == 1 && resGetSub.Count() == 1);
        }

        [TestMethod]
        public void TestUpdateTwitter()
        {
            #region Recreate Database
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase();
                var resultCreate = context.CreateDatabase();
            }

            var _requestTypeDao = new RequestTypeDataAccessObject();

            #region Build RequestType Table
            RequestType defaultRequest = new RequestType { Id = (int)RequestTypeEnum.Default, Name = "Default" };
            RequestType commentRequest = new RequestType { Id = (int)RequestTypeEnum.Comment, Name = "Comment" };
            RequestType postRequest = new RequestType { Id = (int)RequestTypeEnum.Post, Name = "Post" };

            _requestTypeDao.InsertAsync(defaultRequest).Wait();
            _requestTypeDao.InsertAsync(commentRequest).Wait();
            _requestTypeDao.InsertAsync(postRequest).Wait();
            #endregion
            #endregion

            var twitterContextDAO = new TwitterContextDataAccessObject();
            var twitterCommentDAO = new TwitterCommentDataAccessObject();
            var twitterSubmissionDAO = new TwitterSubmissionDataAccessObject();
            var twitterCollector = new TwitterCollector();
            var twitterPoco = new TwitterContextPocoDataAccessObject();
            var logger = new Mock<ILogger<BaseBusinessObject>>();
            var twitterContextBO = new TwitterContextBusinessObject(twitterContextDAO, twitterCommentDAO, twitterSubmissionDAO, twitterPoco, twitterCollector, logger.Object);
            var twitterCommentBO = new TwitterCommentBusinessObject(twitterCommentDAO, logger.Object);
            var twitterSubmissionBO = new TwitterSubmissionBusinessObject(twitterSubmissionDAO, logger.Object);

            var twitterContext = new TwitterContext
            {
                Id = Guid.NewGuid(),
                RequestTypeId = defaultRequest.Id,
                CreatedAt = DateTime.UtcNow,
            };

            var twitterComment = new TwitterComment
            {
                Id = Guid.NewGuid(),
                CommentId = "tk1",
                Content = "@_NeverForgetBot Tweet1",
                CommentDate = DateTime.UtcNow,
                Author = "Author1",
                Link = "Link",
                MediaLink = "Link2",
                ReplyToId = "s1",
                TwitterContextId = twitterContext.Id,
                CreatedAt = DateTime.UtcNow,
            };

            var twitterSubmission = new TwitterSubmission
            {
                Id = Guid.NewGuid(),
                SubmissionId = "s1",
                Content = "Main Tweet",
                SubmissionDate = DateTime.UtcNow,
                Author = "Tweeter1",
                Link = "Link",
                MediaLink = "Link2",
                TwitterContextId = twitterContext.Id,
                CreatedAt = DateTime.UtcNow,
            };

            twitterContextBO.InsertAsync(twitterContext).Wait();
            twitterCommentBO.InsertAsync(twitterComment).Wait();
            twitterSubmissionBO.InsertAsync(twitterSubmission).Wait();

            twitterContext.RequestTypeId = commentRequest.Id;
            twitterComment.Link = "NewLink";
            twitterSubmission.Link = "NewLink";

            twitterContextBO.UpdateAsync(twitterContext).Wait();
            twitterCommentBO.UpdateAsync(twitterComment).Wait();
            twitterSubmissionBO.UpdateAsync(twitterSubmission).Wait();

            var resGetCon = twitterContextBO.GetAsync(twitterContext.Id).Result.Result;
            var resGetCom = twitterCommentBO.GetAsync(twitterComment.Id).Result.Result;
            var resGetSub = twitterSubmissionBO.GetAsync(twitterSubmission.Id).Result.Result;

            Assert.IsTrue(resGetCon.RequestTypeId == commentRequest.Id && resGetCom.Link == "NewLink" && resGetSub.Link == "NewLink");
        }

        [TestMethod]
        public void TestDeleteTwitter()
        {
            #region Recreate Database
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase();
                var resultCreate = context.CreateDatabase();
            }

            var _requestTypeDao = new RequestTypeDataAccessObject();

            #region Build RequestType Table
            RequestType defaultRequest = new RequestType { Id = (int)RequestTypeEnum.Default, Name = "Default" };
            RequestType commentRequest = new RequestType { Id = (int)RequestTypeEnum.Comment, Name = "Comment" };
            RequestType postRequest = new RequestType { Id = (int)RequestTypeEnum.Post, Name = "Post" };

            _requestTypeDao.InsertAsync(defaultRequest).Wait();
            _requestTypeDao.InsertAsync(commentRequest).Wait();
            _requestTypeDao.InsertAsync(postRequest).Wait();
            #endregion
            #endregion

            var twitterContextDAO = new TwitterContextDataAccessObject();
            var twitterCommentDAO = new TwitterCommentDataAccessObject();
            var twitterSubmissionDAO = new TwitterSubmissionDataAccessObject();
            var twitterCollector = new TwitterCollector();
            var twitterPoco = new TwitterContextPocoDataAccessObject();
            var logger = new Mock<ILogger<BaseBusinessObject>>();
            var twitterContextBO = new TwitterContextBusinessObject(twitterContextDAO, twitterCommentDAO, twitterSubmissionDAO, twitterPoco, twitterCollector, logger.Object);
            var twitterCommentBO = new TwitterCommentBusinessObject(twitterCommentDAO, logger.Object);
            var twitterSubmissionBO = new TwitterSubmissionBusinessObject(twitterSubmissionDAO, logger.Object);

            var twitterContext = new TwitterContext
            {
                Id = Guid.NewGuid(),
                RequestTypeId = defaultRequest.Id,
                CreatedAt = DateTime.UtcNow,
            };

            var twitterComment = new TwitterComment
            {
                Id = Guid.NewGuid(),
                CommentId = "tk1",
                Content = "@_NeverForgetBot Tweet1",
                CommentDate = DateTime.UtcNow,
                Author = "Author1",
                Link = "Link",
                MediaLink = "Link2",
                ReplyToId = "s1",
                TwitterContextId = twitterContext.Id,
                CreatedAt = DateTime.UtcNow,
            };

            var twitterSubmission = new TwitterSubmission
            {
                Id = Guid.NewGuid(),
                SubmissionId = "s1",
                Content = "Main Tweet",
                SubmissionDate = DateTime.UtcNow,
                Author = "Tweeter1",
                Link = "Link",
                MediaLink = "Link2",
                TwitterContextId = twitterContext.Id,
                CreatedAt = DateTime.UtcNow,
            };

            twitterContextBO.InsertAsync(twitterContext).Wait();
            twitterCommentBO.InsertAsync(twitterComment).Wait();
            twitterSubmissionBO.InsertAsync(twitterSubmission).Wait();

            twitterContextBO.DeleteAsync(twitterContext).Wait();
            twitterCommentBO.DeleteAsync(twitterComment).Wait();
            twitterSubmissionBO.DeleteAsync(twitterSubmission).Wait();

            var resGetCon = twitterContextBO.GetAsync(twitterContext.Id).Result.Result;
            var resGetCom = twitterCommentBO.GetAsync(twitterComment.Id).Result.Result;
            var resGetSub = twitterSubmissionBO.GetAsync(twitterSubmission.Id).Result.Result;

            Assert.IsTrue(resGetCon == null && resGetCom == null && resGetSub == null);
        }

        #endregion
    }
}
