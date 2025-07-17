using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    public class Comment : AbstractModel<Comment>
    {
        public Comment() {}

        public Comment(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

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

    }
}
