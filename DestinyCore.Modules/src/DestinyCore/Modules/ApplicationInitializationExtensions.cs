using DestinyCore.Dependency;
using DestinyCore.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DestinyCore.Modules
{
    public static class ApplicationInitializationExtensions

    {

        public static IApplicationBuilder GetApplicationBuilder(this ApplicationContext applicationContext)
        {
            return applicationContext.ServiceProvider.GetRequiredService<IObjectAccessor<IApplicationBuilder>>().Value;
        }


        /// <summary>
        /// 得到配置文件
        /// </summary>
        /// <param name="applicationContext"></param>
        /// <returns></returns>
        public static AppOptionSettings GetAppSettings(this ApplicationContext applicationContext)
        {
            return applicationContext.ServiceProvider.GetRequiredService<IObjectAccessor<AppOptionSettings>>().Value;
        }

    }
}
