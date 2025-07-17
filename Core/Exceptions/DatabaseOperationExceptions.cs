using System.Net;
namespace BackEnd.Core.Exceptions;

/// <summary>
/// Exception générique pour les opérations en base de données
/// </summary>
public class DatabaseOperationException : BaseException
{
    /// <summary>
    /// Constructeur protégé pour les classes dérivées
    /// </summary>
    protected DatabaseOperationException(string operation, string entityName)
        : base(
            "Une erreur est survenue lors du traitement de votre demande.",
            HttpStatusCode.InternalServerError,
            new { 
                Details = $"Erreur {operation} pour l'entité {entityName}" 
            })
    {
    }
}

/// <summary>
/// Exception lors de la création d'une entité
/// </summary>
public class CreateEntityException : DatabaseOperationException
{
    public CreateEntityException(string entityName)
        : base("lors de la création", entityName)
    {
    }
}

/// <summary>
/// Exception lors de la mise à jour d'une entité
/// </summary>
public class UpdateEntityException : DatabaseOperationException
{
    public UpdateEntityException(string entityName)
        : base("lors de la mise à jour", entityName)
    {
    }
}

/// <summary>
/// Exception lors de la suppression d'une entité
/// </summary>
public class DeleteEntityException : DatabaseOperationException
{
    public DeleteEntityException(string entityName)
        : base("lors de la suppression", entityName)
    {
    }
}