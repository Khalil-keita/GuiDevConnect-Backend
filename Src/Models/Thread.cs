using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    /// <summary>
    /// Représente un sujet de discussion contenant plusieurs posts
    /// </summary>
    public class Thread : AbstractModel<Thread>
    {
        /// <summary>
        /// Titre du sujet (visible dans la liste des threads)
        /// </summary>
        [BsonElement("title")]
        public string?  Title { get; set; }

        /// <summary>
        /// ID du post initial qui a lancé la discussion
        /// </summary>
        [BsonElement("initial_post_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? InitialPostId { get; set; }

        /// <summary>
        /// ID de la catégorie parente
        /// </summary>
        [BsonElement("category_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CategoryId { get; set; }

        /// <summary>
        /// Nombre total de vues
        /// </summary>
        [BsonElement("view_count")]
        public int ViewCount { get; set; } = 0;

        /// <summary>
        /// Nombre total de posts dans le thread
        /// </summary>
        [BsonElement("post_count")]
        public int PostCount { get; set; } = 1;

        /// <summary>
        /// Date du dernier post dans le thread
        /// </summary>
        [BsonElement("last_post_date")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? LastPostDate { get; set; }

        /// <summary>
        /// ID du dernier post ajouté
        /// </summary>
        [BsonElement("last_post_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? LastPostId { get; set; }

        /// <summary>
        /// Createur du thread
        /// </summary>
        [BsonElement("creator_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> CreatorId { get; set; } = [];

        /// <summary>
        /// Statut de verrouillage (empêche nouveaux posts)
        /// </summary>
        [BsonElement("is_locked")]
        public bool IsLocked { get; set; } = false;

        /// <summary>
        /// Statut épinglé (reste en haut de la liste)
        /// </summary>
        [BsonElement("is_pinned")]
        public bool IsPinned { get; set; } = false;

        public Thread() { }

        public Thread(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Creator() => await BelongsTo<User>("creator_id");
        public async Task<Category> Category() => await BelongsTo<Category>("category_id");
        public async Task<List<Post>> Posts() => await HasMany<Post>("thread_id");
        public async Task<List<Tag>> Tags() => await BelongsToMany<Tag>("thread_tags");
        public async Task<Poll> Poll() => await HasOne<Poll>("thread_id");
        public async Task<List<Report>> Reports() => await HasMany<Report>("content_id");

    }
}
