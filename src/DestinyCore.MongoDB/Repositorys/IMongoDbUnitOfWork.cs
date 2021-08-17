using DestinyCore.MongoDB.DbContexts;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DestinyCore.MongoDB.Repositorys
{
    public interface  IMongoDbUnitOfWork : IDisposable
    {
        IMongoDatabase Database { get; }

        ILogger GetLogger();

        /// <summary>
        /// 得到DB上下文
        /// </summary>
        /// <returns></returns>
        MongoDbContextBase GetDbContext();


        /// <summary>
        /// 是否提交
        /// </summary>
        /// <returns></returns>
        bool HasCommit();


        /// <summary>
        /// 开启事务
        /// </summary>
        void StartSession();


        /// <summary>
        /// 提交事务
        /// </summary>
        void CommitTransaction();
  
        /// <summary>
        /// 回滚事务
        /// </summary>
        void AbortTransaction();

        /// <summary>
        /// 异步开启事务
        /// </summary>
        /// <returns></returns>
        ValueTask StartSessionAsync();

        /// <summary>
        /// 异步提交事务
        /// </summary>
        /// <returns></returns>
        ValueTask CommitTransactionAsync();

        ValueTask AbortTransactionAsync();
    }
}
