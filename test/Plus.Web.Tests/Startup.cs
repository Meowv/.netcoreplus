using Castle.Facilities.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Plus.Core.Tests;
using Plus.Dependency;
using Plus.EFCore.Test;
using Plus.Log4Net;
using Plus.Modules;
using Plus.MongoDb.Test;
using Plus.Services.Dto.Test;
using Plus.Services.Test;
using System.Reflection;

namespace Plus.Web.Tests
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            PlusStarter.Create<BlogWebModule>(options =>
            {
                options.IocManager
                       .IocContainer
                       .AddFacility<LoggingFacility>(x => x.UseLog4Net()
                       .WithConfig("log4net.config"));
            }).Initialize();
        }
    }

    [DependsOn(
        typeof(BlogCoreModule),
        typeof(BlogServicesModule),
        typeof(BlogServicesDtoModule),
        typeof(BlogEntityFrameworkCoreModule),
        typeof(BlogMongoDbModule)
    )]
    internal class BlogWebModule : PlusModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssembly(Assembly.GetExecutingAssembly());
        }
    }
}