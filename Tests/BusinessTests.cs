using BlockBase.Dapps.NeverForget.Business.BusinessModels;
using BlockBase.Dapps.NeverForget.Business.BusinessObjects;
using BlockBase.Dapps.NeverForget.Common.Enums;
using BlockBase.Dapps.NeverForget.Data.Context;
using BlockBase.Dapps.NeverForget.Data.Entities;
using BlockBase.Dapps.NeverForget.DataAccess.DataAccessModels;
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
        #region Recreate Database
        //using (var context = new NeverForgetBotDbContext())
        //{
        //    var resultDrop = context.DropDatabase();
        //    var resultCreate = context.CreateDatabase();
        //}

        //var _requestTypeDao = new RequestTypeDataAccessObject();

        //#region Build RequestType Table
        //RequestType defaultRequest = new RequestType { Id = (int)RequestTypeEnum.Default, Name = "Default" };
        //RequestType commentRequest = new RequestType { Id = (int)RequestTypeEnum.Comment, Name = "Comment" };
        //RequestType postRequest = new RequestType { Id = (int)RequestTypeEnum.Post, Name = "Post" };

        //_requestTypeDao.InsertAsync(defaultRequest).Wait();
        //_requestTypeDao.InsertAsync(commentRequest).Wait();
        //_requestTypeDao.InsertAsync(postRequest).Wait();
        //#endregion
        #endregion

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
            var genericDAO = new GenericDataAccessObject();
            var logger = new Mock<ILogger<BaseBusinessObject>>();
            var redditContextBO = new RedditContextBusinessObject(redditCommentDAO, redditSubmissionDAO, redditPoco, redditCollector, redditContextDAO, genericDAO, logger.Object);
            var redditCommentBO = new RedditCommentBusinessObject(redditCommentDAO, genericDAO, logger.Object);
            var redditSubmissionBO = new RedditSubmissionBusinessObject(redditSubmissionDAO, genericDAO, logger.Object);

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
            var genericDAO = new GenericDataAccessObject();
            var logger = new Mock<ILogger<BaseBusinessObject>>();
            var redditContextBO = new RedditContextBusinessObject(redditCommentDAO, redditSubmissionDAO, redditPoco, redditCollector, redditContextDAO, genericDAO, logger.Object);
            var redditCommentBO = new RedditCommentBusinessObject(redditCommentDAO, genericDAO, logger.Object);
            var redditSubmissionBO = new RedditSubmissionBusinessObject(redditSubmissionDAO, genericDAO, logger.Object);

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
            var genericDAO = new GenericDataAccessObject();
            var logger = new Mock<ILogger<BaseBusinessObject>>();
            var redditContextBO = new RedditContextBusinessObject(redditCommentDAO, redditSubmissionDAO, redditPoco, redditCollector, redditContextDAO, genericDAO, logger.Object);
            var redditCommentBO = new RedditCommentBusinessObject(redditCommentDAO, genericDAO, logger.Object);
            var redditSubmissionBO = new RedditSubmissionBusinessObject(redditSubmissionDAO, genericDAO, logger.Object);

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
            var genericDAO = new GenericDataAccessObject();
            var logger = new Mock<ILogger<BaseBusinessObject>>();
            var redditContextBO = new RedditContextBusinessObject(redditCommentDAO, redditSubmissionDAO, redditPoco, redditCollector, redditContextDAO, genericDAO, logger.Object);
            var redditCommentBO = new RedditCommentBusinessObject(redditCommentDAO, genericDAO, logger.Object);
            var redditSubmissionBO = new RedditSubmissionBusinessObject(redditSubmissionDAO, genericDAO, logger.Object);

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


            Assert.IsTrue(redditComment3.IsDeleted == true);
        }

        [TestMethod]
        public void TestGetAllDeletedReddit()
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
            var genericDAO = new GenericDataAccessObject();
            var logger = new Mock<ILogger<BaseBusinessObject>>();
            var redditContextBO = new RedditContextBusinessObject(redditCommentDAO, redditSubmissionDAO, redditPoco, redditCollector, redditContextDAO, genericDAO, logger.Object);
            var redditCommentBO = new RedditCommentBusinessObject(redditCommentDAO, genericDAO, logger.Object);
            var redditSubmissionBO = new RedditSubmissionBusinessObject(redditSubmissionDAO, genericDAO, logger.Object);

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



            Assert.IsTrue(redditContextBMList.Count() == 1);
        }
        #endregion


        #region Twitter

        #endregion
    }
}
