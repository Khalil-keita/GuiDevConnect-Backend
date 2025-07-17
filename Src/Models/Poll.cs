using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    /// <summary>
    /// Sondages associés aux posts
    /// </summary>
    public class Poll(IMongoDbContext dbContext) : AbstractModel<Poll>(dbContext)
    {
        /// <summary>
        /// Post parent
        /// </summary>
        [BsonElement("post_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string PostId { get; set; }

        /// <summary>
        /// Question du sondage
        /// </summary>
        [BsonElement("question")]
        public required string Question { get; set; }

        /// <summary>
        /// Options disponibles
        /// </summary>
        [BsonElement("options")]
        public List<PollOption> Options { get; set; } = [];

        /// <summary>
        /// Date d'expiration
        /// </summary>
        [BsonElement("expires_at")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? ExpiresAt { get; set; }

        /// <summary>
        /// Sondage multi-choix
        /// </summary>
        [BsonElement("is_multi_choice")]
        public bool IsMultiChoice { get; set; } = false;

        /// <summary>
        /// Résultats visibles avant vote
        /// </summary>
        [BsonElement("show_results_before_voting")]
        public bool ShowResultsBeforeVoting { get; set; } = false;
    }

    public class PollOption
    {
        /// <summary>
        /// Texte de l'option
        /// </summary>
        [BsonElement("text")]
        public string Text { get; set; }

        /// <summary>
        /// Nombre de votes
        /// </summary>
        [BsonElement("vote_count")]
        public int VoteCount { get; set; } = 0;

        /// <summary>
        /// Pourcentage calculé (peut être stocké ou calculé à la volée)
        /// </summary>
        [BsonElement("percentage")]
        public double Percentage { get; set; } = 0;
    }
}
