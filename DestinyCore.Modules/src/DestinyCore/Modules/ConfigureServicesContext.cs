
using DestinyCore.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace DestinyCore.Modules
{
    /// <summary>
    /// 配置服务上下文
    /// </summary>
    public class ConfigureServicesContext
    {
        public ConfigureServicesContext(IServiceCollection services)
        {
            Services = services;

        }
        public IServiceCollection Services { get; }


        public IConfiguration GetConfiguration()
        {

            var implemenInstance = Services.GetSingletonInstanceOrNull<IConfiguration>();
            return implemenInstance;
        }



        
    }
}
