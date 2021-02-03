using BlockBase.Dapps.NeverForgetBot.Business.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Data.Context;
using BlockBase.Dapps.NeverForgetBot.Services.API;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.ConsoleApp
{
    public class App
    {
        private IRedditContextBo _redditContextBo;
        private ITwitterContextBo _twitterContextBo;

        public App(IRedditContextBo redditContextBo, ITwitterContextBo twitterContextBo)
        {
            _redditContextBo = redditContextBo;
            _twitterContextBo = twitterContextBo;
        }

        public async Task Run()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var resultDrop = context.DropDatabase().Result;
                var resultCreate = context.CreateDatabase().Result;
            }

            ApiHelper.InitializeClient();

            var content = await RedditCollector.RedditInfo();
            await _redditContextBo.ProcessRedditInfoAsync(content);
        }
    }
}
