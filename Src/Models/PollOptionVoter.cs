using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    public class PollOptionVoter: AbstractModel<PollOptionVoter>
    {
        /// <summary>
        /// Utilisateur qui a voter pour option
        /// </summary>
        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }

        /// <summary>
        /// Option voté
        /// </summary>
        [BsonElement("poll_option_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? PollOptionId { get; set; }

        public PollOptionVoter() { }

        public PollOptionVoter(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
