using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    /// <summary>
    /// Sanctions appliquées aux utilisateurs
    /// </summary>
    /// <remarks>
    /// Gère les bannissements temporaires et permanents ainsi que les avertissements
    /// </remarks>
    public class Ban : AbstractModel<Ban>
    {
        /// <summary>
        /// ID de l'utilisateur sanctionné
        /// </summary>
        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }

        /// <summary>
        /// Type de sanction (ban, mute, warning)
        /// </summary>
        [BsonElement("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Raison de la sanction
        /// </summary>
        [BsonElement("reason")]
        public string? Reason { get; set; }

        /// <summary>
        /// Date de début de la sanction
        /// </summary>
        [BsonElement("start_date")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Date de fin (null pour bannissement permanent)
        /// </summary>
        [BsonElement("end_date")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// ID du modérateur ayant appliqué la sanction
        /// </summary>
        [BsonElement("moderator_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ModeratorId { get; set; }

        /// <summary>
        /// Indique si la sanction est active
        /// </summary>
        [BsonElement("is_active")]
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Notes supplémentaires sur la sanction
        /// </summary>
        [BsonElement("notes")]
        public string? Notes { get; set; }

        public Ban() { }

        public Ban(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> BannedUser() => await BelongsTo<User>("user_id");
        public async Task<User> Moderator() => await BelongsTo<User>("moderator_id");
        public async Task<Report> SourceReport() => await BelongsTo<Report>("report_id");
    }
}
