using BlockBase.Dapps.NeverForgetBot.ConsoleApp.API;
using BlockBase.Dapps.NeverForgetBot.Data.Context;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.ConsoleApp
{
    public class App
    {
        public async Task Run()
        {

            ApiHelper.InitializeClient();
            await RedditScrapper.RedditInfo();
        }
    }
}
