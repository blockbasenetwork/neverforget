using BlockBase.Dapps.NeverForget.Common.Enums;
using BlockBase.Dapps.NeverForget.Data.Context;
using BlockBase.Dapps.NeverForget.Data.Entities;
using BlockBase.Dapps.NeverForget.DataAccess.DataAccessObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

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


            var redditContextList = redditCommentDao.List().Result;

            Assert.IsTrue(redditContextList.Count() == 3);
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

            var twitterDao = new TwitterContextDataAccessObject();
            var twitterCommentDao = new TwitterCommentDataAccessObject();
            var twitterSubmissionDao = new TwitterSubmissionDataAccessObject();

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
            
            twitterDao.InsertAsync(twitterContext).Wait();
            twitterCommentDao.InsertAsync(twitterComment).Wait();
            twitterSubmissionDao.InsertAsync(twitterSubmission).Wait();

            var resGetCon = twitterDao.GetAsync(twitterContext.Id).Result;
            var resGetCom = twitterCommentDao.GetAsync(twitterComment.Id).Result;
            var resGetSub = twitterSubmissionDao.GetAsync(twitterSubmission.Id).Result;

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

            var twitterDao = new TwitterContextDataAccessObject();
            var twitterCommentDao = new TwitterCommentDataAccessObject();
            var twitterSubmissionDao = new TwitterSubmissionDataAccessObject();

            var twitterContext = new TwitterContext
            {
                Id = Guid.NewGuid(),
                RequestTypeId = defaultRequest.Id,
                CreatedAt = DateTime.UtcNow,
            };

            var twitterContext2 = new TwitterContext
            {
                Id = Guid.NewGuid(),
                RequestTypeId = defaultRequest.Id,
                CreatedAt = DateTime.UtcNow,
            };

            var twitterContext3 = new TwitterContext
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

            var twitterComment2 = new TwitterComment
            {
                Id = Guid.NewGuid(),
                CommentId = "tk2",
                Content = "@_NeverForgetBot Tweet2",
                CommentDate = DateTime.UtcNow,
                Author = "Author2",
                Link = "Link",
                MediaLink = "Link2",
                ReplyToId = "s2",
                TwitterContextId = twitterContext2.Id,
                CreatedAt = DateTime.UtcNow,
            };

            var twitterComment3 = new TwitterComment
            {
                Id = Guid.NewGuid(),
                CommentId = "tk3",
                Content = "@_NeverForgetBot Tweet3",
                CommentDate = DateTime.UtcNow,
                Author = "Author3",
                Link = "Link",
                MediaLink = "Link2",
                ReplyToId = "s3",
                TwitterContextId = twitterContext3.Id,
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

            var twitterSubmission2 = new TwitterSubmission
            {
                Id = Guid.NewGuid(),
                SubmissionId = "s2",
                Content = "Main Tweet",
                SubmissionDate = DateTime.UtcNow,
                Author = "Tweeter2",
                Link = "Link",
                MediaLink = "Link2",
                TwitterContextId = twitterContext2.Id,
                CreatedAt = DateTime.UtcNow,
            };

            var twitterSubmission3 = new TwitterSubmission
            {
                Id = Guid.NewGuid(),
                SubmissionId = "s3",
                Content = "Main Tweet",
                SubmissionDate = DateTime.UtcNow,
                Author = "Tweeter3",
                Link = "Link",
                MediaLink = "Link2",
                TwitterContextId = twitterContext3.Id,
                CreatedAt = DateTime.UtcNow,
            };

            twitterDao.InsertAsync(twitterContext).Wait();
            twitterDao.InsertAsync(twitterContext2).Wait();
            twitterDao.InsertAsync(twitterContext3).Wait();
            twitterCommentDao.InsertAsync(twitterComment).Wait();
            twitterCommentDao.InsertAsync(twitterComment2).Wait();
            twitterCommentDao.InsertAsync(twitterComment3).Wait();
            twitterSubmissionDao.InsertAsync(twitterSubmission).Wait();
            twitterSubmissionDao.InsertAsync(twitterSubmission2).Wait();
            twitterSubmissionDao.InsertAsync(twitterSubmission3).Wait();

            var twitterContextList = twitterDao.List().Result;
            var twitterCommentList = twitterCommentDao.List().Result;
            var twitterSubmissionList = twitterSubmissionDao.List().Result;

            Assert.IsTrue(twitterContextList.Count() == 3 && twitterCommentList.Count() == 3 && twitterSubmissionList.Count() == 3);
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

            var twitterDao = new TwitterContextDataAccessObject();
            var twitterCommentDao = new TwitterCommentDataAccessObject();
            var twitterSubmissionDao = new TwitterSubmissionDataAccessObject();

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

            twitterDao.InsertAsync(twitterContext).Wait();
            twitterCommentDao.InsertAsync(twitterComment).Wait();
            twitterSubmissionDao.InsertAsync(twitterSubmission).Wait();

            twitterContext.RequestTypeId = commentRequest.Id;
            twitterComment.Link = "NewLink";
            twitterSubmission.Link = "NewLink";

            twitterDao.UpdateAsync(twitterContext).Wait();
            twitterCommentDao.UpdateAsync(twitterComment).Wait();
            twitterSubmissionDao.UpdateAsync(twitterSubmission).Wait();

            var resGetCon = twitterDao.GetAsync(twitterContext.Id).Result;
            var resGetCom = twitterCommentDao.GetAsync(twitterComment.Id).Result;
            var resGetSub = twitterSubmissionDao.GetAsync(twitterSubmission.Id).Result;

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

            var twitterDao = new TwitterContextDataAccessObject();
            var twitterCommentDao = new TwitterCommentDataAccessObject();
            var twitterSubmissionDao = new TwitterSubmissionDataAccessObject();

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
                ReplyToId = "fefe",
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

            twitterDao.InsertAsync(twitterContext).Wait();
            twitterCommentDao.InsertAsync(twitterComment).Wait();
            twitterSubmissionDao.InsertAsync(twitterSubmission).Wait();

            twitterCommentDao.DeleteAsync(twitterComment).Wait();
            twitterSubmissionDao.DeleteAsync(twitterSubmission).Wait();
            twitterDao.DeleteAsync(twitterContext).Wait();

            var twitterContextList = twitterDao.List().Result;
            var twitterCommentList = twitterCommentDao.List().Result;
            var twitterSubmissionList = twitterSubmissionDao.List().Result;

            Assert.IsTrue(twitterContextList.Count() == 0 && twitterCommentList.Count() == 0 && twitterSubmissionList.Count() == 0);
        }

        #endregion
    }
}
