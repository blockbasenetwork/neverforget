﻿using BlockBase.Dapps.NeverForgetBot.Business.BusinessLayer.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Common.Enums;
using BlockBase.Dapps.NeverForgetBot.Dal;
using BlockBase.Dapps.NeverForgetBot.Data.Context;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Services.API;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.ConsoleApp
{
    public class App
    {
        private IRedditContextBo _redditContextBo;
        private IRedditCommentBo _redditCommentBo;
        private IRedditSubmissionBo _redditSubmissionBo;
        private ITwitterContextBo _twitterContextBo;
        private ITwitterCommentBo _twitterCommentBo;
        private ITwitterSubmissionBo _twitterSubmissionBo;
        private IRequestTypeDao _requestTypeDao;
        private RedditCollector _redditCollector;
        private TwitterCollector _twitterCollector;

        public App(
            IRedditContextBo redditContextBo,
            IRedditCommentBo redditCommentBo,
            IRedditSubmissionBo redditSubmissionBo,
            ITwitterContextBo twitterContextBo,
            ITwitterCommentBo twitterCommentBo,
            ITwitterSubmissionBo twitterSubmissionBo,
            IRequestTypeDao requestTypeDao,
            RedditCollector redditCollector,
            TwitterCollector twitterCollector
            )
        {
            _redditContextBo = redditContextBo;
            _redditCommentBo = redditCommentBo;
            _redditSubmissionBo = redditSubmissionBo;
            _twitterContextBo = twitterContextBo;
            _twitterCommentBo = twitterCommentBo;
            _twitterSubmissionBo = twitterSubmissionBo;
            _requestTypeDao = requestTypeDao;
            _redditCollector = redditCollector;
            _twitterCollector = twitterCollector;
        }

        public async Task Run()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase();
                var resultCreate = context.CreateDatabase();
            }

            #region Build RequestType Table
            RequestType defaultRequest = new RequestType { Id = (int)RequestTypeEnum.Default, Name = "Default" };
            RequestType commentRequest = new RequestType { Id = (int)RequestTypeEnum.Comment, Name = "Comment" };
            RequestType postRequest = new RequestType { Id = (int)RequestTypeEnum.Post, Name = "Post" };

            await _requestTypeDao.InsertAsync(defaultRequest);
            await _requestTypeDao.InsertAsync(commentRequest);
            await _requestTypeDao.InsertAsync(postRequest);
            #endregion



            ApiHelper.InitializeClient();

            //RedditApi.AuthorizeUser();
            RedditApi.AuthenticateClient();
            await _redditContextBo.FromApiRedditAllComments();


            TwitterApi.AuthenticateClient();

            var mentions = await _twitterCollector.GetMentions();

            await _twitterContextBo.FromApiTwitterModel(mentions);

        }
    }
}
