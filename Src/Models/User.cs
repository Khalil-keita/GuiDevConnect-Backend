using backEnd.Core.Model;
using backEnd.Core.Mongo;
using backEnd.Utils;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace backEnd.Src.Models
{
    /// <summary>
    /// Représente un utilisateur du forum avec toutes les propriétés nécessaires
    /// </summary>
    public class User(IMongoDbContext dbContext) : AbstractModel<User>(dbContext)
    {
       /// <summary>
       /// Nom d'utilisateur unique pour la connexion
       /// </summary>
       [BsonElement("username")]
       public required string Username { get; set; }

       /// <summary>
       /// Email de l'utilisateur (unique)
       /// </summary>
       [BsonElement("email")]
       public required string Email { get; set; }

       /// <summary>
       /// Mot de passe hashé
       /// </summary>
       [BsonElement("password_hash")]
       public required string PasswordHash { get; set; }

       /// <summary>
       /// Prénom de l'utilisateur
       /// </summary>
       [BsonElement("first_name")]
       public required string FirstName { get; set; }

       /// <summary>
       /// Nom de famille de l'utilisateur
       /// </summary>
       [BsonElement("last_name")]
       public required string LastName { get; set; }

       /// <summary>
       /// Date de dernière connexion
       /// </summary>
       [BsonElement("last_login")]
       [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
       public DateTime? LastLogin { get; set; }

       /// <summary>
       /// URL de l'avatar de l'utilisateur
       /// </summary>
       [BsonElement("avatar_url")]
       public string? AvatarUrl { get; set; }

       /// <summary>
       /// Bio/description de l'utilisateur
       /// </summary>
       [BsonElement("bio")]
       public string? Bio { get; set; }

       /// <summary>
       /// Rôle de l'utilisateur (user, admin, moderateur...)
       /// </summary>
       [BsonElement("role")]
       public string Role { get; set; } = RoleHelper.USER;

       /// <summary>
       /// Coordonnées GPS
       /// </summary>
       [BsonElement("coordinates")]
       public GeoJson2DCoordinates? Coordinates { get; set; }

       /// <summary>
       /// Statut de vérification de l'email
       /// </summary>
       [BsonElement("email_verified")]
       public bool EmailVerified { get; set; } = false;

       /// <summary>
       /// Token de vérification d'email
       /// </summary>
       [BsonElement("email_verification_token")]
       public string? EmailVerificationToken { get; set; }

       /// <summary>
       /// Date d'expiration du token de vérification
       /// </summary>
       [BsonElement("email_verification_expires")]
       [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
       public DateTime? EmailVerificationExpires { get; set; }

       /// <summary>
       /// Token de réinitialisation de mot de passe
       /// </summary>
       [BsonElement("password_reset_token")]
       public string? PasswordResetToken { get; set; }

       /// <summary>
       /// Date d'expiration du token de réinitialisation
       /// </summary>
       [BsonElement("password_reset_expires")]
       [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
       public DateTime? PasswordResetExpires { get; set; }

       /// <summary>
       /// Statut de bannissement
       /// </summary>
       [BsonElement("is_banned")]
       public bool IsBanned { get; set; } = false;

       /// <summary>
       /// Raison du bannissement
       /// </summary>
       [BsonElement("ban_reason")]
       public string? BanReason { get; set; }

       /// <summary>
       /// Préférences de l'utilisateur
       /// </summary>
       [BsonElement("preferences_id")]
       [BsonRepresentation(BsonType.ObjectId)]
       public string? PreferencesId { get; set; }

       /// <summary>
       /// Statistiques de l'utilisateur
       /// </summary>
       [BsonElement("statistics_id")]
       [BsonRepresentation(BsonType.ObjectId)]
       public string? StatisticsId { get; set; }

        //public async Task<List<Comment>> Comments() => await HasMany<Comment>("autor_id");
        //public async Task<List<Post>> Posts() => await HasMany<Post>("author_id");
        //public async Task<UserPreferences> Preferences() => await BelongsTo<UserPreferences>();
        //public async Task<UserStatistics> Statistics() => await BelongsTo<UserStatistics>();
        //public async Task<UserStatistics> Bagdes() => await BelongsToMany<Badge>();
    }
}
