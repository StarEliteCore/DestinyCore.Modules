using DestinyCore.Entity;
using MongoDB.Bson;

namespace DestinyCore.MongoDB
{
    public abstract class MongoEntity : IEntity<ObjectId>
    {

        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
    }
}
