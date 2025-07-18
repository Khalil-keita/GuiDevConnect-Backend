using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    /// <summary>
    /// Sondages associés aux posts
    /// </summary>
    public class Poll : AbstractModel<Poll>
    {
        /// <summary>
        /// Post parent
        /// </summary>
        [BsonElement("post_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? PostId { get; set; }

        /// <summary>
        /// Question du sondage
        /// </summary>
        [BsonElement("question")]
        public string? Question { get; set; }

        /// <summary>
        /// Date d'expiration
        /// </summary>
        [BsonElement("expires_at")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? ExpiresAt { get; set; }

        /// <summary>
        /// Thread lié
        /// </summary>
        [BsonElement("thread_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ThreadId { get; set; }

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

        public Poll() { }

        public Poll(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Thread> Thread() => await BelongsTo<Thread>("thread_id");
        public async Task<List<PollOption>> Options() => await HasMany<PollOption>("poll_id");
    }
}
