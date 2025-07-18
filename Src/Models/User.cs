using backEnd.Core.Model;
using backEnd.Core.Mongo;
using backEnd.Src.Dtos;
using backEnd.Utils;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace backEnd.Src.Models
{
    /// <summary>
    /// Représente un utilisateur du forum avec toutes les propriétés nécessaires
    /// </summary>
    public class User : AbstractModel<User>
    {
        /// <summary>
        /// Nom d'utilisateur unique pour la connexion
        /// </summary>
        [BsonElement("username")]
       public string? Username { get; set; }

       /// <summary>
       /// Email de l'utilisateur (unique)
       /// </summary>
       [BsonElement("email")]
       public string? Email { get; set; }

       /// <summary>
       /// Mot de passe hashé
       /// </summary>
       [BsonElement("password_hash")]
       public string? PasswordHash { get; set; }

       /// <summary>
       /// Prénom de l'utilisateur
       /// </summary>
       [BsonElement("first_name")]
       public string? FirstName { get; set; }

       /// <summary>
       /// Nom de famille de l'utilisateur
       /// </summary>
       [BsonElement("last_name")]
       public string? LastName { get; set; }

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
       public Coordinates? Coordinates { get; set; }

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

        public User() { }

        public User(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Post>> Posts() => await HasMany<Post>("author_id");
        public async Task<List<Comment>> Comments() => await HasMany<Comment>("author_id");
        public async Task<List<Thread>> Threads() => await HasMany<Thread>("creator_id");
        public async Task<List<Reaction>> Reactions() => await HasMany<Reaction>("user_id");
        public async Task<List<Message>> SentMessages() => await HasMany<Message>("sender_id");
        public async Task<List<Message>> ReceivedMessages() => await HasMany<Message>("receiver_id");
        public async Task<List<Report>> Reports() => await HasMany<Report>("reporter_id");
        public async Task<UserPreference> Preferences() => await HasOne<UserPreference>("user_id");
        public async Task<UserStatistic> Statistics() => await HasOne<UserStatistic>("user_id");
        public async Task<Ranking> Ranking() => await HasOne<Ranking>("user_id");
        public async Task<List<UserActivity>> Activities() => await HasMany<UserActivity>("user_id");
        public async Task<List<UserBookmark>> Bookmarks() => await HasMany<UserBookmark>("user_id");
        public async Task<List<Notification>> Notifications() => await HasMany<Notification>("user_id");
        public async Task<List<Badge>> Badges() => await BelongsToMany<Badge>("user_badges");
        public async Task<List<Ban>> Bans() => await HasMany<Ban>("user_id");
        public async Task<List<PollOption>> VotedPolls() => await BelongsToMany<PollOption>("voter_ids");
    }
}
