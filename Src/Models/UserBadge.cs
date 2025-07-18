using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    /// <summary>
    /// Association utilisateur-badge
    /// </summary>
    public class UserBadge : AbstractModel<UserBadge>
    {
        public UserBadge() { }

        public UserBadge(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string UserId { get; set; }

        [BsonElement("badge_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string BadgeId { get; set; }

        [BsonElement("earned_at")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime EarnedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("is_featured")]
        public bool IsFeatured { get; set; } = false;
    }
}
