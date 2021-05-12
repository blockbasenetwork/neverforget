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
            #region Recreate Database
            //using (var context = new NeverForgetBotDbContext())
            //{
            //    var resultDrop = context.DropDatabase();
            //    var resultCreate = context.CreateDatabase();
            //}

            //#region Build RequestType Table
            //RequestType defaultRequest = new RequestType { Id = (int)RequestTypeEnum.Default, Name = "Default" };
            //RequestType commentRequest = new RequestType { Id = (int)RequestTypeEnum.Comment, Name = "Comment" };
            //RequestType postRequest = new RequestType { Id = (int)RequestTypeEnum.Post, Name = "Post" };

            //await _requestTypeDataAccessObject.InsertAsync(defaultRequest);
            //await _requestTypeDataAccessObject.InsertAsync(commentRequest);
            //await _requestTypeDataAccessObject.InsertAsync(postRequest);
            //#endregion
            #endregion

            //_redditCollector.CreateLastCommentDate(1611131718);


            //ApiHelper.InitializeClient();

            //RedditApi.AuthenticateClient();
            //await _redditContextBusinessObject.FromApiRedditAllComments();


            //TwitterApi.AuthenticateClient();

            //var mentions = await _twitterCollector.GetMentions();

            //await _twitterContextBusinessObject.FromApiTwitterModel(mentions);
        }
    }
}
