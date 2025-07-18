using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    /// <summary>
    /// Association utilisateur-badge
    /// </summary>
    public class PostTag : AbstractModel<PostTag>
    {
        [BsonElement("post_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string PostId { get; set; }

        [BsonElement("tag_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string TagId { get; set; }

        public PostTag() { }

        public PostTag(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
