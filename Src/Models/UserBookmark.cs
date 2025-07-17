using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    /// <summary>
    /// Marque-pages utilisateur pour threads/posts
    /// </summary>
    public class UserBookmark(IMongoDbContext dbContext) : AbstractModel<UserBookmark>(dbContext)
    {
        /// <summary>
        /// Utilisateur propriétaire du bookmark
        /// </summary>
        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string UserId { get; set; }

        /// <summary>
        /// ID de la cible (thread/post)
        /// </summary>
        [BsonElement("target_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string TargetId { get; set; }

        /// <summary>
        /// Type de cible ('thread' ou 'post')
        /// </summary>
        [BsonElement("target_type")]
        public required string TargetType { get; set; }

        /// <summary>
        /// Notes personnelles de l'utilisateur
        /// </summary>
        [BsonElement("notes")]
        public string? Notes { get; set; }
    }
}
