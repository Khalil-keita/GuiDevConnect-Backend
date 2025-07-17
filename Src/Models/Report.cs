using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    /// <summary>
    /// Signalements de contenu inapproprié
    /// </summary>
    public class Report(IMongoDbContext dbContext) : AbstractModel<Report>(dbContext)
    {
        /// <summary>
        /// Utilisateur ayant signalé
        /// </summary>
        [BsonElement("reporter_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string ReporterId { get; set; }

        /// <summary>
        /// Contenu signalé (post/commentaire/user)
        /// </summary>
        [BsonElement("content_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string ContentId { get; set; }

        /// <summary>
        /// Type de contenu ('post', 'comment', 'user')
        /// </summary>
        [BsonElement("content_type")]
        public required string ContentType { get; set; }

        /// <summary>
        /// Raison du signalement
        /// </summary>
        [BsonElement("reason")]
        public required string Reason { get; set; }

        /// <summary>
        /// Statut du traitement ('pending', 'resolved', 'rejected')
        /// </summary>
        [BsonElement("status")]
        public string Status { get; set; } = "pending";

        /// <summary>
        /// Modérateur ayant traité le signalement
        /// </summary>
        [BsonElement("moderator_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ModeratorId { get; set; }

        /// <summary>
        /// Actions entreprises
        /// </summary>
        [BsonElement("actions_taken")]
        public List<string> ActionsTaken { get; set; } = [];

    }
}
