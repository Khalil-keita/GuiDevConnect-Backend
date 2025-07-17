using System.Net;

namespace BackEnd.Core.Exceptions;

/// <summary>
/// Exception pour les ressources non trouvées
/// </summary>
public class NotFoundException : BaseException
{
    /// <summary>
    /// Constructeur avec nom de la ressource et identifiant optionnel
    /// </summary>
    public NotFoundException(string resourceName, object? identifier = null)
        : base(
            // Message clair sans détails techniques
            $"Nous n'avons pas trouvé ce que vous cherchez.",
            HttpStatusCode.NotFound,
            new { 
                // Détails utiles pour le développement
                Details = $"Ressource '{resourceName}' non trouvée" + 
                         (identifier != null ? $" (ID: {identifier})" : "") 
            })
    {
    }
}

/// <summary>
/// Exception pour les requêtes incorrectes
/// </summary>
public class BadRequestException : BaseException
{
    /// <summary>
    /// Constructeur avec message simple
    /// </summary>
    public BadRequestException(string message = "Votre demande n'est pas valide.")
        : base(message, HttpStatusCode.BadRequest)
    {
    }
    
    /// <summary>
    /// Constructeur avec erreurs de validation
    /// </summary>
    public BadRequestException(object validationErrors)
        : base(
            "Certaines informations sont manquantes ou incorrectes.",
            HttpStatusCode.BadRequest,
            new { Errors = validationErrors })
    {
    }
}

/// <summary>
/// Exception pour les accès non autorisés
/// </summary>
public class UnauthorizedException : BaseException
{
    public UnauthorizedException()
        : base(
            "Vous devez vous connecter pour accéder à cette fonctionnalité.",
            HttpStatusCode.Unauthorized)
    {
    }
}

/// <summary>
/// Exception pour les permissions insuffisantes
/// </summary>
public class ForbiddenException : BaseException
{
    public ForbiddenException()
        : base(
            "Vous n'avez pas les droits nécessaires pour cette action.",
            HttpStatusCode.Forbidden)
    {
    }
}