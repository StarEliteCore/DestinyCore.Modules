using System.Diagnostics.CodeAnalysis;

namespace DestinyCore.MongoDB.DbContexts
{
    public class DefaultMongoDbContext : MongoDbContextBase
    {
        public DefaultMongoDbContext([NotNull] MongoDbContextOptions options) : base(options)
        {

        }

    }
}
