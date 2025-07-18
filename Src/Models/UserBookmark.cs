using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    /// <summary>
    /// Marque-pages utilisateur pour threads/posts
    /// </summary>
    public class UserBookmark : AbstractModel<UserBookmark>
    {
        /// <summary>
        /// Utilisateur propriétaire du bookmark
        /// </summary>
        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }

        /// <summary>
        /// ID de la cible (thread/post)
        /// </summary>
        [BsonElement("target_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? TargetId { get; set; }

        /// <summary>
        /// Type de cible ('thread' ou 'post')
        /// </summary>
        [BsonElement("target_type")]
        public string? TargetType { get; set; }

        /// <summary>
        /// Notes personnelles de l'utilisateur
        /// </summary>
        [BsonElement("notes")]
        public string? Notes { get; set; }

        public UserBookmark() { }

        public UserBookmark(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> User() => await BelongsTo<User>("user_id");
        public async Task<Post> BookmarkedPost() => await BelongsTo<Post>("target_id");
        public async Task<Thread> BookmarkedThread() => await BelongsTo<Thread>("target_id");
    }
}
