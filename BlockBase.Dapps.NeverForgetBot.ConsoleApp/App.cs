using BlockBase.Dapps.NeverForgetBot.Business.Interfaces;
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
        private RedditCollector _redditCollector;

        public App(
            IRedditContextBo redditContextBo,
            IRedditCommentBo redditCommentBo,
            IRedditSubmissionBo redditSubmissionBo,
            ITwitterContextBo twitterContextBo,
            ITwitterCommentBo twitterCommentBo,
            ITwitterSubmissionBo twitterSubmissionBo,
            RedditCollector redditCollector
            )
        {
            _redditContextBo = redditContextBo;
            _redditCommentBo = redditCommentBo;
            _redditSubmissionBo = redditSubmissionBo;
            _twitterContextBo = twitterContextBo;
            _twitterCommentBo = twitterCommentBo;
            _twitterSubmissionBo = twitterSubmissionBo;
            _redditCollector = redditCollector;
        }

        public async Task Run()
        {
            //using (var context = new NeverForgetBotDbContext())
            //{
            //    var resultDrop = context.DropDatabase().Result;
            //    var resultCreate = context.CreateDatabase().Result;
            //}

            ApiHelper.InitializeClient();

            var content = _redditCollector.RedditContextInfo().Result;
            var comment = _redditCollector.RedditInfo().Result;

            await _redditContextBo.FromApiRedditModel(content, comment);
        }
    }
}
