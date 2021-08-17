
using DestinyCore.Exceptions;
using DestinyCore.Extensions;
using MongoDB.Driver;
using System;
using System.Diagnostics.CodeAnalysis;


namespace DestinyCore.MongoDB.DbContexts
{
    public abstract class MongoDbContextBase :IDisposable
    {
        private readonly MongoDbContextOptions _options;
        public MongoDbContextBase([NotNull] MongoDbContextOptions options)
        {
            _options = options;
        }

        private string ConnectionString => _options.ConnectionString;

        public IMongoDatabase Database => GetDbContext();

        public IMongoClient MongoClient => Database.Client;

        public IMongoCollection<TEntity> Collection<TEntity>()
        {
            return Database.GetCollection<TEntity>(GetTableName<TEntity>());
        }

        private string GetTableName<TEntity>()
        {
            Type t = typeof(TEntity);
            var table = t.GetAttribute<MongoDBTableAttribute>();

            if (table == null)
            {
                return t.Name;
            }
            if (table.TableName.IsNullOrEmpty())
            {
                throw new AppException("Table name does not exist!");
            }
            return table.TableName;

        }


        public IMongoDatabase GetDbContext()
        {
            var mongoUrl = new MongoUrl(ConnectionString);
            var databaseName = mongoUrl.DatabaseName;
            if (databaseName.IsNullOrEmpty())
            {
                throw new AppException($"{mongoUrl}不存DatabaseName名!!!");
            }

            var database = new MongoClient(mongoUrl).GetDatabase(databaseName);
            return database;
        }

        public void Dispose()
        {
        
        }

     
    }
}
