using BlockBase.Dapps.NeverForget.Business.Interfaces;
using BlockBase.Dapps.NeverForget.Common.Enums;
using BlockBase.Dapps.NeverForget.Data.Context;
using BlockBase.Dapps.NeverForget.Data.Entities;
using BlockBase.Dapps.NeverForget.DataAccess.Interfaces;
using BlockBase.Dapps.NeverForget.Services.API;
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
            ApiHelper.InitializeClient();

            RedditApi.AuthenticateClient();
            await _redditContextBusinessObject.FromApiRedditAllComments();


            TwitterApi.AuthenticateClient();

            var mentions = await _twitterCollector.GetMentions();

            await _twitterContextBusinessObject.FromApiTwitterModel(mentions);
        }
    }
}
