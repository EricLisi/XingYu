using KGM.Framework.Application.AutoMapping;
using KGM.Framework.Infrastructure;
using KGM.Framework.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;

namespace KGM.Framework.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        public string groupid { get; set; }
        #region 注入配置服务，用于读取配置文件
        /// <summary>
        /// 
        /// </summary>
        public static IConfiguration Configuration { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion 

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        { 
            services.AddMvc().AddJsonOptions(options => { options.SerializerSettings.ContractResolver = new DefaultContractResolver(); });
            #region 注册验证JWT
            double expTime = 20;
            AuthorizeConfig.Instance(expTime).RegisterAuth(services);
            #endregion

            #region Swagger 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v0.1.0",
                    Title = "KgmSoft WebAPI",
                    Description = "上海金戈马软件API说明",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "上海金戈马软件有限公司", Url = "http://www.kgmsoft.com.cn" }
                });

                #region 加载xml注释 
                var xmlPath = PathUtil.GetAbsolutePath("KGM.Framework.WebApi.xml");//这个就是刚刚配置的xml文件名
                var dtoXmlPath = PathUtil.GetAbsolutePath("KGM.Framework.Application.xml");//这个就是刚刚配置的xml文件名
                c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改
                c.IncludeXmlComments(dtoXmlPath);
                #endregion

                #region Token绑定到ConfigureServices
                var security = new Dictionary<string, IEnumerable<string>> { { "上海金戈马软件WebAPI", new string[] { } }, };
                c.AddSecurityRequirement(security);
                c.AddSecurityDefinition("上海金戈马软件WebAPI", new ApiKeyScheme
                {
                    Description = "请输入token",
                    Name = "Authorization",//jwt默认的参数名称
                    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
                    Type = "apiKey"
                });
                #endregion

            });
            #endregion 

            #region 跨域注册 
            services.AddCors();
            #endregion

            #region MVC注册
            services.AddMvc(options =>
            {
                options.Filters.Add<HttpGlobalExceptionFilter>();
                options.Filters.Add<HttpGlobalExcuteFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            #endregion

            #region 注册自动映射  
            AutoMapperConfig.RegisterAutoMapper(services);
            #endregion

            #region 注册IoC
            return IoCContainer.RegisterIoC(services);
            #endregion 
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Swagger 
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api帮助文档 V1");
            });

            #endregion

            #region 实现跨域 
            app.UseCors(builder =>
            {
                builder.WithHeaders("*");
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            });
            #endregion

            #region Token验证
            AuthorizeConfig.Instance().ConfigAuth(app);
            #endregion 

            #region MVC
            app.UseMvc();
            #endregion
        }
    }
}
