using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    public class Comment : AbstractModel<Comment>
    {
        /// <summary>
        /// Contenu textuel du commentaire
        /// </summary>
        [BsonElement("content")]
        public string? Content { get; set; }

        /// <summary>
        /// Auteur du commentaire
        /// </summary>
        [BsonElement("author_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? AuthorId { get; set; }

        /// <summary>
        /// Publication parente
        /// </summary>
        [BsonElement("post_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? PostId { get; set; }

        /// <summary>
        /// Commentaire parent (pour les réponses)
        /// </summary>
        [BsonElement("parent_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ParentId { get; set; }

        /// <summary>
        /// Nombre de likes/reactions
        /// </summary>
        [BsonElement("like_count")]
        public int LikeCount { get; set; } = 0;

        public Comment() { }

        public Comment(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<User> Author() => await BelongsTo<User>("author_id");
        public async Task<Post> Post() => await BelongsTo<Post>("post_id");
        public async Task<List<Reaction>> Reactions() => await HasMany<Reaction>("target_id");
        public async Task<List<Report>> Reports() => await HasMany<Report>("content_id");
        public async Task<Comment> Parent() => await BelongsTo<Comment>("parent_id");
        public async Task<List<Comment>> Replies() => await HasMany<Comment>("parent_id");



    }
}
