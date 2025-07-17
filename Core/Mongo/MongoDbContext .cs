using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using System.Diagnostics;
using System.Threading.Tasks;

namespace backEnd.Core.Mongo
{
    /// <summary>
    /// Implémentation concrète du contexte de base de données MongoDB avec gestion améliorée.
    /// Fournit une abstraction pour interagir avec MongoDB en incluant :
    /// - Gestion des connexions
    /// - Configuration avancée du client
    /// - Gestion des sessions transactionnelles
    /// - Logging détaillé
    /// - Optimisation des performances
    /// </summary>
    public class MongoDbContext : IMongoDbContext, IDisposable
    {
        // Client MongoDB pour les opérations de base
        private readonly IMongoClient _client;

        // Référence à la base de données MongoDB
        private readonly IMongoDatabase _database;

        // Logger pour le suivi des activités et erreurs
        private readonly ILogger<MongoDbContext> _logger;

        // Session active pour les transactions (nullable car optionnelle)
        private IClientSessionHandle? _session;

        /// <summary>
        /// Constructeur initialisant la connexion à MongoDB avec gestion robuste des erreurs
        /// </summary>
        /// <param name="connectionString">Chaîne de connexion au format MongoDB</param>
        /// <param name="databaseName">Nom de la base de données cible</param>
        /// <param name="logger">Instance de logger injectée pour le suivi</param>
        /// <exception cref="InvalidOperationException">Si la connexion ou la vérification échoue</exception>
        public MongoDbContext(string connectionString, string databaseName, ILogger<MongoDbContext> logger)
        {
            _logger = logger;

            // Chronométrage de l'initialisation pour le suivi des performances
            var stopwatch = Stopwatch.StartNew();

            try
            {
                // Configuration avancée à partir de la chaîne de connexion
                var settings = MongoClientSettings.FromConnectionString(connectionString);

                // Application des paramètres optimisés
                ConfigureClientSettings(settings);

                // Création du client avec les paramètres configurés
                _client = new MongoClient(settings);

                // Obtention de la référence à la base de données
                _database = _client.GetDatabase(databaseName);

                // Vérification active de la connexion
                VerifyConnection();

                _logger.LogInformation("MongoDB connection established in {ElapsedMs}ms", stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Failed to initialize MongoDB connection");
                throw; // Remontée de l'exception après logging
            }
        }

        /// <summary>
        /// Configure les paramètres avancés du client MongoDB pour :
        /// - La cohérence des données
        /// - La résilience aux pannes
        /// - Les optimisations de performance
        /// - Le logging des opérations
        /// </summary>
        /// <param name="settings">Instance des paramètres à configurer</param>
        private void ConfigureClientSettings(MongoClientSettings settings)
        {
            // Configuration de la cohérence de lecture (majority pour une forte cohérence)
            settings.ReadConcern = ReadConcern.Majority;

            // Configuration de la confirmation d'écriture (majority pour la durabilité)
            settings.WriteConcern = WriteConcern.WMajority;

            // Activation des réessais automatiques pour les opérations
            settings.RetryReads = true;
            settings.RetryWrites = true;

            // Paramètres de timeout optimisés pour la production
            settings.ConnectTimeout = TimeSpan.FromSeconds(15);
            settings.SocketTimeout = TimeSpan.FromSeconds(30);
            settings.ServerSelectionTimeout = TimeSpan.FromSeconds(30);

            // Configuration du pool de connexions
            settings.MaxConnectionPoolSize = 100;   // Maximum de connexions simultanées
            settings.MinConnectionPoolSize = 10;    // Minimum maintenu en permanence

            // Configuration du logging des commandes MongoDB (optionnel en debug)
            settings.ClusterConfigurator = cb =>
            {
                cb.Subscribe<CommandStartedEvent>(e =>
                    _logger.LogDebug("MongoDB Command: {CommandName} - {CommandJson}",
                        e.CommandName, e.Command.ToJson()));
            };
        }

        /// <summary>
        /// Vérifie activement que la connexion à MongoDB est fonctionnelle
        /// en exécutant une commande ping simple mais efficace
        /// </summary>
        /// <exception cref="InvalidOperationException">Si la commande ping échoue</exception>
        private void VerifyConnection()
        {
            try
            {
                // Exécution synchrone d'une commande ping de test
                _database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("MongoDB connection test failed", ex);
            }
        }

        /// <summary>
        /// Accès à la base de données MongoDB configurée
        /// </summary>
        public IMongoDatabase Database => _database;

        /// <summary>
        /// Obtient une collection MongoDB pour le type spécifié
        /// </summary>
        /// <typeparam name="T">Type d'entité associé à la collection</typeparam>
        /// <param name="name">Nom optionnel de la collection (sinon déduit du type)</param>
        /// <returns>Interface de la collection MongoDB</returns>
        /// <exception cref="MongoException">En cas d'échec d'accès à la collection</exception>
        public IMongoCollection<T> GetCollection<T>(string? name = null)
        {
            try
            {
                // Utilisation du nom fourni ou déduction à partir du type
                var collectionName = name ?? GetCollectionName<T>();

                _logger.LogDebug("Accessing collection {CollectionName} for type {Type}",
                    collectionName, typeof(T).Name);

                return _database.GetCollection<T>(collectionName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to access MongoDB collection");
                throw;
            }
        }

        /// <summary>
        /// Démarre une nouvelle session MongoDB pour les transactions ACID
        /// (Dispose automatiquement toute session existante)
        /// </summary>
        /// <returns>Handle de la nouvelle session</returns>
        /// <exception cref="MongoException">Si le démarrage de session échoue</exception>
        public async Task<IClientSessionHandle> StartSessionAsync()
        {
            try
            {
                // Nettoyage de la session précédente si existante
                _session?.Dispose();

                // Démarrage d'une nouvelle session
                _session = await _client.StartSessionAsync();

                _logger.LogDebug("Started new MongoDB session with options: {Options}", _session.Options.ToJson());

                return _session;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to start MongoDB session");
                throw;
            }
        }

        /// <summary>
        /// Initialise tous les index nécessaires pour les collections
        /// </summary>
        public void InitializeIndexes()
        {
            try
            {
                MongoIndexInitializer.InitializeAllIndexes(_database);
                _logger.LogInformation("MongoDB indexes initialized successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize MongoDB indexes");
                throw;
            }
        }

        
        /// <summary>
        /// Génère un nom de collection à partir du type d'entité
        /// en convertissant le nom de classe PascalCase en snake_case
        /// </summary>
        /// <typeparam name="T">Type d'entité</typeparam>
        /// <returns>Nom de la collection en snake_case</returns>
        public string GetCollectionName<T>()
        {
            var type = typeof(T);
            var className = type.Name;

            // Conversion PascalCase en snake_case:
            // Insère un underscore avant les majuscules (sauf la première lettre)
            // et convertit tout en minuscules
            return string.Concat(className.Select((x, i) =>
                i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower() + 's';
        }

        /// <summary>
        /// Implémentation de IDisposable pour le nettoyage des ressources
        /// - Libère la session active
        /// - Supprime l'objet de la finalisation
        /// - Loggue l'opération
        /// </summary>
        public void Dispose()
        {
            try
            {
                // Libération de la session si existante
                _session?.Dispose();
                _logger.LogInformation("MongoDB context disposed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error disposing MongoDB context");
            }

            // Empêche le garbage collector de rappeler le finalizer
            GC.SuppressFinalize(this);
        }
    }
}