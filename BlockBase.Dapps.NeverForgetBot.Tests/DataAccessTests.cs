using BlockBase.Dapps.NeverForgetBot.Common.Enums;
using BlockBase.Dapps.NeverForgetBot.Dal;
using BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess;
using BlockBase.Dapps.NeverForgetBot.Data.Context;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BlockBase.Dapps.NeverForgetBot.Tests
{
    [TestClass]
    public class DataAccessTests
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
            #region Build RequestType Table
            RequestType defaultRequest = new RequestType { Id = (int)RequestTypeEnum.Default, Name = "Default" };
            RequestType commentRequest = new RequestType { Id = (int)RequestTypeEnum.Comment, Name = "Comment" };
            RequestType threadRequest = new RequestType { Id = (int)RequestTypeEnum.Thread, Name = "Thread" };
            RequestType postRequest = new RequestType { Id = (int)RequestTypeEnum.Post, Name = "Post" };

            _requestTypeDao.InsertAsync(defaultRequest).Wait();
            _requestTypeDao.InsertAsync(commentRequest).Wait();
            _requestTypeDao.InsertAsync(threadRequest).Wait();
            _requestTypeDao.InsertAsync(postRequest).Wait();
            #endregion

            var redditContextDao = new RedditContextDao();
            var redditCommentDao = new RedditCommentDao();

            var redditContext = new RedditContext { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, RequestTypeId = defaultRequest.Id };

            var redditComment = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };


            redditContextDao.InsertAsync(redditContext).Wait();
            redditCommentDao.InsertAsync(redditComment).Wait();


            var resGet = redditCommentDao.GetAsync(redditComment.Id).Result;



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

            var _requestTypeDao = new RequestTypeDao();
            #region Build RequestType Table
            RequestType defaultRequest = new RequestType { Id = (int)RequestTypeEnum.Default, Name = "Default" };
            RequestType commentRequest = new RequestType { Id = (int)RequestTypeEnum.Comment, Name = "Comment" };
            RequestType threadRequest = new RequestType { Id = (int)RequestTypeEnum.Thread, Name = "Thread" };
            RequestType postRequest = new RequestType { Id = (int)RequestTypeEnum.Post, Name = "Post" };

            _requestTypeDao.InsertAsync(defaultRequest).Wait();
            _requestTypeDao.InsertAsync(commentRequest).Wait();
            _requestTypeDao.InsertAsync(threadRequest).Wait();
            _requestTypeDao.InsertAsync(postRequest).Wait();
            #endregion

            var redditContextDao = new RedditContextDao();
            var redditCommentDao = new RedditCommentDao();

            var redditContext = new RedditContext { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, RequestTypeId = defaultRequest.Id };
            redditContextDao.InsertAsync(redditContext).Wait();

            var redditComment = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentDao.InsertAsync(redditComment).Wait();
            var redditComment2 = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk2", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentDao.InsertAsync(redditComment2).Wait();
            var redditComment3 = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk3", Author = "Ator", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentDao.InsertAsync(redditComment3).Wait();


            var redditContextList = redditCommentDao.GetAllAsync().Result;

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

            var _requestTypeDao = new RequestTypeDao();
            #region Build RequestType Table
            RequestType defaultRequest = new RequestType { Id = (int)RequestTypeEnum.Default, Name = "Default" };
            RequestType commentRequest = new RequestType { Id = (int)RequestTypeEnum.Comment, Name = "Comment" };
            RequestType threadRequest = new RequestType { Id = (int)RequestTypeEnum.Thread, Name = "Thread" };
            RequestType postRequest = new RequestType { Id = (int)RequestTypeEnum.Post, Name = "Post" };

            _requestTypeDao.InsertAsync(defaultRequest).Wait();
            _requestTypeDao.InsertAsync(commentRequest).Wait();
            _requestTypeDao.InsertAsync(threadRequest).Wait();
            _requestTypeDao.InsertAsync(postRequest).Wait();
            #endregion

            var redditContextDao = new RedditContextDao();
            var redditCommentDao = new RedditCommentDao();

            var redditContext = new RedditContext { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, RequestTypeId = defaultRequest.Id };

            var redditComment = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };


            redditContextDao.InsertAsync(redditContext).Wait();
            redditCommentDao.InsertAsync(redditComment).Wait();


            var resGet = redditCommentDao.GetAsync(redditComment.Id).Result;
            resGet.Author = "NovoAutor";
            redditCommentDao.UpdateAsync(resGet).Wait();



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

            var _requestTypeDao = new RequestTypeDao();
            #region Build RequestType Table
            RequestType defaultRequest = new RequestType { Id = (int)RequestTypeEnum.Default, Name = "Default" };
            RequestType commentRequest = new RequestType { Id = (int)RequestTypeEnum.Comment, Name = "Comment" };
            RequestType threadRequest = new RequestType { Id = (int)RequestTypeEnum.Thread, Name = "Thread" };
            RequestType postRequest = new RequestType { Id = (int)RequestTypeEnum.Post, Name = "Post" };

            _requestTypeDao.InsertAsync(defaultRequest).Wait();
            _requestTypeDao.InsertAsync(commentRequest).Wait();
            _requestTypeDao.InsertAsync(threadRequest).Wait();
            _requestTypeDao.InsertAsync(postRequest).Wait();
            #endregion

            var redditContextDao = new RedditContextDao();
            var redditCommentDao = new RedditCommentDao();

            var redditContext = new RedditContext { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, RequestTypeId = defaultRequest.Id };
            redditContextDao.InsertAsync(redditContext).Wait();

            var redditComment = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentDao.InsertAsync(redditComment).Wait();
            var redditComment2 = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk2", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentDao.InsertAsync(redditComment2).Wait();
            var redditComment3 = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk3", Author = "Ator", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentDao.InsertAsync(redditComment3).Wait();


            redditCommentDao.DeleteAsync(redditComment3).Wait();



            Assert.IsTrue(redditComment3.IsDeleted == true);
        }

        [TestMethod]
        public void TestGetAllDeletedReddit()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase().Result;
                var resultCreate = context.CreateDatabase().Result;
            }

            var _requestTypeDao = new RequestTypeDao();
            #region Build RequestType Table
            RequestType defaultRequest = new RequestType { Id = (int)RequestTypeEnum.Default, Name = "Default" };
            RequestType commentRequest = new RequestType { Id = (int)RequestTypeEnum.Comment, Name = "Comment" };
            RequestType threadRequest = new RequestType { Id = (int)RequestTypeEnum.Thread, Name = "Thread" };
            RequestType postRequest = new RequestType { Id = (int)RequestTypeEnum.Post, Name = "Post" };

            _requestTypeDao.InsertAsync(defaultRequest).Wait();
            _requestTypeDao.InsertAsync(commentRequest).Wait();
            _requestTypeDao.InsertAsync(threadRequest).Wait();
            _requestTypeDao.InsertAsync(postRequest).Wait();
            #endregion

            var redditContextDao = new RedditContextDao();
            var redditCommentDao = new RedditCommentDao();

            var redditContext = new RedditContext { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, RequestTypeId = defaultRequest.Id };
            redditContextDao.InsertAsync(redditContext).Wait();

            var redditComment = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentDao.InsertAsync(redditComment).Wait();
            var redditComment2 = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk2", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentDao.InsertAsync(redditComment2).Wait();
            var redditComment3 = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk3", Author = "Ator", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentDao.InsertAsync(redditComment3).Wait();


            redditCommentDao.DeleteAsync(redditComment3).Wait();
            var redditContextList = redditCommentDao.GetAllAsync().Result;
            var redditContextListDeleted = redditCommentDao.GetAllDeletedAsync().Result;



            Assert.IsTrue(redditContextList.Count == 2 && redditContextListDeleted.Count == 1);
        }
        #endregion


        //#region Twitter
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


            var twitterDAO = new TwitterContextDao();
            var twitterCommentDao = new TwitterCommentDao();

            var twitterContext = new TwitterContext
            {
                Id = Guid.NewGuid(),
                RequestTypeId = 0,
                CreatedAt = DateTime.UtcNow,
            };
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
            twitterDAO.InsertAsync(twitterContext).Wait();
            twitterCommentDao.InsertAsync(twitterComment).Wait();


            var resGet = twitterCommentDao.GetAsync(twitterComment.Id).Result;

            Assert.IsTrue(resGet != null);
        }

        //[TestMethod]
        //public void TestGetAllTwitter()
        //{
        //    using (var context = new NeverForgetBotDbContext())
        //    {
        //        var resultDrop = context.DropDatabase().Result;
        //        var resultCreate = context.CreateDatabase().Result;
        //    }

        //    var _requestTypeDao = new RequestTypeDao();
        //    RequestType defaultRequest = new RequestType { Id = (int)RequestTypeEnum.Default, Name = "Default" };
        //    _requestTypeDao.InsertAsync(defaultRequest).Wait();


        //    var twitterDAO = new TwitterContextDao();
        //    var twitterCommentDao = new TwitterCommentDao();

        //    var twitterContext = new TwitterContext
        //    {
        //        Id = Guid.NewGuid(),
        //        RequestTypeId = 0,
        //        RequestType = defaultRequest,
        //        CreatedAt = DateTime.UtcNow,
        //    };
        //    var twitterComment = new TwitterComment
        //    {
        //        Id = Guid.NewGuid(),
        //        CommentId = "tk1",
        //        Content = "@_NeverForgetBot Tweet1",
        //        CommentDate = DateTime.UtcNow,
        //        Author = "Author1",
        //        IsDeleted = false,
        //        Link = "Link",
        //        MediaLink = "Link2",
        //        ReplyToId = "fefe",
        //        TwitterContextId = twitterContext.Id,
        //        CreatedAt = DateTime.UtcNow,

        //    };
        //    twitterDAO.InsertAsync(twitterContext).Wait();
        //    twitterCommentDao.InsertAsync(twitterComment).Wait();

        //    var twitterComment2 = new TwitterComment
        //    {
        //        Id = Guid.NewGuid(),
        //        CommentId = "tk2",
        //        Content = "@_NeverForgetBot Tweet2",
        //        CommentDate = DateTime.UtcNow,
        //        Author = "Author2",
        //        IsDeleted = false,
        //        Link = "Link",
        //        MediaLink = "Link2",
        //        ReplyToId = "fefe",
        //        TwitterContextId = twitterContext.Id,
        //        CreatedAt = DateTime.UtcNow,
        //    };
        //    twitterCommentDao.InsertAsync(twitterComment2).Wait();

        //    var twitterComment3 = new TwitterComment
        //    {
        //        Id = Guid.NewGuid(),
        //        CommentId = "tk3",
        //        Content = "@_NeverForgetBot Tweet3",
        //        CommentDate = DateTime.UtcNow,
        //        Author = "Author3",
        //        IsDeleted = false,
        //        Link = "Link",
        //        MediaLink = "Link2",
        //        ReplyToId = "fefe",
        //        TwitterContextId = twitterContext.Id,
        //        CreatedAt = DateTime.UtcNow,
        //    };
        //    twitterCommentDao.InsertAsync(twitterComment3).Wait();

        //    var twitterCommentList = twitterCommentDao.GetAllAsync().Result;

        //    Assert.IsTrue(twitterCommentList.Count == 3);
        //}

        //[TestMethod]
        //public void TestUpdateTwitter()
        //{
        //    using (var context = new NeverForgetBotDbContext())
        //    {
        //        var resultDrop = context.DropDatabase().Result;
        //        var resultCreate = context.CreateDatabase().Result;
        //    }

        //    var _requestTypeDao = new RequestTypeDao();
        //    RequestType defaultRequest = new RequestType { Id = (int)RequestTypeEnum.Default, Name = "Default" };
        //    _requestTypeDao.InsertAsync(defaultRequest).Wait();


        //    var twitterDAO = new TwitterContextDao();
        //    var twitterCommentDao = new TwitterCommentDao();

        //    var twitterContext = new TwitterContext
        //    {
        //        Id = Guid.NewGuid(),
        //        RequestTypeId = 0,
        //        RequestType = defaultRequest,
        //        CreatedAt = DateTime.UtcNow,
        //    };
        //    var twitterComment = new TwitterComment
        //    {
        //        Id = Guid.NewGuid(),
        //        CommentId = "tk1",
        //        Content = "@_NeverForgetBot Tweet1",
        //        CommentDate = DateTime.UtcNow,
        //        Author = "Author1",
        //        IsDeleted = false,
        //        Link = "Link",
        //        MediaLink = "Link2",
        //        ReplyToId = "fefe",
        //        TwitterContextId = twitterContext.Id,
        //        CreatedAt = DateTime.UtcNow,

        //    };
        //    twitterDAO.InsertAsync(twitterContext).Wait();
        //    twitterCommentDao.InsertAsync(twitterComment).Wait();


        //    var resGet = twitterCommentDao.GetAsync(twitterComment.Id).Result;
        //    resGet.Author = "NewAuthor";
        //    twitterCommentDao.UpdateAsync(resGet).Wait();

        //    Assert.IsTrue(resGet.Author == "NewAuthor");
        //}

        //[TestMethod]
        //public void TestDeleteTwitter()
        //{
        //    using (var context = new NeverForgetBotDbContext())
        //    {
        //        var resultDrop = context.DropDatabase().Result;
        //        var resultCreate = context.CreateDatabase().Result;
        //    }
        //    var _requestTypeDao = new RequestTypeDao();
        //    RequestType defaultRequest = new RequestType { Id = (int)RequestTypeEnum.Default, Name = "Default" };
        //    _requestTypeDao.InsertAsync(defaultRequest).Wait();

        //    var twitterDAO = new TwitterContextDao();
        //    var twitterCommentDao = new TwitterCommentDao();

        //    var twitterContext = new TwitterContext
        //    {
        //        Id = Guid.NewGuid(),
        //        RequestTypeId = 0,
        //        RequestType = defaultRequest,
        //        CreatedAt = DateTime.UtcNow,
        //    };
        //    var twitterComment = new TwitterComment
        //    {
        //        Id = Guid.NewGuid(),
        //        CommentId = "tk1",
        //        Content = "@_NeverForgetBot Tweet1",
        //        CommentDate = DateTime.UtcNow,
        //        Author = "Author1",
        //        IsDeleted = false,
        //        Link = "Link",
        //        MediaLink = "Link2",
        //        ReplyToId = "fefe",
        //        TwitterContextId = twitterContext.Id,
        //        CreatedAt = DateTime.UtcNow,

        //    };
        //    twitterDAO.InsertAsync(twitterContext).Wait();
        //    twitterCommentDao.InsertAsync(twitterComment).Wait();

        //    var twitterComment2 = new TwitterComment
        //    {
        //        Id = Guid.NewGuid(),
        //        CommentId = "tk2",
        //        Content = "@_NeverForgetBot Tweet2",
        //        CommentDate = DateTime.UtcNow,
        //        Author = "Author2",
        //        IsDeleted = false,
        //        Link = "Link",
        //        MediaLink = "Link2",
        //        ReplyToId = "fefe",
        //        TwitterContextId = twitterContext.Id,
        //        CreatedAt = DateTime.UtcNow,
        //    };
        //    twitterCommentDao.InsertAsync(twitterComment2).Wait();

        //    var twitterComment3 = new TwitterComment
        //    {
        //        Id = Guid.NewGuid(),
        //        CommentId = "tk3",
        //        Content = "@_NeverForgetBot Tweet3",
        //        CommentDate = DateTime.UtcNow,
        //        Author = "Author3",
        //        IsDeleted = false,
        //        Link = "Link",
        //        MediaLink = "Link2",
        //        ReplyToId = "fefe",
        //        TwitterContextId = twitterContext.Id,
        //        CreatedAt = DateTime.UtcNow,
        //    };
        //    twitterCommentDao.InsertAsync(twitterComment3).Wait();

        //    twitterCommentDao.DeleteAsync(twitterComment3).Wait();

        //    Assert.IsTrue(twitterComment3.IsDeleted == true);
        //}

        //[TestMethod]
        //public void TestGetNonDeletedTwitter()
        //{
        //    using (var context = new NeverForgetBotDbContext())
        //    {
        //        var resultDrop = context.DropDatabase().Result;
        //        var resultCreate = context.CreateDatabase().Result;
        //    }

        //    var _requestTypeDao = new RequestTypeDao();
        //    RequestType defaultRequest = new RequestType { Id = (int)RequestTypeEnum.Default, Name = "Default" };
        //    _requestTypeDao.InsertAsync(defaultRequest).Wait();

        //    var twitterDAO = new TwitterContextDao();
        //    var twitterCommentDao = new TwitterCommentDao();

        //    var twitterContext = new TwitterContext
        //    {
        //        Id = Guid.NewGuid(),
        //        RequestTypeId = 0,
        //        RequestType = defaultRequest,
        //        CreatedAt = DateTime.UtcNow,
        //    };
        //    var twitterComment = new TwitterComment
        //    {
        //        Id = Guid.NewGuid(),
        //        CommentId = "tk1",
        //        Content = "@_NeverForgetBot Tweet1",
        //        CommentDate = DateTime.UtcNow,
        //        Author = "Author1",
        //        IsDeleted = false,
        //        Link = "Link",
        //        MediaLink = "Link2",
        //        ReplyToId = "fefe",
        //        TwitterContextId = twitterContext.Id,
        //        CreatedAt = DateTime.UtcNow,

        //    };
        //    twitterDAO.InsertAsync(twitterContext).Wait();
        //    twitterCommentDao.InsertAsync(twitterComment).Wait();

        //    var twitterComment2 = new TwitterComment
        //    {
        //        Id = Guid.NewGuid(),
        //        CommentId = "tk2",
        //        Content = "@_NeverForgetBot Tweet2",
        //        CommentDate = DateTime.UtcNow,
        //        Author = "Author2",
        //        IsDeleted = false,
        //        Link = "Link",
        //        MediaLink = "Link2",
        //        ReplyToId = "fefe",
        //        TwitterContextId = twitterContext.Id,
        //        CreatedAt = DateTime.UtcNow,
        //    };
        //    twitterCommentDao.InsertAsync(twitterComment2).Wait();

        //    var twitterComment3 = new TwitterComment
        //    {
        //        Id = Guid.NewGuid(),
        //        CommentId = "tk3",
        //        Content = "@_NeverForgetBot Tweet3",
        //        CommentDate = DateTime.UtcNow,
        //        Author = "Author3",
        //        IsDeleted = false,
        //        Link = "Link",
        //        MediaLink = "Link2",
        //        ReplyToId = "fefe",
        //        TwitterContextId = twitterContext.Id,
        //        CreatedAt = DateTime.UtcNow,
        //    };
        //    twitterCommentDao.InsertAsync(twitterComment3).Wait();

        //    twitterCommentDao.DeleteAsync(twitterComment3).Wait();

        //    var resGetNonDeleted = twitterCommentDao.GetAsync(twitterComment3.Id).Result;
        //    var resGetNonDeleted2 = twitterCommentDao.GetAsync(twitterComment2.Id).Result;

        //    Assert.IsTrue(resGetNonDeleted == null && resGetNonDeleted2 != null);
        //}

        //[TestMethod]
        //public void TestGetAllNonDeletedTwitter()
        //{
        //    using (var context = new NeverForgetBotDbContext())
        //    {
        //        var resultDrop = context.DropDatabase().Result;
        //        var resultCreate = context.CreateDatabase().Result;
        //    }

        //    var _requestTypeDao = new RequestTypeDao();
        //    RequestType defaultRequest = new RequestType { Id = (int)RequestTypeEnum.Default, Name = "Default" };
        //    _requestTypeDao.InsertAsync(defaultRequest).Wait();

        //    var twitterDAO = new TwitterContextDao();
        //    var twitterCommentDao = new TwitterCommentDao();

        //    var twitterContext = new TwitterContext
        //    {
        //        Id = Guid.NewGuid(),
        //        RequestTypeId = 0,
        //        RequestType = defaultRequest,
        //        CreatedAt = DateTime.UtcNow,
        //    };
        //    var twitterComment = new TwitterComment
        //    {
        //        Id = Guid.NewGuid(),
        //        CommentId = "tk1",
        //        Content = "@_NeverForgetBot Tweet1",
        //        CommentDate = DateTime.UtcNow,
        //        Author = "Author1",
        //        IsDeleted = false,
        //        Link = "Link",
        //        MediaLink = "Link2",
        //        ReplyToId = "fefe",
        //        TwitterContextId = twitterContext.Id,
        //        CreatedAt = DateTime.UtcNow,

        //    };
        //    twitterDAO.InsertAsync(twitterContext).Wait();
        //    twitterCommentDao.InsertAsync(twitterComment).Wait();

        //    var twitterComment2 = new TwitterComment
        //    {
        //        Id = Guid.NewGuid(),
        //        CommentId = "tk2",
        //        Content = "@_NeverForgetBot Tweet2",
        //        CommentDate = DateTime.UtcNow,
        //        Author = "Author2",
        //        IsDeleted = false,
        //        Link = "Link",
        //        MediaLink = "Link2",
        //        ReplyToId = "fefe",
        //        TwitterContextId = twitterContext.Id,
        //        CreatedAt = DateTime.UtcNow,
        //    };
        //    twitterCommentDao.InsertAsync(twitterComment2).Wait();

        //    var twitterComment3 = new TwitterComment
        //    {
        //        Id = Guid.NewGuid(),
        //        CommentId = "tk3",
        //        Content = "@_NeverForgetBot Tweet3",
        //        CommentDate = DateTime.UtcNow,
        //        Author = "Author3",
        //        IsDeleted = false,
        //        Link = "Link",
        //        MediaLink = "Link2",
        //        ReplyToId = "fefe",
        //        TwitterContextId = twitterContext.Id,
        //        CreatedAt = DateTime.UtcNow,
        //    };
        //    twitterCommentDao.InsertAsync(twitterComment3).Wait();

        //    twitterCommentDao.DeleteAsync(twitterComment3).Wait();

        //    var twitterCommentList = twitterCommentDao.GetAllAsync().Result;

        //    //Assert.IsTrue(twitterContextList.Contains);
        //    Assert.IsTrue(twitterCommentList.Count == 2);
        //}

        //[TestMethod]
        //public void TestGetAllDeletedTwitter()
        //{
        //    using (var context = new NeverForgetBotDbContext())
        //    {
        //        var resultDrop = context.DropDatabase().Result;
        //        var resultCreate = context.CreateDatabase().Result;
        //    }

        //    var _requestTypeDao = new RequestTypeDao();
        //    RequestType defaultRequest = new RequestType { Id = (int)RequestTypeEnum.Default, Name = "Default" };
        //    _requestTypeDao.InsertAsync(defaultRequest).Wait();

        //    var twitterDAO = new TwitterContextDao();
        //    var twitterCommentDao = new TwitterCommentDao();

        //    var twitterContext = new TwitterContext
        //    {
        //        Id = Guid.NewGuid(),
        //        RequestTypeId = 0,
        //        RequestType = defaultRequest,
        //        CreatedAt = DateTime.UtcNow,
        //    };
        //    var twitterComment = new TwitterComment
        //    {
        //        Id = Guid.NewGuid(),
        //        CommentId = "tk1",
        //        Content = "@_NeverForgetBot Tweet1",
        //        CommentDate = DateTime.UtcNow,
        //        Author = "Author1",
        //        IsDeleted = false,
        //        Link = "Link",
        //        MediaLink = "Link2",
        //        ReplyToId = "fefe",
        //        TwitterContextId = twitterContext.Id,
        //        CreatedAt = DateTime.UtcNow,

        //    };
        //    twitterDAO.InsertAsync(twitterContext).Wait();
        //    twitterCommentDao.InsertAsync(twitterComment).Wait();

        //    var twitterComment2 = new TwitterComment
        //    {
        //        Id = Guid.NewGuid(),
        //        CommentId = "tk2",
        //        Content = "@_NeverForgetBot Tweet2",
        //        CommentDate = DateTime.UtcNow,
        //        Author = "Author2",
        //        IsDeleted = false,
        //        Link = "Link",
        //        MediaLink = "Link2",
        //        ReplyToId = "fefe",
        //        TwitterContextId = twitterContext.Id,
        //        CreatedAt = DateTime.UtcNow,
        //    };
        //    twitterCommentDao.InsertAsync(twitterComment2).Wait();

        //    var twitterComment3 = new TwitterComment
        //    {
        //        Id = Guid.NewGuid(),
        //        CommentId = "tk3",
        //        Content = "@_NeverForgetBot Tweet1",
        //        CommentDate = DateTime.UtcNow,
        //        Author = "Author3",
        //        IsDeleted = false,
        //        Link = "Link",
        //        MediaLink = "Link2",
        //        ReplyToId = "fefe",
        //        TwitterContextId = twitterContext.Id,
        //        CreatedAt = DateTime.UtcNow,
        //    };
        //    twitterCommentDao.InsertAsync(twitterComment3).Wait();

        //    twitterCommentDao.DeleteAsync(twitterComment3).Wait();

        //    var twitterCommentList = twitterCommentDao.GetAllAsync().Result;
        //    var twitterCommentListDeleted = twitterCommentDao.GetAllDeletedAsync().Result;

        //    Assert.IsTrue(twitterCommentList.Count == 2 && twitterCommentListDeleted.Count == 1);
        //}
        //#endregion
    }
}
