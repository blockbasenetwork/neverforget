using BlockBase.Dapps.NeverForgetBot.Services.API;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.ConsoleApp
{
    public class App
    {
        public App()
        {
        }

        public async Task Run()
        {
            ApiHelper.InitializeClient();
        }
    }
}
