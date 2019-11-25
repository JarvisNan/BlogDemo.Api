using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlogDemo.Api.Extensions;
using BlogDemo.Core.Interfaces;
using BlogDemo.DB.DataBase;
using BlogDemo.DB.Repositories;
using BlogDemo.DB.Resources;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BlogDemo.Api
{
    public class StartupDevelopment
    {
        public static IConfiguration Configuration { get; set; }

        /// <summary>
        /// 构造函数添加配置文件读取
        /// </summary>
        /// <param name="configuration"></param>
        public StartupDevelopment(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //服务注册
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //配置数据库连接，    注入数据库
            services.AddDbContext<MyContext>(options => 
            {
                var a = Configuration["ConnectionStrings:DefaultConnection"];
                var connnectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlite(connnectionString);
                //options.UseSqlite("Data Source=BlogDemo.db");
            });

            //Https重定向配置
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 5001;
            });

            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //添加对象映射
            services.AddAutoMapper();

            //添加数据验证
            services.AddTransient<IValidator<PostResource>,PostResourceValidator>();
        }

        //配置中间件
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            //app.UseDeveloperExceptionPage();//程序报错之后页面显示错误信息（网页样式较好）
            app.UsrMyExceptionHandler(loggerFactory);

            app.UseHttpsRedirection();

            app.UseMvc();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
