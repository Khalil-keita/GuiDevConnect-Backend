﻿using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Net.Mail;

namespace backEnd.Src.Models
{
    /// <summary>
    /// Représente une publication dans le forum
    /// </summary>
    public class Post : AbstractModel<Post>
    {
        /// <summary>
        /// Titre de la publication (obligatoire)
        /// </summary>
        [BsonElement("title")]
        public string? Title { get; set; }

        /// <summary>
        /// Contenu textuel de la publication (markdown/HTML)
        /// </summary>
        [BsonElement("content")]
        public string? Content { get; set; }

        /// <summary>
        /// Référence à l'auteur de la publication
        /// </summary>
        [BsonElement("author_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? AuthorId { get; set; }

        /// <summary>
        /// Catégorie principale de la publication
        /// </summary>
        [BsonElement("category_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CategoryId { get; set; }

        /// <summary>
        /// Thread principale de la publication
        /// </summary>
        [BsonElement("thread_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ThreadId { get; set; }

        /// <summary>
        /// Nombre total de vues
        /// </summary>
        [BsonElement("view_count")]
        public int ViewCount { get; set; } = 0;

        /// <summary>
        /// Nombre total de likes/reactions
        /// </summary>
        [BsonElement("like_count")]
        public int LikeCount { get; set; } = 0;

        /// <summary>
        /// Nombre total de commentaires
        /// </summary>
        [BsonElement("comment_count")]
        public int CommentCount { get; set; } = 0;

        /// <summary>
        /// Indique si le post est épinglé en haut de la liste
        /// </summary>
        [BsonElement("is_pinned")]
        public bool IsPinned { get; set; } = false;

        /// <summary>
        /// Indique si les commentaires sont fermés
        /// </summary>
        [BsonElement("is_closed")]
        public bool IsClosed { get; set; } = false;

        /// <summary>
        /// Date du dernier commentaire (pour le tri)
        /// </summary>
        [BsonElement("last_comment_date")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? LastCommentDate { get; set; }

        /// <summary>
        /// Pièces jointes (images, fichiers, etc.)
        /// </summary>
        [BsonElement("attachments")]
        public List<PostAttachment> Attachments { get; set; } = [];

        public Post() { }

        public Post(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Author() => await BelongsTo<User>("author_id");
        public async Task<Thread> Thread() => await BelongsTo<Thread>("thread_id");
        public async Task<List<Comment>> Comments() => await HasMany<Comment>("post_id");
        public async Task<List<Reaction>> Reactions() => await HasMany<Reaction>("target_id");
        public async Task<List<Report>> Reports() => await HasMany<Report>("content_id");
        public async Task<List<Tag>> Tags() => await BelongsToMany<Tag>("post_tags");
    }

    public class PostAttachment
    {
        /// <summary>
        /// URL publique du fichier
        /// </summary>
        [BsonElement("url")]
        public required string Url { get; set; }

        /// <summary>
        /// Type de fichier (image/video/file/etc.)
        /// </summary>
        [BsonElement("type")]
        public required string Type { get; set; }

        /// <summary>
        /// Nom original du fichier
        /// </summary>
        [BsonElement("name")]
        public required string Name { get; set; }

        /// <summary>
        /// Taille en octets
        /// </summary>
        [BsonElement("size")]
        public long Size { get; set; }
    }
}
