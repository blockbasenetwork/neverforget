using BlockBase.Dapps.NeverForgetBot.Common.Enums;
using BlockBase.Dapps.NeverForgetBot.Dal.DAOs;
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

            var redditContext = new RedditContext { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, RequestTypeId = defaultRequest.Id, RequestType = defaultRequest };

            var redditComment = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id};


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

            var redditContext = new RedditContext { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, RequestTypeId = defaultRequest.Id, RequestType = defaultRequest };
            redditContextDao.InsertAsync(redditContext).Wait();

            var redditComment = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentDao.InsertAsync(redditComment).Wait();
            var redditComment2 = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk2", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id};
            redditCommentDao.InsertAsync(redditComment2).Wait();
            var redditComment3 = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk3", Author = "Ator", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id};
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

            var redditContext = new RedditContext { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, RequestTypeId = defaultRequest.Id, RequestType = defaultRequest };

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

            var redditContext = new RedditContext { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, RequestTypeId = defaultRequest.Id, RequestType = defaultRequest };
            redditContextDao.InsertAsync(redditContext).Wait();

            var redditComment = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentDao.InsertAsync(redditComment).Wait();
            var redditComment2 = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk2", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id };
            redditCommentDao.InsertAsync(redditComment2).Wait();
            var redditComment3 = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk3", Author = "Ator", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id};
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

            var redditContext = new RedditContext { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, RequestTypeId = defaultRequest.Id, RequestType = defaultRequest };
            redditContextDao.InsertAsync(redditContext).Wait();

            var redditComment = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk1", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id};
            redditCommentDao.InsertAsync(redditComment).Wait();
            var redditComment2 = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk2", Author = "Autor", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id};
            redditCommentDao.InsertAsync(redditComment2).Wait();
            var redditComment3 = new RedditComment { Id = Guid.NewGuid(), CommentId = "tk3", Author = "Ator", SubReddit = "Testing", Content = "NeverForgetThis", CommentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow, Link = "Zelda", ParentId = "t1_qualquercoisa", ParentSubmissionId = "t3_qualquercoisa", RedditContextId = redditContext.Id};
            redditCommentDao.InsertAsync(redditComment3).Wait();


            redditCommentDao.DeleteAsync(redditComment3).Wait();
            var redditContextList = redditCommentDao.GetAllAsync().Result;
            var redditContextListDeleted = redditCommentDao.GetAllDeletedAsync().Result;



            Assert.IsTrue(redditContextList.Count == 2 && redditContextListDeleted.Count == 1);
        }
        #endregion


        #region Twitter
        //[TestMethod]
        //public void TestInsertAndGetTwitter()
        //{
        //    using (var context = new NeverForgetBotDbContext())
        //    {
        //        var resultDrop = context.DropDatabase().Result;
        //        var resultCreate = context.CreateDatabase().Result;
        //    }

        //    var twitterDAO = new TwitterContextDao();

        //    var twitterContext = new TwitterContext
        //    {
        //        Id = Guid.NewGuid(),
        //        TweetId = "tk1",
        //        TweetText = "@_NeverForgetBot Tweet1",
        //        TweetDate = DateTime.UtcNow,
        //        AuthorId = "Au1",
        //        Author = "Author1",
        //        InReplyToTweetId = null,
        //        InReplyToUserId = null,
        //        InReplyToUser = null,
        //        CreatedAt = DateTime.UtcNow,
        //        UpdatedAt = DateTime.UtcNow
        //    };
        //    twitterDAO.InsertAsync(twitterContext).Wait();

        //    var resGet = twitterDAO.GetAsync(twitterContext.Id).Result;

        //    Assert.IsTrue(resGet != null);
        //}

        //[TestMethod]
        //public void TestGetAllTwitter()
        //{
        //    using (var context = new NeverForgetBotDbContext())
        //    {
        //        var resultDrop = context.DropDatabase().Result;
        //        var resultCreate = context.CreateDatabase().Result;
        //    }

        //    var twitterDAO = new TwitterContextDao();

        //    var twitterContext = new TwitterContext
        //    {
        //        Id = Guid.NewGuid(),
        //        TweetId = "tk1",
        //        TweetText = "@_NeverForgetBot Tweet1",
        //        TweetDate = DateTime.UtcNow,
        //        AuthorId = "Au1",
        //        Author = "Author1",
        //        InReplyToTweetId = null,
        //        InReplyToUserId = null,
        //        InReplyToUser = null,
        //        CreatedAt = DateTime.UtcNow,
        //        UpdatedAt = DateTime.UtcNow
        //    };
        //    twitterDAO.InsertAsync(twitterContext).Wait();

        //    var twitterContext2 = new TwitterContext
        //    {
        //        Id = Guid.NewGuid(),
        //        TweetId = "tk2",
        //        TweetText = "@_NeverForgetBot Tweet2 #comment",
        //        TweetDate = DateTime.UtcNow,
        //        AuthorId = "Au2",
        //        Author = "Author2",
        //        InReplyToTweetId = twitterContext.TweetId,
        //        InReplyToUserId = twitterContext.AuthorId,
        //        InReplyToUser = twitterContext.Author,
        //        CreatedAt = DateTime.UtcNow,
        //        UpdatedAt = DateTime.UtcNow
        //    };
        //    twitterDAO.InsertAsync(twitterContext2).Wait();

        //    var twitterContext3 = new TwitterContext
        //    {
        //        Id = Guid.NewGuid(),
        //        TweetId = "tk3",
        //        TweetText = "@_NeverForgetBot Tweet3 #tweet",
        //        TweetDate = DateTime.UtcNow,
        //        AuthorId = "Au3",
        //        Author = "Author3",
        //        InReplyToTweetId = twitterContext2.TweetId,
        //        InReplyToUserId = twitterContext2.AuthorId,
        //        InReplyToUser = twitterContext2.Author,
        //        CreatedAt = DateTime.UtcNow,
        //        UpdatedAt = DateTime.UtcNow
        //    };
        //    twitterDAO.InsertAsync(twitterContext3).Wait();

        //    var twitterContextList = twitterDAO.GetAllAsync().Result;

        //    Assert.IsTrue(twitterContextList.Count == 3);
        //}

        //[TestMethod]
        //public void TestUpdateTwitter()
        //{
        //    using (var context = new NeverForgetBotDbContext())
        //    {
        //        var resultDrop = context.DropDatabase().Result;
        //        var resultCreate = context.CreateDatabase().Result;
        //    }

        //    var twitterDAO = new TwitterContextDao();

        //    var twitterContext = new TwitterContext
        //    {
        //        Id = Guid.NewGuid(),
        //        TweetId = "tk1",
        //        TweetText = "@_NeverForgetBot Tweet1",
        //        TweetDate = DateTime.UtcNow,
        //        AuthorId = "Au1",
        //        Author = "Author1",
        //        InReplyToTweetId = null,
        //        InReplyToUserId = null,
        //        InReplyToUser = null,
        //        CreatedAt = DateTime.UtcNow,
        //        UpdatedAt = DateTime.UtcNow
        //    };
        //    twitterDAO.InsertAsync(twitterContext).Wait();


        //    var resGet = twitterDAO.GetAsync(twitterContext.Id).Result;
        //    resGet.Author = "NewAuthor";
        //    twitterDAO.UpdateAsync(resGet).Wait();

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

        //    var twitterDAO = new TwitterContextDao();

        //    var twitterContext = new TwitterContext
        //    {
        //        Id = Guid.NewGuid(),
        //        TweetId = "tk1",
        //        TweetText = "@_NeverForgetBot Tweet1",
        //        TweetDate = DateTime.UtcNow,
        //        AuthorId = "Au1",
        //        Author = "Author1",
        //        InReplyToTweetId = null,
        //        InReplyToUserId = null,
        //        InReplyToUser = null,
        //        CreatedAt = DateTime.UtcNow,
        //        UpdatedAt = DateTime.UtcNow
        //    };
        //    twitterDAO.InsertAsync(twitterContext).Wait();

        //    var twitterContext2 = new TwitterContext
        //    {
        //        Id = Guid.NewGuid(),
        //        TweetId = "tk2",
        //        TweetText = "@_NeverForgetBot Tweet2 #comment",
        //        TweetDate = DateTime.UtcNow,
        //        AuthorId = "Au2",
        //        Author = "Author2",
        //        InReplyToTweetId = twitterContext.TweetId,
        //        InReplyToUserId = twitterContext.AuthorId,
        //        InReplyToUser = twitterContext.Author,
        //        CreatedAt = DateTime.UtcNow,
        //        UpdatedAt = DateTime.UtcNow
        //    };
        //    twitterDAO.InsertAsync(twitterContext2).Wait();

        //    var twitterContext3 = new TwitterContext
        //    {
        //        Id = Guid.NewGuid(),
        //        TweetId = "tk3",
        //        TweetText = "@_NeverForgetBot Tweet3 #tweet #test",
        //        TweetDate = DateTime.UtcNow,
        //        AuthorId = "Au3",
        //        Author = "Author3",
        //        InReplyToTweetId = twitterContext2.TweetId,
        //        InReplyToUserId = twitterContext2.AuthorId,
        //        InReplyToUser = twitterContext2.Author,
        //        CreatedAt = DateTime.UtcNow,
        //        UpdatedAt = DateTime.UtcNow
        //    };
        //    twitterDAO.InsertAsync(twitterContext3).Wait();

        //    twitterDAO.DeleteAsync(twitterContext3).Wait();

        //    Assert.IsTrue(twitterContext3.IsDeleted == true);
        //}

        //[TestMethod]
        //public void TestGetNonDeletedTwitter()
        //{
        //    using (var context = new NeverForgetBotDbContext())
        //    {
        //        var resultDrop = context.DropDatabase().Result;
        //        var resultCreate = context.CreateDatabase().Result;
        //    }

        //    var twitterDAO = new TwitterContextDao();

        //    var twitterContext = new TwitterContext
        //    {
        //        Id = Guid.NewGuid(),
        //        TweetId = "tk1",
        //        TweetText = "@_NeverForgetBot Tweet1",
        //        TweetDate = DateTime.UtcNow,
        //        AuthorId = "Au1",
        //        Author = "Author1",
        //        InReplyToTweetId = null,
        //        InReplyToUserId = null,
        //        InReplyToUser = null,
        //        CreatedAt = DateTime.UtcNow,
        //        UpdatedAt = DateTime.UtcNow
        //    };
        //    twitterDAO.InsertAsync(twitterContext).Wait();

        //    var twitterContext2 = new TwitterContext
        //    {
        //        Id = Guid.NewGuid(),
        //        TweetId = "tk2",
        //        TweetText = "@_NeverForgetBot Tweet2 #comment",
        //        TweetDate = DateTime.UtcNow,
        //        AuthorId = "Au2",
        //        Author = "Author2",
        //        InReplyToTweetId = twitterContext.TweetId,
        //        InReplyToUserId = twitterContext.AuthorId,
        //        InReplyToUser = twitterContext.Author,
        //        CreatedAt = DateTime.UtcNow,
        //        UpdatedAt = DateTime.UtcNow
        //    };
        //    twitterDAO.InsertAsync(twitterContext2).Wait();

        //    var twitterContext3 = new TwitterContext
        //    {
        //        Id = Guid.NewGuid(),
        //        TweetId = "tk3",
        //        TweetText = "@_NeverForgetBot Tweet3 #tweet #test",
        //        TweetDate = DateTime.UtcNow,
        //        AuthorId = "Au3",
        //        Author = "Author3",
        //        InReplyToTweetId = twitterContext2.TweetId,
        //        InReplyToUserId = twitterContext2.AuthorId,
        //        InReplyToUser = twitterContext2.Author,
        //        CreatedAt = DateTime.UtcNow,
        //        UpdatedAt = DateTime.UtcNow
        //    };
        //    twitterDAO.InsertAsync(twitterContext3).Wait();

        //    twitterDAO.DeleteAsync(twitterContext3).Wait();

        //    var resGetNonDeleted = twitterDAO.GetNonDeletedAsync(twitterContext3.Id).Result;
        //    var resGetNonDeleted2 = twitterDAO.GetNonDeletedAsync(twitterContext2.Id).Result;

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

        //    var twitterDAO = new TwitterContextDao();

        //    var twitterContext = new TwitterContext
        //    {
        //        Id = Guid.NewGuid(),
        //        TweetId = "tk1",
        //        TweetText = "@_NeverForgetBot Tweet1",
        //        TweetDate = DateTime.UtcNow,
        //        AuthorId = "Au1",
        //        Author = "Author1",
        //        InReplyToTweetId = null,
        //        InReplyToUserId = null,
        //        InReplyToUser = null,
        //        CreatedAt = DateTime.UtcNow,
        //        UpdatedAt = DateTime.UtcNow
        //    };
        //    twitterDAO.InsertAsync(twitterContext).Wait();

        //    var twitterContext2 = new TwitterContext
        //    {
        //        Id = Guid.NewGuid(),
        //        TweetId = "tk2",
        //        TweetText = "@_NeverForgetBot Tweet2 #comment",
        //        TweetDate = DateTime.UtcNow,
        //        AuthorId = "Au2",
        //        Author = "Author2",
        //        InReplyToTweetId = twitterContext.TweetId,
        //        InReplyToUserId = twitterContext.AuthorId,
        //        InReplyToUser = twitterContext.Author,
        //        CreatedAt = DateTime.UtcNow,
        //        UpdatedAt = DateTime.UtcNow
        //    };
        //    twitterDAO.InsertAsync(twitterContext2).Wait();

        //    var twitterContext3 = new TwitterContext
        //    {
        //        Id = Guid.NewGuid(),
        //        TweetId = "tk3",
        //        TweetText = "@_NeverForgetBot Tweet3 #tweet #test",
        //        TweetDate = DateTime.UtcNow,
        //        AuthorId = "Au3",
        //        Author = "Author3",
        //        InReplyToTweetId = twitterContext2.TweetId,
        //        InReplyToUserId = twitterContext2.AuthorId,
        //        InReplyToUser = twitterContext2.Author,
        //        CreatedAt = DateTime.UtcNow,
        //        UpdatedAt = DateTime.UtcNow
        //    };
        //    twitterDAO.InsertAsync(twitterContext3).Wait();

        //    twitterDAO.DeleteAsync(twitterContext3).Wait();

        //    var twitterContextList = twitterDAO.GetAllAsync().Result;

        //    var twitterContextListNonDeleted = twitterDAO.GetAllNonDeletedAsync().Result;

        //    //Assert.IsTrue(twitterContextList.Contains);
        //    Assert.IsTrue(twitterContextList.Count == 3 && twitterContextListNonDeleted.Count == 2);
        //}

        //[TestMethod]
        //public void TestGetAllDeletedTwitter()
        //{
        //    using (var context = new NeverForgetBotDbContext())
        //    {
        //        var resultDrop = context.DropDatabase().Result;
        //        var resultCreate = context.CreateDatabase().Result;
        //    }

        //    var twitterDAO = new TwitterContextDao();

        //    var twitterContext = new TwitterContext
        //    {
        //        Id = Guid.NewGuid(),
        //        TweetId = "tk1",
        //        TweetText = "@_NeverForgetBot Tweet1",
        //        TweetDate = DateTime.UtcNow,
        //        AuthorId = "Au1",
        //        Author = "Author1",
        //        InReplyToTweetId = null,
        //        InReplyToUserId = null,
        //        InReplyToUser = null,
        //        CreatedAt = DateTime.UtcNow,
        //        UpdatedAt = DateTime.UtcNow
        //    };
        //    twitterDAO.InsertAsync(twitterContext).Wait();

        //    var twitterContext2 = new TwitterContext
        //    {
        //        Id = Guid.NewGuid(),
        //        TweetId = "tk2",
        //        TweetText = "@_NeverForgetBot Tweet2 #comment",
        //        TweetDate = DateTime.UtcNow,
        //        AuthorId = "Au2",
        //        Author = "Author2",
        //        InReplyToTweetId = twitterContext.TweetId,
        //        InReplyToUserId = twitterContext.AuthorId,
        //        InReplyToUser = twitterContext.Author,
        //        CreatedAt = DateTime.UtcNow,
        //        UpdatedAt = DateTime.UtcNow
        //    };
        //    twitterDAO.InsertAsync(twitterContext2).Wait();

        //    var twitterContext3 = new TwitterContext
        //    {
        //        Id = Guid.NewGuid(),
        //        TweetId = "tk3",
        //        TweetText = "@_NeverForgetBot Tweet3 #tweet #test",
        //        TweetDate = DateTime.UtcNow,
        //        AuthorId = "Au3",
        //        Author = "Author3",
        //        InReplyToTweetId = twitterContext2.TweetId,
        //        InReplyToUserId = twitterContext2.AuthorId,
        //        InReplyToUser = twitterContext2.Author,
        //        CreatedAt = DateTime.UtcNow,
        //        UpdatedAt = DateTime.UtcNow
        //    };
        //    twitterDAO.InsertAsync(twitterContext3).Wait();

        //    twitterDAO.DeleteAsync(twitterContext3).Wait();

        //    var twitterContextList = twitterDAO.GetAllAsync().Result;
        //    var twitterContextListDeleted = twitterDAO.GetAllDeletedAsync().Result;

        //    Assert.IsTrue(twitterContextList.Count == 3 && twitterContextListDeleted.Count == 1);
        //}
        #endregion
    }
}
