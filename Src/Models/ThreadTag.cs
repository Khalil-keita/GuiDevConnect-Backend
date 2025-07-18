using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    /// <summary>
    /// Association utilisateur-badge
    /// </summary>
    public class ThreadTag : AbstractModel<ThreadTag>
    {
        [BsonElement("post_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string PostId { get; set; }

        [BsonElement("thread_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string ThreadId { get; set; }

        public ThreadTag() { }

        public ThreadTag(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
