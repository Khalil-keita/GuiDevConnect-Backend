using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace backEnd.Core.Model;
/// <summary>
/// Constructeur de requêtes fluent pour les modèles MongoDB
/// </summary>
/// <typeparam name="TModel">Type du modèle</typeparam>
/// <remarks>
/// Initialise une nouvelle instance du constructeur de requêtes
/// </remarks>
/// <param name="collection">Collection MongoDB</param>
public class ModelQueryBuilder<TModel>(IMongoCollection<TModel> collection) where TModel : AbstractModel<TModel>
    {
        private readonly IMongoCollection<TModel> _collection = collection ?? throw new ArgumentNullException(nameof(collection));
        private FilterDefinition<TModel> _filter = Builders<TModel>.Filter.Empty;
        private SortDefinition<TModel>? _sort = null;
        private int? _limit = null;
        private int? _skip = null;
        private ProjectionDefinition<TModel, object>? _projection = null;

        /// <summary>
        /// Ajoute une condition WHERE à la requête
        /// </summary>
        /// <param name="expression">Expression de filtrage</param>
        /// <returns>Instance courante du constructeur</returns>
        public ModelQueryBuilder<TModel> Where(Expression<Func<TModel, bool>> expression)
        {
            _filter &= Builders<TModel>.Filter.Where(expression);
            return this;
        }

        /// <summary>
        /// Ajoute un tri ASCENDANT à la requête
        /// </summary>
        /// <param name="field">Champ à trier</param>
        /// <returns>Instance courante du constructeur</returns>
        public ModelQueryBuilder<TModel> OrderBy(Expression<Func<TModel, object>> field)
        {
            _sort = _sort == null 
                ? Builders<TModel>.Sort.Ascending(field) 
                : _sort.Ascending(field);
            return this;
        }

        /// <summary>
        /// Ajoute un tri DESCENDANT à la requête
        /// </summary>
        /// <param name="field">Champ à trier</param>
        /// <returns>Instance courante du constructeur</returns>
        public ModelQueryBuilder<TModel> OrderByDescending(Expression<Func<TModel, object>> field)
        {
            _sort = _sort == null 
                ? Builders<TModel>.Sort.Descending(field) 
                : _sort.Descending(field);
            return this;
        }

        /// <summary>
        /// Limite le nombre de résultats
        /// </summary>
        /// <param name="limit">Nombre maximum de résultats</param>
        /// <returns>Instance courante du constructeur</returns>
        public ModelQueryBuilder<TModel> Take(int limit)
        {
            _limit = limit;
            return this;
        }

        /// <summary>
        /// Ignore un nombre de résultats
        /// </summary>
        /// <param name="skip">Nombre de résultats à ignorer</param>
        /// <returns>Instance courante du constructeur</returns>
        public ModelQueryBuilder<TModel> Skip(int skip)
        {
            _skip = skip;
            return this;
        }

        /// <summary>
        /// Définit une projection pour la requête
        /// </summary>
        /// <param name="projection">Expression de projection</param>
        /// <returns>Instance courante du constructeur</returns>

        //public ModelQueryBuilder<TModel> Select<TProjection>(Expression<Func<TModel, TProjection>> projection)
        //{
        //    _projection = Builders<TModel>.Projection.Include(projection);
        //    return this;
        //}


        /// <summary>
        /// Exécute la requête et retourne les résultats
        /// </summary>
        /// <returns>Liste des entités correspondantes</returns>
        public async Task<List<TModel>> GetAsync()
        {
            var query = _collection.Find(_filter);

            if (_sort != null)
                query = query.Sort(_sort);

            //if (_projection != null)
            //    query = query.Project<TModel>(_projection); 

            if (_skip.HasValue)
                query = query.Skip(_skip);

            if (_limit.HasValue)
                query = query.Limit(_limit);

            return await query.ToListAsync();
        }


        /// <summary>
        /// Exécute la requête et retourne le premier résultat
        /// </summary>
        /// <returns>Première entité correspondante ou null</returns>
        public async Task<TModel> FirstAsync()
        {
            return await _collection.Find(_filter).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Compte le nombre de résultats correspondants
        /// </summary>
        /// <returns>Nombre d'entités correspondantes</returns>
        public async Task<long> CountAsync()
        {
            return await _collection.CountDocumentsAsync(_filter);
        }

        /// <summary>
        /// Vérifie si au moins un résultat existe
        /// </summary>
        /// <returns>True si au moins un résultat existe</returns>
        public async Task<bool> ExistsAsync()
        {
            return await _collection.Find(_filter).AnyAsync();
        }
}
