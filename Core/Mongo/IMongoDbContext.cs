using MongoDB.Driver;

namespace backEnd.Core.Mongo;
    /// <summary>
    /// Interface définissant le contrat pour le contexte de base de données MongoDB
    /// </summary>
    public interface IMongoDbContext
    {
        /// <summary>
        /// Instance de la base de données MongoDB
        /// </summary>
        IMongoDatabase Database { get; }

        /// <summary>
        /// Obtient une collection MongoDB pour un type donné
        /// </summary>
        /// <typeparam name="T">Type de l'entité</typeparam>
        /// <param name="name">Nom personnalisé de la collection (optionnel)</param>
        /// <returns>Collection MongoDB</returns>
        IMongoCollection<T> GetCollection<T>(string? name = null);

        /// <summary>
        /// Démarre une nouvelle session pour les transactions
        /// </summary>
        /// <returns>Gestionnaire de session client</returns>
        Task<IClientSessionHandle> StartSessionAsync();

        /// <summary>
        /// Obtient le nom de collection par défaut pour un type
        /// </summary>
        /// <typeparam name="T">Type de l'entité</typeparam>
        /// <returns>Nom de la collection en snake_case</returns>
        string GetCollectionName<T>();

        /// <summary>
        /// Initialise tous les index nécessaires pour les collections
        /// </summary>
        void InitializeIndexes();
    }
