using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    public class Category : AbstractModel<Category>
    {
        /// <summary>
        /// Nom affiché de la catégorie
        /// </summary>
        [BsonElement("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Description courte
        /// </summary>
        [BsonElement("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Slug URL-friendly
        /// </summary>
        [BsonElement("slug")]
        public string? Slug { get; set; }

        /// <summary>
        /// Ordre d'affichage dans les listes
        /// </summary>
        [BsonElement("order")]
        public int Order { get; set; } = 0;

        /// <summary>
        /// Nombre total de publications
        /// </summary>
        [BsonElement("post_count")]
        public int PostCount { get; set; } = 0;

        /// <summary>
        /// Catégorie active/inactive
        /// </summary>
        [BsonElement("is_active")]
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Couleur d'affichage (hexadecimal)
        /// </summary>
        [BsonElement("color_code")]
        public string ColorCode { get; set; } = "#3498db";

        public Category() { }

        public Category(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Thread>> Threads() => await HasMany<Thread>("category_id");
    }
}
