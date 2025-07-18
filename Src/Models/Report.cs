using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    /// <summary>
    /// Signalements de contenu inapproprié
    /// </summary>
    public class Report : AbstractModel<Report>
    {
        /// <summary>
        /// Utilisateur ayant signalé
        /// </summary>
        [BsonElement("reporter_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ReporterId { get; set; }

        /// <summary>
        /// Contenu signalé (post/commentaire/user)
        /// </summary>
        [BsonElement("content_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ContentId { get; set; }

        /// <summary>
        /// Type de contenu ('post', 'comment', 'user')
        /// </summary>
        [BsonElement("content_type")]
        public string? ContentType { get; set; }

        /// <summary>
        /// ID de l'utilisateur affecté (si applicable)
        /// </summary>
        [BsonElement("content_user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ContentUserId { get; set; }

        /// <summary>
        /// Raison du signalement
        /// </summary>
        [BsonElement("reason")]
        public string? Reason { get; set; }

        /// <summary>
        /// Détails supplémentaires sur l'action
        /// </summary>
        [BsonElement("details")]
        public string? Details { get; set; }

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

        public Report() { }

        public Report(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Reporter() => await BelongsTo<User>("reporter_id");
        public async Task<Post> ReportedPost() => await BelongsTo<Post>("content_id");
        public async Task<Comment> ReportedComment() => await BelongsTo<Comment>("content_id");
        public async Task<List<ReportLog>> Logs() => await HasMany<ReportLog>("report_id");
        public async Task<Ban> ResultingBan() => await HasOne<Ban>("report_id");

    }
}
