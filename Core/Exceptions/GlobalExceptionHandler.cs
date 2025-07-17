using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace BackEnd.Core.Exceptions;

/// <summary>
/// Middleware pour la gestion centralisée des exceptions
/// </summary>
public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;

    /// <summary>
    /// Gère les exceptions et produit une réponse JSON standardisée
    /// </summary>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        // Log technique pour les développeurs
        _logger.LogError(exception, "Exception: {Message}", exception.Message);

        var response = httpContext.Response;
        response.ContentType = "application/json";

        // Gestion des exceptions personnalisées
        if (exception is BaseException baseException)
        {
            response.StatusCode = (int)baseException.StatusCode;
            await response.WriteAsJsonAsync(new
            {
                // Message clair pour l'utilisateur
                baseException.Message,
                
                // Détails supplémentaires (optionnels)
                Details = baseException.Data,
                
                // Code de statut HTTP
                response.StatusCode
            }, cancellationToken);
        }
        // Gestion des autres exceptions
        else
        {
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await response.WriteAsJsonAsync(new
            {
                // Message générique pour l'utilisateur
                Message = "Une erreur inattendue s'est produite.",
                
                // Pas de détails techniques exposés
                response.StatusCode
            }, cancellationToken);
        }

        return true;
    }
}