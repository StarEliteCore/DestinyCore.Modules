using DestinyCore.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace DestinyCore.Redis
{
    public abstract class RedisModuleBase : AppModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddDefaultRedisRepository();
            AddRedis(context.Services);
        }

        public abstract void AddRedis(IServiceCollection service);
    }
}
