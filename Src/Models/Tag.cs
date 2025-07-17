using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    /// <summary>
    /// Représente un tag indépendant avec statistiques et métadonnées
    /// </summary>
    public class Tag(IMongoDbContext dbContext) : AbstractModel<Tag>(dbContext)
    {
        /// <summary>
        /// Nom du tag (en minuscules, sans espaces)
        /// </summary>
        [BsonElement("name")]
        public required string Name { get; set; }

        /// <summary>
        /// Version affichable du tag (avec majuscules et caractères spéciaux)
        /// </summary>
        [BsonElement("display_name")]
        public required string DisplayName { get; set; }

        /// <summary>
        /// Slug pour les URLs
        /// </summary>
        [BsonElement("slug")]
        public required string Slug { get; set; }

        /// <summary>
        /// Description du tag
        /// </summary>
        [BsonElement("description")]
        public required string Description { get; set; }

        /// <summary>
        /// Nombre total de publications utilisant ce tag
        /// </summary>
        [BsonElement("usage_count")]
        public int UsageCount { get; set; } = 0;

        /// <summary>
        /// Tag officiel (créé par un modérateur/admin)
        /// </summary>
        [BsonElement("is_official")]
        public bool IsOfficial { get; set; } = false;

        /// <summary>
        /// Tag vérifié (contenu approprié)
        /// </summary>
        [BsonElement("is_verified")]
        public bool IsVerified { get; set; } = false;

        /// <summary>
        /// Couleur d'affichage optionnelle
        /// </summary>
        [BsonElement("color")]
        public string Color { get; set; } = "#6c757d";

        /// <summary>
        /// Icône associée (nom de classe FontAwesome ou URL)
        /// </summary>
        [BsonElement("icon")]
        public required string Icon { get; set; }

        /// <summary>
        /// Relations avec d'autres tags
        /// </summary>
        [BsonElement("related_tags")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> RelatedTags { get; set; } = [];

        /// <summary>
        /// Métadonnées SEO
        /// </summary>
        [BsonElement("meta")]
        public TagMeta? Meta { get; set; }
    }

    /// <summary>
    /// Métadonnées SEO pour les tags
    /// </summary>
    public class TagMeta
    {
        /// <summary>
        /// Titre pour les balises meta
        /// </summary>
        [BsonElement("title")]
        public required string Title { get; set; }

        /// <summary>
        /// Description pour les balises meta
        /// </summary>
        [BsonElement("description")]
        public required string Description { get; set; }

        /// <summary>
        /// Mots-clés pour le SEO
        /// </summary>
        [BsonElement("keywords")]
        public List<string> Keywords { get; set; } = [];
    }
}