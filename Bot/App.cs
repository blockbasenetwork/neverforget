using BlockBase.Dapps.NeverForget.Business.Interfaces;
using BlockBase.Dapps.NeverForget.Common.Enums;
using BlockBase.Dapps.NeverForget.Data.Context;
using BlockBase.Dapps.NeverForget.Data.Entities;
using BlockBase.Dapps.NeverForget.DataAccess.Interfaces;
using BlockBase.Dapps.NeverForget.Services.API;
using System;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.Bot
{
    public class App
    {
        private RedditCollector _redditCollector;
        private TwitterCollector _twitterCollector;
        private IRedditContextBusinessObject _redditContextBusinessObject;
        private ITwitterContextBusinessObject _twitterContextBusinessObject;
        private IRequestTypeDataAccessObject _requestTypeDataAccessObject;


        public App(
            RedditCollector redditCollector,
            TwitterCollector twitterCollector,
            IRedditContextBusinessObject redditContextBusinessObject,
            ITwitterContextBusinessObject twitterContextBusinessObject,
            IRequestTypeDataAccessObject requestTypeDataAccessObject
    )
        {
            _redditCollector = redditCollector;
            _twitterCollector = twitterCollector;
            _redditContextBusinessObject = redditContextBusinessObject;
            _twitterContextBusinessObject = twitterContextBusinessObject;
            _requestTypeDataAccessObject = requestTypeDataAccessObject;
        }

        public async Task Run()
        {
            //#region Recreate Database
            //using (var context = new NeverForgetBotDbContext())
            //{
            //    var resultDrop = context.DropDatabase();
            //    var resultCreate = context.CreateDatabase();
            //}

            //#region Build RequestType Table
            //RequestType defaultRequest = new RequestType { Id = Guid.Parse("35d9d452-108f-41c1-9a23-35db29bf4d86"), Name = "Default", Type = RequestTypeEnum.Default };
            //RequestType commentRequest = new RequestType { Id = Guid.Parse("663b161e-0d38-487b-b253-061bfb390799"), Name = "Comment", Type = RequestTypeEnum.Comment };
            //RequestType postRequest = new RequestType { Id = Guid.Parse("2d1879da-5fe1-4f01-8b3f-da129e1b9c02"), Name = "Post", Type = RequestTypeEnum.Post };

            //await _requestTypeDataAccessObject.InsertAsync(defaultRequest);
            //await _requestTypeDataAccessObject.InsertAsync(commentRequest);
            //await _requestTypeDataAccessObject.InsertAsync(postRequest);
            //#endregion
            //#endregion


            //ApiHelper.InitializeClient();

            //RedditApi.AuthenticateClient();
            //await _redditContextBusinessObject.FromApiRedditAllComments();


            TwitterApi.AuthenticateClient();

            var mentions = await _twitterCollector.GetMentions();

            await _twitterContextBusinessObject.FromApiTwitterModel(mentions);
        }
    }
}
