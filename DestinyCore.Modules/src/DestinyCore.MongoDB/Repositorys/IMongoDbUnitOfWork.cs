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


        void StartSession();


        void CommitTransaction();
  
        void AbortTransaction();

        ValueTask StartSessionAsync();


        ValueTask CommitTransactionAsync();

        ValueTask AbortTransactionAsync();
    }
}
