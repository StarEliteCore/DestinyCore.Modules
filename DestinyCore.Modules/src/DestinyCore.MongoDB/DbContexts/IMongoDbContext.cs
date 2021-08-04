using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DestinyCore.MongoDB.DbContexts
{
    public interface IMongoDbContext
    {
        IMongoDatabase Database { get; }

        IMongoCollection<TEntity> Collection<TEntity>();



        IMongoClient MongoClient { get; }
        IMongoDatabase GetDbContext();

    }
}
