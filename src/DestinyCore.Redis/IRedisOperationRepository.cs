using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DestinyCore.Redis
{
    //todo 名字不应该这样叫吧？ 直接叫RedisCache
    /// <summary>
    /// Redis接口
    /// </summary>
    public interface IRedisOperationRepository : IDisposable
    {
        ///异步加Async

        /// <summary>
        /// 获取Redis缓存值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> Get(string key);

        /// <summary>
        /// 获取值,并序列化
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<TEntity> Get<TEntity>(string key);

        /// <summary>
        /// 保存(序列化)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="cacheTime"></param>
        /// <returns></returns>
        Task Set(string key, object value, TimeSpan cacheTime);

        /// <summary>
        /// 保存string类型
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="cacheTime"></param>
        /// <returns></returns>
        Task SetString(string key, string value, TimeSpan cacheTime);
        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> Exist(string key);

        /// <summary>
        /// 移除某一个
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task Remove(string key);

        /// <summary>
        /// 全部清除
        /// </summary>
        /// <returns></returns>
        Task Clear();

        /// <summary>
        /// 根据key获取RedisValue
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<RedisValue[]> ListRangeAsync(string redisKey);

        /// <summary>
        /// 在列表头部插入值,如果健不存在,先创建在插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        Task<long> ListLeftPushAsync(string redisKey, string redisValue);

        /// <summary>
        /// 在列表尾部插入值 如果不存在 就先创建再插入
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        Task<long> ListRightPushAsync(string redisKey, string redisValue);

        /// <summary>
        /// 在列表尾部插入数值祝贺,如果键不存在 先创建再插入
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        Task<long> ListRightPushAsync(string redisKey, IEnumerable<string> redisValue);

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素  反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<T> ListLeftPopAsync<T>(string redisKey) where T : class;

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<T> ListRightPopAsync<T>(string redisKey) where T : class;

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<string> ListLeftPopAsync(string redisKey);

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<string> ListRightPopAsync(string redisKey);

        /// <summary>
        /// 列表长度
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<long> ListLengthAsync(string redisKey);

        /// <summary>
        /// 返回在该列表上键所对应的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> ListRangeAsync(string redisKey, int db = -1);

        /// <summary>
        /// 根据索引获取指定位置数据
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> ListRangeAsync(string redisKey, int start, int stop);

        /// <summary>
        /// 删除List中的元素 并返回删除个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<long> ListDelRangeAsync(string redisKey, string redisValue, long type = 0);

        /// <summary>
        /// 清空List
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task ListClearAsync(string redisKey);

        /// <summary>
        /// 释放锁
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> UnLockAsync(string key);

        /// <summary>
        /// 获取锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        Task<bool> LockAsync(string key, TimeSpan expiretime);

        /// <summary>
        /// 释放锁
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool UnLock(string key);

        /// <summary>
        /// 获取锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiretime"></param>
        /// <returns></returns>
        bool Lock(string key, TimeSpan expiretime);


    }
}
