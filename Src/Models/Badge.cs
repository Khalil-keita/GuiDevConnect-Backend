﻿using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    /// <summary>
    /// Système de badges/récompenses
    /// </summary>
    public class Badge : AbstractModel<Badge>
    {
        /// <summary>
        /// Nom du badge
        /// </summary>
        [BsonElement("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [BsonElement("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Icône (URL ou référence)
        /// </summary>
        [BsonElement("icon")]
        public string? Icon { get; set; }

        /// <summary>
        /// Critère d'obtention
        /// </summary>
        [BsonElement("criteria")]
        public string? Criteria { get; set; }

        /// <summary>
        /// Rareté ('common', 'rare', 'epic', 'legendary')
        /// </summary>
        [BsonElement("rarity")]
        public string? Rarity { get; set; } = "common";

        public Badge() { }

        public Badge(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> Users() => await BelongsToMany<User>("user_badges");
    }
}
