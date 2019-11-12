using AutoMapper;
using ChuanGoing.Application;
using ChuanGoing.Base.Interface.Event;
using ChuanGoing.Base.Ioc;
using ChuanGoing.Storage.Dapper;
using ChuanGoing.Web.API.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace ChuanGoing.Web.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// 环境
        /// </summary>
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            ///*使用NLog*/
            //loggerFactory.AddNLog();
            //env.ConfigureNLog("NLog.config");

            if (Environment.IsDevelopment())
            {
                services.AddMvc(mvcOptions =>
                {
                    mvcOptions.Filters.Add<ExceptionFilter>();
                }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
                services.AddOpenApiDocument(conf => conf.Title = "WebApi 接口文档");
            }
            else
            {
                services.AddMvc(mvcOptions =>
                {
                    mvcOptions.Filters.Add<ExceptionFilter>();
                }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            }
            //注册AutoMapper
            services.AddAutoMapper(Assembly.GetAssembly(typeof(ApplicationModule)));
            //依赖注入
            return services.UseAutofac<WebModule>(options =>
            {
                options.UseDbContext<DapperDbContext, IDapperDbContext>("conStr", Configuration.GetConnectionString("MySqlString"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            var eventBus = serviceProvider.GetRequiredService<IEventBus>();
            eventBus.Subscribe();

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
        }
    }
}
