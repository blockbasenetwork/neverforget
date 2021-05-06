using BlockBase.Dapps.NeverForget.Business.BusinessModels;
using BlockBase.Dapps.NeverForget.Business.BusinessObjects;
using BlockBase.Dapps.NeverForget.Business.Interfaces;
using BlockBase.Dapps.NeverForget.DataAccess.DataAccessModels;
using BlockBase.Dapps.NeverForget.DataAccess.DataAccessObjects;
using BlockBase.Dapps.NeverForget.DataAccess.Interfaces;
using BlockBase.Dapps.NeverForget.Services.API;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace BlockBase.Dapps.NeverForget.WebApp
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddSingleton<IGenericDataAccessObject, GenericDataAccessObject>();
            services.AddSingleton<IRequestTypeDataAccessObject, RequestTypeDataAccessObject>();
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
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseSerilogRequestLogging();

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
