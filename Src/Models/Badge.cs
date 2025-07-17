using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    /// <summary>
    /// Système de badges/récompenses
    /// </summary>
    public class Badge(IMongoDbContext dbContext) : AbstractModel<Badge>(dbContext)
    {
        /// <summary>
        /// Nom du badge
        /// </summary>
        [BsonElement("name")]
        public required string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [BsonElement("description")]
        public required string Description { get; set; }

        /// <summary>
        /// Icône (URL ou référence)
        /// </summary>
        [BsonElement("icon")]
        public required string Icon { get; set; }

        /// <summary>
        /// Critère d'obtention
        /// </summary>
        [BsonElement("criteria")]
        public required string Criteria { get; set; }

        /// <summary>
        /// Rareté ('common', 'rare', 'epic', 'legendary')
        /// </summary>
        [BsonElement("rarity")]
        public string Rarity { get; set; } = "common";
    }

    
}
