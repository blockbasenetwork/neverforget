using BlockBase.Dapps.NeverForget.Common.Enums;
using BlockBase.Dapps.NeverForget.Data.Context;
using BlockBase.Dapps.NeverForget.Data.Entities;
using BlockBase.Dapps.NeverForget.DataAccess.DataAccessObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BlockBase.Dapps.NeverForget.Tests
{
    [TestClass]
    public class DataAccessTests
    {
        #region Recreate Database
        //using (var context = new NeverForgetBotDbContext())
        //{
        //    var resultDrop = context.DropDatabase();
        //    var resultCreate = context.CreateDatabase();
        //}

        //var _requestTypeDao = new RequestTypeDao();
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

            var redditContextDao = new RedditContextDataAccessObject();
            var redditCommentDao = new RedditCommentDataAccessObject();
            var redditSubmissionDao = new RedditSubmissionDataAccessObject();

            var redditContext = new RedditContext { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, RequestTypeId = defaultRequest.Id };

            var redditComment = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            var redditSubmission = new RedditSubmission { Id = Guid.NewGuid(), SubmissionId = "t3_qualquercoisa", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", SubmissionDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", RedditContextId = redditContext.Id, Title = "Test" };




            redditContextDao.InsertAsync(redditContext).Wait();
            redditCommentDao.InsertAsync(redditComment).Wait();
            redditSubmissionDao.InsertAsync(redditSubmission).Wait();


            var resGet = redditCommentDao.GetAsync(redditComment.Id).Result;



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

            var redditContextDao = new RedditContextDataAccessObject();
            var redditCommentDao = new RedditCommentDataAccessObject();
            var redditSubmissionDao = new RedditSubmissionDataAccessObject();

            var redditContext = new RedditContext { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, RequestTypeId = defaultRequest.Id };
            redditContextDao.InsertAsync(redditContext).Wait();

            var redditComment = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentDao.InsertAsync(redditComment).Wait();
            var redditComment2 = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk2", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentDao.InsertAsync(redditComment2).Wait();
            var redditComment3 = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk3", Author = "Ator", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentDao.InsertAsync(redditComment3).Wait();

            var redditSubmission = new RedditSubmission { Id = Guid.NewGuid(), SubmissionId = "t3_qualquercoisa", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", SubmissionDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", RedditContextId = redditContext.Id, Title = "Test" };
            redditSubmissionDao.InsertAsync(redditSubmission).Wait();


            var redditContextList = redditCommentDao.GetAllAsync().Result;

            Assert.IsTrue(redditContextList.Count == 3);
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

            var redditContextDao = new RedditContextDataAccessObject();
            var redditCommentDao = new RedditCommentDataAccessObject();
            var redditSubmissionDao = new RedditSubmissionDataAccessObject();

            var redditContext = new RedditContext { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, RequestTypeId = defaultRequest.Id };

            var redditComment = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };

            var redditSubmission = new RedditSubmission { Id = Guid.NewGuid(), SubmissionId = "t3_qualquercoisa", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", SubmissionDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", RedditContextId = redditContext.Id, Title = "Test" };



            redditContextDao.InsertAsync(redditContext).Wait();
            redditCommentDao.InsertAsync(redditComment).Wait();
            redditSubmissionDao.InsertAsync(redditSubmission).Wait();

            var resGet = redditCommentDao.GetAsync(redditComment.Id).Result;
            resGet.Author = "NovoAutor";
            redditCommentDao.UpdateAsync(resGet).Wait();



            Assert.IsTrue(resGet.Author == "NovoAutor");
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

            var redditContextDao = new RedditContextDataAccessObject();
            var redditCommentDao = new RedditCommentDataAccessObject();
            var redditSubmissionDao = new RedditSubmissionDataAccessObject();

            var redditContext = new RedditContext { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, RequestTypeId = defaultRequest.Id };
            redditContextDao.InsertAsync(redditContext).Wait();

            var redditComment = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentDao.InsertAsync(redditComment).Wait();
            var redditComment2 = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk2", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentDao.InsertAsync(redditComment2).Wait();
            var redditComment3 = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk3", Author = "Ator", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentDao.InsertAsync(redditComment3).Wait();

            var redditSubmission = new RedditSubmission { Id = Guid.NewGuid(), SubmissionId = "t3_qualquercoisa", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", SubmissionDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", RedditContextId = redditContext.Id, Title = "Test" };
            redditSubmissionDao.InsertAsync(redditSubmission).Wait();


            redditCommentDao.DeleteAsync(redditComment3).Wait();



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

            var redditContextDao = new RedditContextDataAccessObject();
            var redditCommentDao = new RedditCommentDataAccessObject();
            var redditSubmissionDao = new RedditSubmissionDataAccessObject();

            var redditContext = new RedditContext { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, RequestTypeId = defaultRequest.Id };
            redditContextDao.InsertAsync(redditContext).Wait();

            var redditComment = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentDao.InsertAsync(redditComment).Wait();
            var redditComment2 = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk2", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentDao.InsertAsync(redditComment2).Wait();
            var redditComment3 = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk3", Author = "Ator", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentDao.InsertAsync(redditComment3).Wait();

            var redditSubmission = new RedditSubmission { Id = Guid.NewGuid(), SubmissionId = "t3_qualquercoisa", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", SubmissionDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", RedditContextId = redditContext.Id, Title = "Test" };
            redditSubmissionDao.InsertAsync(redditSubmission).Wait();


            redditCommentDao.DeleteAsync(redditComment3).Wait();
            var redditContextList = redditCommentDao.GetAllAsync().Result;
            var redditContextListDeleted = redditCommentDao.GetAllDeletedAsync().Result;



            Assert.IsTrue(redditContextList.Count == 2 && redditContextListDeleted.Count == 1);
        }
        #endregion


        #region Twitter

        #endregion
    }
}
