using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    public class Reaction : AbstractModel<Reaction>
    {
        public Reaction() { }

        public Reaction(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Utilisateur qui a réagi
        /// </summary>
        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string UserId { get; set; }

        /// <summary>
        /// Cible de la réaction (post/comment)
        /// </summary>
        [BsonElement("target_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string TargetId { get; set; }

        /// <summary>
        /// Type de cible ('post' ou 'comment')
        /// </summary>
        [BsonElement("target_type")]
        public required ReactionTargetType TargetType { get; set; }

        /// <summary>
        /// Type de réaction ('like', 'love', etc.)
        /// </summary>
        [BsonElement("reaction_type")]
        public ReactionType ReactionType { get; set; } = ReactionType.Like;
    }

    public enum ReactionTargetType
    {
        Post,
        Comment
    }

    public enum ReactionType
    {
        Like,
        Love,
        Laugh,
        Wow,
        Sad,
        Angry
    }
}
