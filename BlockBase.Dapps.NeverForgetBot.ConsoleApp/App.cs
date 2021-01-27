using BlockBase.Dapps.NeverForgetBot.Data.Context;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.ConsoleApp
{
    public class App
    {
        public async Task Run()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var result = context.CreateDatabase().Result;
            }
        }
    }
}
