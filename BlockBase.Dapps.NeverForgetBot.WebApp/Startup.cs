using BlockBase.Dapps.NeverForgetBot.Business.BusinessLayer.BOs;
using BlockBase.Dapps.NeverForgetBot.Business.BusinessLayer.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Dal;
using BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess;
using BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Dal.Queries;
using BlockBase.Dapps.NeverForgetBot.Services.API;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace BlockBase.Dapps.NeverForgetBot.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddSingleton<IDbOperationExecutor, DbOperationExecutor>();

            services.AddSingleton<IRequestTypeDao, RequestTypeDao>();
            services.AddSingleton<IRedditContextDao, RedditContextDao>();
            services.AddSingleton<IRedditCommentDao, RedditCommentDao>();
            services.AddSingleton<IRedditSubmissionDao, RedditSubmissionDao>();
            services.AddSingleton<ITwitterContextDao, TwitterContextDao>();
            services.AddSingleton<ITwitterCommentDao, TwitterCommentDao>();
            services.AddSingleton<ITwitterSubmissionDao, TwitterSubmissionDao>();
            services.AddSingleton<IRedditContextPocoDao, RedditContextPocoDao>();
            services.AddSingleton<ITwitterContextPocoDao, TwitterContextPocoDao>();

            services.AddSingleton<IRedditContextBo, RedditContextBo>();
            services.AddSingleton<IRedditCommentBo, RedditCommentBo>();
            services.AddSingleton<IRedditSubmissionBo, RedditSubmissionBo>();
            services.AddSingleton<ITwitterContextBo, TwitterContextBo>();
            services.AddSingleton<ITwitterCommentBo, TwitterCommentBo>();
            services.AddSingleton<ITwitterSubmissionBo, TwitterSubmissionBo>();
            services.AddSingleton<IGeneralContextBo, GeneralContextBo>();

            services.AddSingleton<RedditCollector>();
            services.AddSingleton<TwitterCollector>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            //app.UseExceptionHandler("/Error/500");
            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();


            //app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
