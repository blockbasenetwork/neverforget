using BlockBase.Dapps.NeverForgetBot.Business.BOs;
using BlockBase.Dapps.NeverForgetBot.Business.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Dal.DAOs;
using BlockBase.Dapps.NeverForgetBot.Dal.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {       
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);
             
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
            Log.Logger.Information("App Start");

            #region IoC Container 
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<App>();

                    services.AddSingleton<IRedditContextBo, RedditContextBo>();
                    services.AddSingleton<IRedditContextBo, RedditContextBo>();

                    services.AddSingleton<IRedditContextDao, RedditContextDao>();
                    services.AddSingleton<ITwitterContextDao, TwitterContextDao>();
                })
                .UseSerilog()
                .Build();
            #endregion

            //var app = new App();
            //Task.WaitAll(app.Run());
        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
        }
    }
}
