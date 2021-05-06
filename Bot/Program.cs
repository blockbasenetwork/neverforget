using BlockBase.Dapps.NeverForget.Business.BusinessModels;
using BlockBase.Dapps.NeverForget.Business.BusinessObjects;
using BlockBase.Dapps.NeverForget.Business.Interfaces;
using BlockBase.Dapps.NeverForget.DataAccess.DataAccessModels;
using BlockBase.Dapps.NeverForget.DataAccess.DataAccessObjects;
using BlockBase.Dapps.NeverForget.DataAccess.Interfaces;
using BlockBase.Dapps.NeverForget.Services.API;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .WriteTo.Console()
                .WriteTo.File("Logs/log.txt")
                .CreateLogger();
            Log.Logger.Information("App Start");

            #region IoC Container 
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<App>();

                    services.AddSingleton<IRequestTypeDataAccessObject, RequestTypeDataAccessObject>();

                    services.AddSingleton<IGenericDataAccessObject, GenericDataAccessObject>();

                    services.AddSingleton<IRedditContextDataAccessObject, RedditContextDataAccessObject>();
                    services.AddSingleton<IRedditCommentDataAccessObject, RedditCommentDataAccessObject>();
                    services.AddSingleton<IRedditSubmissionDataAccessObject, RedditSubmissionDataAccessObject>();
                    services.AddSingleton<ITwitterContextDataAccessObject, TwitterContextDataAccessObject>();
                    services.AddSingleton<ITwitterCommentDataAccessObject, TwitterCommentDataAccessObject>();
                    services.AddSingleton<ITwitterSubmissionDataAccessObject, TwitterSubmissionDataAccessObject>();
                    services.AddSingleton<IRedditContextPocoDataAccessObject, RedditContextPocoDataAccessObject>();
                    services.AddSingleton<ITwitterContextPocoDataAccessObject, TwitterContextPocoDataAccessObject>();

                    services.AddSingleton<IGenericBusinessObject, GenericBusinessObject>();

                    services.AddSingleton<IRedditContextBusinessObject, RedditContextBusinessObject>();
                    services.AddSingleton<IRedditCommentBusinessObject, RedditCommentBusinessObject>();
                    services.AddSingleton<IRedditSubmissionBusinessObject, RedditSubmissionBusinessObject>();
                    services.AddSingleton<ITwitterContextBusinessObject, TwitterContextBusinessObject>();
                    services.AddSingleton<ITwitterCommentBusinessObject, TwitterCommentBusinessObject>();
                    services.AddSingleton<ITwitterSubmissionBusinessObject, TwitterSubmissionBusinessObject>();
                    services.AddSingleton<IGeneralContextBusinessObject, GeneralContextBusinessObject>();


                    services.AddSingleton<RedditCollector>();
                    services.AddSingleton<TwitterCollector>();
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
