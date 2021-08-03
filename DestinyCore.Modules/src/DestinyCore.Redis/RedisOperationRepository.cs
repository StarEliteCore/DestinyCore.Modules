using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DestinyCore.Redis
{
    public class RedisOperationRepository :IRedisOperationRepository
    {
        private readonly ILogger<RedisOperationRepository> _logger;
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;
        //ILogger<RedisOperationRepository> logger,
        public RedisOperationRepository( ConnectionMultiplexer redis)
        {
            //_logger = logger;
            _redis = redis;
            _database = redis.GetDatabase();
        }

        private IServer GetServer()
        {
            var endpoint = _redis.GetEndPoints();//获取服务器上定义的所有端点
            return _redis.GetServer(endpoint.First());
        }

        public async Task Clear()
        {
            foreach (var endPoint in _redis.GetEndPoints())
            {
                var server = GetServer();
                foreach (var key in server.Keys())
                {
                    await _database.KeyDeleteAsync(key);
                }
            }
        }

        public async Task<bool> Exist(string key)
        {
            return await _database.KeyExistsAsync(key);
        }

        public async Task<string> Get(string key)
        {
            return await _database.StringGetAsync(key);
        }

        public async Task<TEntity> Get<TEntity>(string key)
        {
            var value = await _database.StringGetAsync(key);
            if (value.HasValue)
            {
                //需要用的反序列化,将Redis存储Byte[], 进行反序列化
                return JsonConvert.DeserializeObject<TEntity>(value);
            }
            else
            {
                return default;
            }
        }

        public async Task ListClearAsync(string redisKey)
        {
            await _database.ListTrimAsync(redisKey, 1, 0);
        }

        public async Task<long> ListDelRangeAsync(string redisKey, string redisValue, long type = 0)
        {
            return await _database.ListRemoveAsync(redisKey, redisValue, type);
        }

        public async Task<T> ListLeftPopAsync<T>(string redisKey) where T : class
        {
            var cacheValue = await _database.ListLeftPopAsync(redisKey);
            if (string.IsNullOrEmpty(cacheValue))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<T>(cacheValue);
        }

        public async Task<string> ListLeftPopAsync(string redisKey)
        {
            return await _database.ListLeftPopAsync(redisKey);
        }

        /// <summary>
        /// 列表头部插入值,如果不存在就先创建在插入
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> ListLeftPushAsync(string redisKey, string redisValue)
        {
            return await _database.ListLeftPushAsync(redisKey, redisValue);
        }

        /// <summary>
        /// 列表长度
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<long> ListLengthAsync(string redisKey)
        {
            return await _database.ListLengthAsync(redisKey);
        }

        /// <summary>
        /// 根据key获取RedisValue
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<RedisValue[]> ListRangeAsync(string redisKey)
        {
            return await _database.ListRangeAsync(redisKey);
        }

        /// <summary>
        /// 返回在该列表上键所对应的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> ListRangeAsync(string redisKey, int db = -1)
        {
            var result = await _database.ListRangeAsync(redisKey);
            return result.Select(o => o.ToString());
        }

        /// <summary>
        /// 根据索引获取指定位置数据
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> ListRangeAsync(string redisKey, int start, int stop)
        {
            var result = await _database.ListRangeAsync(redisKey, start, stop);
            return result.Select(o => o.ToString());
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素 反序列化
        /// 只能是对象集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<T> ListRightPopAsync<T>(string redisKey) where T : class
        {
            var cacheValue = await _database.ListRightPopAsync(redisKey);
            if (string.IsNullOrEmpty(cacheValue))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<T>(cacheValue);
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<string> ListRightPopAsync(string redisKey)
        {
            return await _database.ListRightPopAsync(redisKey);
        }

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> ListRightPushAsync(string redisKey, string redisValue)
        {
            return await _database.ListRightPushAsync(redisKey, redisValue);
        }

        /// <summary>
        /// 在列表尾部插入数组集合。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> ListRightPushAsync(string redisKey, IEnumerable<string> redisValue)
        {
            var redislist = new List<RedisValue>();
            foreach (var item in redisValue)
            {
                redislist.Add(item);
            }
            return await _database.ListRightPushAsync(redisKey, redislist.ToArray());
        }

        /// <summary>
        /// 移除某一个
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task Remove(string key)
        {
            await _database.KeyDeleteAsync(key);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="cacheTime"></param>
        /// <returns></returns>
        public async Task Set(string key, object value, TimeSpan cacheTime)
        {
            if (value != null)
            {
                //序列化，将object值生成RedisValue
                await _database.StringSetAsync(key, JsonConvert.SerializeObject(value), cacheTime);
            }
        }

        /// <summary>
        /// 保存 string
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="cacheTime"></param>
        /// <returns></returns>
        public async Task SetString(string key, string value, TimeSpan cacheTime)
        {
            if(value != null)
            {
                await _database.StringSetAsync(key, value, cacheTime);
            }
        }

        #region 分布式锁
        /// <summary>
        /// 分布式锁 Token。
        /// </summary>
        private static readonly RedisValue LockToken = Environment.MachineName;

        /// <summary>
        /// 获取锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiretime"></param>
        /// <returns></returns>
        public bool Lock(string key, TimeSpan expiretime)
        {
            return _database.LockTake(key, LockToken, expiretime);
        }
        /// <summary>
        /// 释放锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiretime"></param>
        /// <returns></returns>
        public bool UnLock(string key)
        {
            return _database.LockRelease(key, LockToken);
        }
        /// <summary>
        /// 获取锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiretime"></param>
        /// <returns></returns>
        public async Task<bool> LockAsync(string key, TimeSpan expiretime)
        {
            return await _database.LockTakeAsync(key, LockToken, expiretime);
        }
        /// <summary>
        /// 释放锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiretime"></param>
        /// <returns></returns>
        public async Task<bool> UnLockAsync(string key)
        {
            return await _database.LockReleaseAsync(key, LockToken);
        }


       
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {

            _redis?.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
