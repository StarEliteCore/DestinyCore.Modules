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
        /// <summary>
        /// 分布式锁单元测试
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DistributedLocker_Test()
        {
            var key = "Order002";
            var lockerkey = await _redisOperationRepository.LockAsync(key, TimeSpan.FromSeconds(180));
            try
            {
                if (lockerkey)
                {
                    Console.WriteLine("获取到了锁");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                await _redisOperationRepository.UnLockAsync(key);
            }
        }

        [Fact]
        public async Task ListLeftPushAsync_Test()
        {
            var str = "插入头部1";
            var key = "testq";
            await _redisOperationRepository.ListLeftPushAsync(key, "插入头部3"); //插入头部
            await _redisOperationRepository.ListRightPushAsync(key, "插入屁股3"); //插入屁股
            var newkey = await _redisOperationRepository.ListRangeAsync(key);
            
        }

        [Fact]
        public async Task ListRightPopAsync_Test()
        {
            var key = "testq";
            //await _redisOperationRepository.ListLeftPopAsync(key);//删除头部
            await _redisOperationRepository.ListRightPopAsync(key); //删除屁股
            var newkey = await _redisOperationRepository.ListRangeAsync(key);

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

