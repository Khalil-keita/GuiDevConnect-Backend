using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    /// <summary>
    /// Système de classement des utilisateurs basé sur leur activité
    /// </summary>
    /// <remarks>
    /// Définit les niveaux/rangs que les utilisateurs peuvent atteindre
    /// selon leur nombre de points (ex: Nouveau, Actif, Vétéran)
    /// </remarks>
    public class Ranking : AbstractModel<Ranking>
    {
        /// <summary>
        /// ID de l'utilisateur 
        /// </summary>
        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }

        /// <summary>
        /// Nom du rang (ex: "Membre Bronze")
        /// </summary>
        [BsonElement("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Points minimum requis pour ce rang
        /// </summary>
        [BsonElement("min_points")]
        public int MinPoints { get; set; }

        /// <summary>
        /// Icône associée au rang (emoji ou URL)
        /// </summary>
        [BsonElement("icon")]
        public string? Icon { get; set; }

        /// <summary>
        /// Couleur associée au rang (code hexadécimal)
        /// </summary>
        [BsonElement("color")]
        public string Color { get; set; } = "#808080";

        /// <summary>
        /// Avantages spécifiques à ce rang
        /// </summary>
        [BsonElement("perks")]
        public List<string> Perks { get; set; } = [];

        public Ranking() { }

        public Ranking(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> User() => await BelongsTo<User>("user_id");

    }
}
