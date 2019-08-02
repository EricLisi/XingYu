using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace KGM.Framework.Application.AutoMapping
{
    /// <summary>
    /// 
    /// </summary>
    public class AutoMapperConfig
    {
        /// <summary>
        /// 注册AutoMapper
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper();
          

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
        }
    }
}
