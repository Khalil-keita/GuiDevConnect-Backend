using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    public class Notification : AbstractModel<Notification>
    {
        public Notification() { }

        public Notification(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Utilisateur destinataire
        /// </summary>
        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string UserId { get; set; }

        /// <summary>
        /// Type de notification
        /// </summary>
        [BsonElement("type")]
        public required string Type { get; set; }

        /// <summary>
        /// Message de notification
        /// </summary>
        [BsonElement("message")]
        public required string Message { get; set; }

        /// <summary>
        /// Entité concernée
        /// </summary>
        [BsonElement("related_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string RelatedId { get; set; }

        /// <summary>
        /// Type d'entité concernée
        /// </summary>
        [BsonElement("related_type")]
        public required string RelatedType { get; set; }

        /// <summary>
        /// Indique si la notification a été lue
        /// </summary>
        [BsonElement("is_read")]
        public bool IsRead { get; set; } = false;

    }
}
