using BlockBase.Dapps.NeverForgetBot.Business.BOs;
using BlockBase.Dapps.NeverForgetBot.Business.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Dal.DAOs;
using BlockBase.Dapps.NeverForgetBot.Dal.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Services.API;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.ConsoleApp
{
    public class App
    {
        public async Task Run()
        {
            //using (var context = new NeverForgetBotDbContext())
            //{
            //    var result = context.CreateDatabase().Result;
            //}


            ApiHelper.InitializeClient();

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IRedditContextBo, RedditContextBo>();
                    services.AddTransient<IRedditContextBo, RedditContextBo>();

                    services.AddTransient<IRedditContextDao, RedditContextDao>();
                    services.AddTransient<ITwitterContextDao, TwitterContextDao>();
                })
                .UseSerilog()
                .Build();


            //obter a instância do IoC e pedir a esta, a instância do RedditContextBO
            //var context = services.Resolve<IRedditContextBO>();
            //
        }
    }
}
