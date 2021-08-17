using DestinyCore.Extensions;
using DestinyCore.MongoDB.DbContexts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DestinyCore.MongoDB.Repositorys
{
    public class MongoDbUnitOfWork<TMongoDbContext> : IMongoDbUnitOfWork
        where TMongoDbContext : MongoDbContextBase
    {

        private TMongoDbContext _context = null;
        private ILogger _logger;
        public MongoDbUnitOfWork(TMongoDbContext context,ILoggerFactory loggerFactory)
        {

            context.NotNull(nameof(TMongoDbContext));
            _context = context;
            _logger = loggerFactory.CreateLogger<MongoDbUnitOfWork<TMongoDbContext>>() ?? NullLogger<MongoDbUnitOfWork<TMongoDbContext>>.Instance;
        }

        private IClientSessionHandle ClientSession { get; set; }
        /// <summary>
        /// 开启事务
        /// </summary>
        public virtual void StartSession()
        {
            _logger.LogInformation("开启事务中....");

            if (Database?.Client != null&&ClientSession==null)
            {
                ClientSession = Database.Client.StartSession();
                _logger.LogInformation("开启事务成功....");
            }
         

            HasCommitted = false;
            _logger.LogInformation("事务开启结束....");
        }

        public virtual void CommitTransaction()
        {
            _logger.LogInformation("事务提交中....");
            if (HasCommitted || ClientSession == null)
            {
                _logger.LogInformation("事务已提交或释放没有产生任何效果....");
                return;
            }
            ClientSession.CommitTransaction();
            _logger.LogInformation("成功提交事务....");
            HasCommitted = true;
        }

        public virtual void AbortTransaction()
        {
            _logger.LogInformation("事务回滚中....");
            if (ClientSession != null)
            {
                 ClientSession.AbortTransaction();
                _logger.LogInformation("事务回滚成功....");
            }
            _logger.LogInformation("事务结束回滚....");
            HasCommitted = true;
        }

        public async ValueTask StartSessionAsync()
        {
            _logger.LogInformation("开启异步事务中....");

            if (Database?.Client != null && ClientSession == null)
            {
                ClientSession =await Database.Client.StartSessionAsync().ConfigureAwait(false);
                _logger.LogInformation("开启异步事务成功....");
            }


            HasCommitted = false;
            _logger.LogInformation("异步事务开启结束....");
        }

        public virtual async ValueTask CommitTransactionAsync()
        {
            _logger.LogInformation("异步事务提交中....");
            if (HasCommitted || ClientSession == null)
            {
                _logger.LogInformation("异步事务已提交或释放没有产生任何效果....");
                return;
            }
            await ClientSession.CommitTransactionAsync().ConfigureAwait(false);
            _logger.LogInformation("成功提交异步事务....");
            HasCommitted = true;
        }

        public virtual async ValueTask AbortTransactionAsync()
        {
            _logger.LogInformation("异步事务回滚中....");
            if (ClientSession != null)
            {
               await ClientSession.AbortTransactionAsync().ConfigureAwait(false);
                _logger.LogInformation("异步事务回滚成功....");
            }
            _logger.LogInformation("异步事务结束回滚....");
            HasCommitted = true;
        }


        /// <summary>
        /// 是否提交
        /// </summary>
        /// <returns></returns>
        public bool HasCommit()
        {
            return HasCommitted;
        }

        /// <summary>
        /// 是否提交
        /// </summary>
        private bool HasCommitted { get; set; }
        public IMongoDatabase Database => _context?.Database;

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            this._context?.Dispose();
            ClientSession?.Dispose();
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        private bool _disposed;

        public MongoDbContextBase GetDbContext()
        {
            return _context;
        }

        public ILogger GetLogger()
        {
            return _logger;
        }
    }
}
