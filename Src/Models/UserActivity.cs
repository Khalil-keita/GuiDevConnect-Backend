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
    public class UserActivity : AbstractModel<UserActivity>
    {
        /// <summary>
        /// Utilisateur concerné
        /// </summary>
        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }

        /// <summary>
        /// Type d'activité ('login', 'post', 'vote', etc.)
        /// </summary>
        [BsonElement("activity_type")]
        public string? ActivityType { get; set; }

        /// <summary>
        /// ID de l'entité concernée
        /// </summary>
        [BsonElement("target_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? TargetId { get; set; }

        /// <summary>
        /// Type d'entité ('thread', 'post', etc.)
        /// </summary>
        [BsonElement("target_type")]
        public string? TargetType { get; set; }

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

        public UserActivity() { }

        public UserActivity(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> User() => await BelongsTo<User>("user_id");
        public async Task<Post> TargetPost() => await BelongsTo<Post>("target_id");
        public async Task<Thread> TargetThread() => await BelongsTo<Thread>("target_id");
    }
}
