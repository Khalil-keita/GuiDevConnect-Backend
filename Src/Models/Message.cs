using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    public class Message(IMongoDbContext dbContext) : AbstractModel<Message>(dbContext)
    {
        /// <summary>
        /// Expéditeur du message
        /// </summary>
        [BsonElement("sender_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string SenderId { get; set; }

        /// <summary>
        /// Destinataire du message
        /// </summary>
        [BsonElement("recipient_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string RecipientId { get; set; }

        /// <summary>
        /// Contenu textuel
        /// </summary>
        [BsonElement("content")]
        public required string Content { get; set; }

        /// <summary>
        /// Indique si le message a été lu
        /// </summary>
        [BsonElement("is_read")]
        public bool IsRead { get; set; } = false;

        /// <summary>
        /// Date de lecture (si applicable)
        /// </summary>
        [BsonElement("read_at")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? ReadAt { get; set; }

        /// <summary>
        /// Soft delete pour l'expéditeur
        /// </summary>
        [BsonElement("is_deleted_for_sender")]
        public bool IsDeletedForSender { get; set; } = false;

        /// <summary>
        /// Soft delete pour le destinataire
        /// </summary>
        [BsonElement("is_deleted_for_recipient")]
        public bool IsDeletedForRecipient { get; set; } = false;
    }
}
