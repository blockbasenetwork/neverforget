using BlockBase.Dapps.NeverForgetBot.Business.Obsolete.BOs;
using BlockBase.Dapps.NeverForgetBot.Business.Obsolete.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Dal;
using BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess;
using BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess.Interfaces;
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
                    services.AddScoped<App>();

                    services.AddScoped<IDbOperationExecutor, DbOperationExecutor>();

                    services.AddScoped<IRequestTypeDao, RequestTypeDao>();

                    services.AddScoped<IRedditContextDao, RedditContextDao>();
                    services.AddScoped<IRedditCommentDao, RedditCommentDao>();
                    services.AddScoped<IRedditSubmissionDao, RedditSubmissionDao>();
                    services.AddScoped<ITwitterContextDao, TwitterContextDao>();
                    services.AddScoped<ITwitterCommentDao, TwitterCommentDao>();
                    services.AddScoped<ITwitterSubmissionDao, TwitterSubmissionDao>();

                    services.AddScoped<IRedditContextBo, RedditContextBo>();
                    services.AddScoped<IRedditCommentBo, RedditCommentBo>();
                    services.AddScoped<IRedditSubmissionBo, RedditSubmissionBo>();
                    services.AddScoped<ITwitterContextBo, TwitterContextBo>();
                    services.AddScoped<ITwitterCommentBo, TwitterCommentBo>();
                    services.AddScoped<ITwitterSubmissionBo, TwitterSubmissionBo>();

                    services.AddScoped<RedditCollector>();
                    services.AddScoped<TwitterCollector>();


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
