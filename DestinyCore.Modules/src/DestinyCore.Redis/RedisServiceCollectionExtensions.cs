using DestinyCore.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RedisServiceCollectionExtensions
    {
        public static void AddRedis(this IServiceCollection service, string connectionString)
        {
            if(service == null)
            {
                throw new ApplicationException(nameof(service));
            }

            //配置启动Redis服务
            service.AddSingleton<ConnectionMultiplexer>(sp =>
            {
                var configuration = ConfigurationOptions.Parse(connectionString, true);
                configuration.ResolveDns = true;
                return ConnectionMultiplexer.Connect(configuration);
            });
        }

        public static void AddDefaultRedisRepository(this IServiceCollection services)
        {
            services.AddTransient<IRedisOperationRepository, RedisOperationRepository>();
        }
    }
}
