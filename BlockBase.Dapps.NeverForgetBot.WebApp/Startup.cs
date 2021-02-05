using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlockBase.Dapps.NeverForgetBot.Business.BOs;
using BlockBase.Dapps.NeverForgetBot.Business.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Dal.DAOs;
using BlockBase.Dapps.NeverForgetBot.Dal.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Services.API;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            services.AddTransient<IDbOperationExecutor, DbOperationExecutor>();

            services.AddSingleton<IRequestTypeDao, RequestTypeDao>();
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
            //services.AddSingleTon<TwitterCollector>();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
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
