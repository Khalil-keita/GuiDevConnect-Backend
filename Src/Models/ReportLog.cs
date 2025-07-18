using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    /// <summary>
    /// Journal des actions de modération effectuées sur le forum
    /// </summary>
    /// <remarks>
    /// Audit trail pour tracer toutes les actions des modérateurs/administrateurs
    /// </remarks>
    public class ReportLog : AbstractModel<ReportLog>
    {
        /// <summary>
        /// Type d'action effectuée (suppression, avertissement, etc.)
        /// </summary>
        [BsonElement("action_type")]
        public string? ActionType { get; set; }

        /// <summary>
        /// ID du modérateur ayant effectué l'action
        /// </summary>
        [BsonElement("report_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ReportId { get; set; }

        /// <summary>
        /// Raison de l'action de modération
        /// </summary>
        [BsonElement("reason")]
        public string? Reason { get; set; }

        /// <summary>
        /// IP associée à l'action
        /// </summary>
        [BsonElement("ip_address")]
        public string? IpAddress { get; set; }

        /// <summary>
        /// Modérateur ayant traité le signalement
        /// </summary>
        [BsonElement("moderator_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ModeratorId { get; set; }

        /// <summary>
        /// Utilisateur ayant signalé
        /// </summary>
        [BsonElement("reporter_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ReporterId { get; set; }

        public ReportLog() { }

        public ReportLog(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Report> Report() => await BelongsTo<Report>("report_id");
        public async Task<User> Moderator() => await BelongsTo<User>("moderator_id");
    }
}
