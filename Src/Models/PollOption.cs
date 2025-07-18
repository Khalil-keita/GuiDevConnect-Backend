using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    public class PollOption: AbstractModel<PollOption>
    {
        /// <summary>
        /// Texte de l'option
        /// </summary>
        [BsonElement("text")]
        public string? Text { get; set; }

        /// <summary>
        /// Poll associé
        /// </summary>
        [BsonElement("poll_id")]
        public string? PollId { get; set; }

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

        public PollOption() { }

        public PollOption(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Poll> Poll() => await BelongsTo<Poll>("poll_id");
        public async Task<List<User>> Voters() => await BelongsToMany<User>("poll_option_voter");
    }
}
