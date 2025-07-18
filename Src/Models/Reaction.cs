using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    public class Reaction : AbstractModel<Reaction>
    {
        /// <summary>
        /// Utilisateur qui a réagi
        /// </summary>
        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }

        /// <summary>
        /// Cible de la réaction (post/comment)
        /// </summary>
        [BsonElement("target_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? TargetId { get; set; }

        /// <summary>
        /// Type de cible ('post' ou 'comment')
        /// </summary>
        [BsonElement("target_type")]
        public string? TargetType { get; set; }

        /// <summary>
        /// Type de réaction ('like', 'love', etc.)
        /// </summary>
        [BsonElement("reaction_type")]
        public string ReactionType { get; set; } = "like";

        /// <summary>
        /// Emoji associé
        /// </summary>
        [BsonElement("emoji")]
        public string? Emoji { get; set; }

        public Reaction() { }

        public Reaction(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> User() => await BelongsTo<User>("user_id");
        public async Task<Post> PostTarget() => await BelongsTo<Post>("target_id");
        public async Task<Comment> CommentTarget() => await BelongsTo<Comment>("target_id");
    }

}
