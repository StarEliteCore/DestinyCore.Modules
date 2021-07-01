using DestinyCore.Dependency;
using DestinyCore.Modules;
using DestinyCore.Redis;
using DestinyCore.TestBase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DestinyCore.Test
{
    public class DestinyCoreRedisTest : IntegratedTest<RedisModelule>
    {
        private readonly IRedisOperationRepository _redisOperationRepository = null;

        public DestinyCoreRedisTest()
        {
            _redisOperationRepository = ServiceProvider.GetService<IRedisOperationRepository>();
        }

        [Fact]
        public async Task SetString_Test()
        {
            var str = "爱上大流口水大门口辣三丁没蓝了";
            var key = "test1";
            await _redisOperationRepository.SetString(key, str, TimeSpan.FromSeconds(2000));
            var newkey= await _redisOperationRepository.Get(key);
            Assert.Equal(str, newkey);
        }
    }

    [DependsOn(typeof(DependencyAppModule))]
    public class RedisModelule : RedisModuleBase
    {
        public override void AddRedis(IServiceCollection service)
        {
            service.AddRedis("192.168.0.166:6379,password = redis123,defaultDatabase=6,prefix = test_");
        }
    }

    //public class TestRedis : RedisEntity,
}

