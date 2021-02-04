using BlockBase.Dapps.NeverForgetBot.Business.BOs;
using BlockBase.Dapps.NeverForgetBot.Business.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Dal.DAOs;
using BlockBase.Dapps.NeverForgetBot.Dal.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Services.API;
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
                .CreateLogger();
            Log.Logger.Information("App Start");

            #region IoC Container 
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<App>();

                    services.AddSingleton<IDbOperationExecutor, DbOperationExecutor>();

                    services.AddSingleton<IRedditContextDao, RedditContextDao>();
                    services.AddSingleton<IRedditCommentDao, RedditCommentDao>();
                    services.AddSingleton<IRedditSubmissionDao, RedditSubmissionDao>();
                    services.AddSingleton<ITwitterContextDao, TwitterContextDao>();
                    services.AddSingleton<ITwitterCommentDao, TwitterCommentDao>();
                    services.AddSingleton<ITwitterSubmissionDao, TwitterSubmissionDao>();

                    services.AddSingleton<IRedditContextBo, RedditContextBo>();
                    services.AddSingleton<IRedditCommentBo, RedditCommentBo>();
                    services.AddSingleton<IRedditSubmissionBo, RedditSubmissionBo>();
                    services.AddSingleton<ITwitterContextBo, TwitterContextBo>();
                    services.AddSingleton<ITwitterCommentBo, TwitterCommentBo>();
                    services.AddSingleton<ITwitterSubmissionBo, TwitterSubmissionBo>();

                    services.AddSingleton<RedditCollector>();


                })
                .UseSerilog()
                .Build();
            #endregion

            var app = host.Services.GetService<App>();
            Task.WaitAll(app.Run());

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
