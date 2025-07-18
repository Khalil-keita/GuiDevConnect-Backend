using backEnd.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using static MongoDB.Driver.WriteConcern;

namespace backEnd.Core.Model
{
    /// <summary>
    /// Classe de base abstraite pour tous les modèles MongoDB
    /// </summary>
    /// <typeparam name="T">Type du modèle concret</typeparam>
    /// <remarks>
    /// Constructeur protégé nécessitant le contexte
    /// </remarks>
    /// <remarks>
    /// Implémente les notifications de changement de propriétés et un accès dynamique style Laravel
    /// </remarks>
    public abstract class AbstractModel<T> where T : AbstractModel<T>
    {
        /// <summary>
        /// Identifiant unique MongoDB (ObjectId)
        /// </summary>
        [BsonId]
        [BsonElement("id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        /// <summary>
        /// Date de creation
        /// </summary>
        [BsonElement("created_at")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Date de modifications
        /// </summary>
        [BsonElement("updated_at")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// ID de l'utilisateur ayant créé le tag
        /// </summary>
        [BsonElement("created_by")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CreatedBy { get; set; }

        #region Accès à la base de données

        /// <summary>
        /// Contexte de base de données injecté
        /// </summary>
        [BsonIgnore]
        protected IMongoDbContext? _dbContext;


        /// <summary>
        /// Collection MongoDB pour ce modèle
        /// </summary>
        protected IMongoCollection<T> Collection => _dbContext?.GetCollection<T>()!;

        protected AbstractModel()
        {
            // Serà null et devra être défini plus tard
        }

        protected AbstractModel(IMongoDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        #endregion

        #region CRUD de Base

        /// <summary>
        /// Récupère toutes les entités de la collection
        /// </summary>
        public async Task<List<T>> AllAsync()
        {
            return await Collection.Find(_ => true).ToListAsync();
        }

        /// <summary>
        /// Récupère une entité par son ID
        /// </summary>
        public async Task<T?> GetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;

            return await Collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Récupère la première entité de la collection
        /// </summary>
        public async Task<T> FirstAsync(Expression<Func<T, object>>? sortField = null)
        {
            var sort = sortField != null
                ? Builders<T>.Sort.Ascending(sortField)
                : Builders<T>.Sort.Ascending(x => x.Id); // Tri par ID par défaut

            return await Collection.Find(_ => true)
                                 .Sort(sort) 
                                 .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Récupère la dernière entité de la collection
        /// </summary>
        public async Task<T> LastAsync(Expression<Func<T, object>>? sortField = null)
        {
            var sort = sortField != null
                ? Builders<T>.Sort.Descending(sortField)
                : Builders<T>.Sort.Descending(x => x.Id); // Tri par ID par défaut

            return await Collection.Find(_ => true)
                                 .Sort(sort) 
                                 .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Sauvegarde l'entité (insert ou update)
        /// </summary>
        /// <param name="session">Session pour transaction (optionnel)</param>
        /// <returns>Task</returns>
        public virtual async Task SaveAsync(IClientSessionHandle? session = null)
        {
            UpdatedAt = DateTime.UtcNow;

            if (string.IsNullOrEmpty(Id))
            {
                CreatedAt = DateTime.UtcNow;

                if (session != null)
                    await Collection.InsertOneAsync(session, (T)this);
                else
                    await Collection.InsertOneAsync((T)this);
            }
            else
            {
                var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(Id));
                if (session != null)
                    await Collection.ReplaceOneAsync(session, filter, (T)this);
                else
                    await Collection.ReplaceOneAsync(filter, (T)this);
            }
        }

        /// <summary>
        /// Supprime l'entité de la base de données
        /// </summary>
        /// <param name="session">Session pour transaction (optionnel)</param>
        /// <returns>Task</returns>
        public virtual async Task DeleteAsync(IClientSessionHandle? session = null)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(Id));
                if (session != null)
                    await Collection.DeleteOneAsync(session, filter);
                else
                    await Collection.DeleteOneAsync(filter);
            }
        }

        #endregion

        #region Relations

        /// <summary>
        /// Relation "Possède Un" (One-to-One)
        /// </summary>
        /// <typeparam name="TRelated">Type du modèle lié</typeparam>
        /// <param name="foreignKey">Clé étrangère (optionnel)</param>
        /// <returns>Entité liée ou null</returns>
        public async Task<TRelated> HasOne<TRelated>(string? foreignKey = null) 
            where TRelated : AbstractModel<TRelated>, new()
        {
            foreignKey ??= $"{typeof(T).Name.ToLower()}_id";
            var filter = Builders<TRelated>.Filter.Eq(foreignKey, Id);
            return await _dbContext.GetCollection<TRelated>()
                                 .Find(filter)
                                 .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Relation "Possède Plusieurs" (One-to-Many)
        /// </summary>
        /// <typeparam name="TRelated">Type du modèle lié</typeparam>
        /// <param name="foreignKey">Clé étrangère (optionnel)</param>
        /// <returns>Liste des entités liées</returns>
        public async Task<List<TRelated>> HasMany<TRelated>(string? foreignKey = null) 
            where TRelated : AbstractModel<TRelated>, new()
        {
            foreignKey ??= $"{typeof(T).Name.ToLower()}_id";
            var filter = Builders<TRelated>.Filter.Eq(foreignKey, Id);
            return await _dbContext.GetCollection<TRelated>()
                                 .Find(filter)
                                 .ToListAsync();
        }

        /// <summary>
        /// Relation "Appartient à" (Inverse de HasOne/HasMany)
        /// </summary>
        /// <typeparam name="TModel">Type du modèle parent</typeparam>
        /// <param name="foreignKey">Clé étrangère (optionnel)</param>
        /// <returns>Entité parente ou null</returns>
        public async Task<TModel?> BelongsTo<TModel>(string? foreignKey = null) 
            where TModel : AbstractModel<TModel>, new()
        {
            foreignKey ??= $"{typeof(TModel).Name.ToLower()}_id";
            var propertyInfo = typeof(T).GetProperty(foreignKey);
            if (propertyInfo == null) return null;
            
            var foreignId = propertyInfo.GetValue(this)?.ToString();
            if (string.IsNullOrEmpty(foreignId)) return null;

            return await _dbContext.GetCollection<TModel>()
                                .Find(Builders<TModel>.Filter.Eq("_id", ObjectId.Parse(foreignId)))
                                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Relation "Appartient à Plusieurs" (Many-to-Many via table pivot)
        /// </summary>
        /// <typeparam name="TRelated">Type du modèle lié</typeparam>
        /// <param name="pivotCollection">Nom de la collection pivot (optionnel)</param>
        /// <param name="foreignKey">Clé étrangère vers ce modèle (optionnel)</param>
        /// <param name="relatedKey">Clé étrangère vers le modèle lié (optionnel)</param>
        /// <returns>Liste des entités liées</returns>
        public async Task<List<TRelated>> BelongsToMany<TRelated>(
            string? pivotCollection = null, 
            string? foreignKey = null, 
            string? relatedKey = null) 
            where TRelated : AbstractModel<TRelated>, new()
        {
            pivotCollection ??= $"{typeof(T).Name.ToLower()}_{typeof(TRelated).Name.ToLower()}";
            foreignKey ??= $"{typeof(T).Name.ToLower()}_id";
            relatedKey ??= $"{typeof(TRelated).Name.ToLower()}_id";

            // Étape 1: Récupérer les IDs liés dans la table pivot
            var pivotFilter = Builders<BsonDocument>.Filter.Eq(foreignKey, Id);
            var pivotResults = await _dbContext.Database
                                             .GetCollection<BsonDocument>(pivotCollection)
                                             .Find(pivotFilter)
                                             .ToListAsync();

            var relatedIds = pivotResults.Select(p => p[relatedKey].ToString()).ToList();

            // Étape 2: Récupérer les documents liés
            if (relatedIds.Count == 0) return [];

            var relatedFilter = Builders<TRelated>.Filter.In("_id", 
                relatedIds.Select(ObjectId.Parse));
            return await _dbContext.GetCollection<TRelated>()
                                 .Find(relatedFilter)
                                 .ToListAsync();
        }

        #endregion

        #region Méthodes Statiques

        /// <summary>
        /// Crée une nouvelle entité et la sauvegarde
        /// </summary>
        /// <param name="dbContext">Contexte de base de données</param>
        /// <param name="values">Dictionnaire des valeurs à attribuer</param>
        /// <returns>Entité créée</returns>
        public static async Task<T> CreateAsync(IMongoDbContext dbContext, Dictionary<string, object> values)
        {
            var instance = (T) Activator.CreateInstance(typeof(T), dbContext)!;
            foreach (var prop in values)
            {
                var propertyInfo = typeof(T).GetProperty(prop.Key, 
                    BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo != null && propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(instance, Convert.ChangeType(prop.Value, propertyInfo.PropertyType), null);
                }
            }
            await instance!.SaveAsync();
            return instance;
        }



        /// <summary>
        /// Initialise un nouveau constructeur de requête
        /// </summary>
        /// <param name="dbContext">Contexte de base de données</param>
        /// <returns>Constructeur de requête</returns>
        public static ModelQueryBuilder<T> Query(IMongoDbContext dbContext)
        {
            return new ModelQueryBuilder<T>(dbContext.GetCollection<T>());
        }

        #endregion

        public AbstractModel<T> SetDbContext(IMongoDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            return (T)this;
        }
    }
}