using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    /// <summary>
    /// Préférences de l'utilisateur (sous-document)
    /// </summary>
    public class UserPreferences(IMongoDbContext dbContext) : AbstractModel<UserPreferences>(dbContext)
    {
        /// <summary>
        /// Thème préféré (light/dark)
        /// </summary>
        [BsonElement("theme")]
        public string Theme { get; set; } = "light";

        /// <summary>
        /// Langue préférée
        /// </summary>
        [BsonElement("language")]
        public string Language { get; set; } = "fr";

        /// <summary>
        /// Recevoir des notifications par email
        /// </summary>
        [BsonElement("email_notifications")]
        public bool EmailNotifications { get; set; } = true;

        /// <summary>
        /// Recevoir des newsletters
        /// </summary>
        [BsonElement("newsletter")]
        public bool Newsletter { get; set; } = true;

        /// <summary>
        /// Afficher l'email publiquement
        /// </summary>
        [BsonElement("show_email")]
        public bool ShowEmail { get; set; } = false;
    }
}
