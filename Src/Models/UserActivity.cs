using backEnd.Core.Model;
using backEnd.Core.Mongo;
using backEnd.Src.Dtos;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    /// <summary>
    /// Suivi des activités utilisateur
    /// </summary>
    public class UserActivity(IMongoDbContext dbContext) : AbstractModel<UserActivity>(dbContext)
    {
        /// <summary>
        /// Utilisateur concerné
        /// </summary>
        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string UserId { get; set; }

        /// <summary>
        /// Type d'activité ('login', 'post', 'vote', etc.)
        /// </summary>
        [BsonElement("activity_type")]
        public required string ActivityType { get; set; }

        /// <summary>
        /// ID de l'entité concernée
        /// </summary>
        [BsonElement("target_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string TargetId { get; set; }

        /// <summary>
        /// Type d'entité ('thread', 'post', etc.)
        /// </summary>
        [BsonElement("target_type")]
        public required string TargetType { get; set; }

        /// <summary>
        /// Adresse IP (hashée)
        /// </summary>
        [BsonElement("ip_hash")]
        public string? IpHash { get; set; }

        /// <summary>
        /// User agent du navigateur
        /// </summary>
        [BsonElement("user_agent")]
        public string? UserAgent { get; set; }

        /// <summary>
        /// Coordonées GPS
        /// </summary>
        [BsonElement("coordinates")]
        public Coordinates? Coordinates { get; set; }
    }
}
