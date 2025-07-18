using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    public class Notification : AbstractModel<Notification>
    {
        /// <summary>
        /// Utilisateur destinataire
        /// </summary>
        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }

        /// <summary>
        /// Type de notification
        /// </summary>
        [BsonElement("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Message de notification
        /// </summary>
        [BsonElement("message")]
        public string? Message { get; set; }

        /// <summary>
        /// Entité concernée
        /// </summary>
        [BsonElement("related_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? RelatedId { get; set; }

        /// <summary>
        /// Type d'entité concernée
        /// </summary>
        [BsonElement("related_type")]
        public string? RelatedType { get; set; }

        /// <summary>
        /// Indique si la notification a été lue
        /// </summary>
        [BsonElement("is_read")]
        public bool IsRead { get; set; } = false;

        public Notification() { }

        public Notification(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> User() => await BelongsTo<User>("user_id");
        public async Task<Post> PostSource() => await BelongsTo<Post>("related_id");
        public async Task<Comment> CommentSource() => await BelongsTo<Comment>("related_id");

    }
}
