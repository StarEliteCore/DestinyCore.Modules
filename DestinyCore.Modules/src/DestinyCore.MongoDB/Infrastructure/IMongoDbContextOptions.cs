namespace DestinyCore.MongoDB.Infrastructure
{
    public interface IMongoDbContextOptions
    {

        string ConnectionString { get; set; }
    }
}
