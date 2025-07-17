using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    /// <summary>
    /// Statistiques de l'utilisateur (sous-document)
    /// </summary>
    public class UserStatistics(IMongoDbContext dbContext) : AbstractModel<UserStatistics>(dbContext)
    {
        /// <summary>
        /// Nombre total de posts
        /// </summary>
        [BsonElement("post_count")]
        public int PostCount { get; set; } = 0;

        /// <summary>
        /// Nombre total de commentaires
        /// </summary>
        [BsonElement("comment_count")]
        public int CommentCount { get; set; } = 0;

        /// <summary>
        /// Nombre total de likes reçus
        /// </summary>
        [BsonElement("likes_received")]
        public int LikesReceived { get; set; } = 0;

        /// <summary>
        /// Date du dernier post
        /// </summary>
        [BsonElement("last_post_date")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? LastPostDate { get; set; }

        /// <summary>
        /// Date du dernier commentaire
        /// </summary>
        [BsonElement("last_comment_date")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? LastCommentDate { get; set; }
    }
}
