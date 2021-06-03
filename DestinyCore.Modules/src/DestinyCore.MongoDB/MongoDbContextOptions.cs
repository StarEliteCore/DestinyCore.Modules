
using DestinyCore.MongoDB.Infrastructure;

namespace DestinyCore.MongoDB
{
    public class MongoDbContextOptions : IMongoDbContextOptions
    {
        public string ConnectionString { get; set; }
    }
}
