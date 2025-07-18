using backEnd.Core.Model;
using backEnd.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backEnd.Src.Models
{
    /// <summary>
    /// Préférences de l'utilisateur (sous-document)
    /// </summary>
    public class UserPreference : AbstractModel<UserPreference>
    {
        public UserPreference() { }

        public UserPreference(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Utilisateur associé
        /// </summary>
        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

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
