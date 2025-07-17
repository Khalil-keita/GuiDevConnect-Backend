using System.Net;

namespace BackEnd.Core.Exceptions;

/// <summary>
/// Classe de base pour toutes les exceptions personnalisées
/// </summary>
/// <remarks>
/// Fournit une structure commune avec :
/// - Un message clair pour l'utilisateur
/// - Un code HTTP approprié
/// - Des données supplémentaires optionnelles
/// </remarks>
public abstract class BaseException : Exception
{
    /// <summary>
    /// Code HTTP associé à l'erreur
    /// </summary>
    public HttpStatusCode StatusCode { get; }
    
    /// <summary>
    /// Données supplémentaires sur l'erreur
    /// </summary>
    public new object? Data { get; }

    /// <summary>
    /// Constructeur de la base
    /// </summary>
    /// <param name="message">Message clair pour l'utilisateur</param>
    /// <param name="statusCode">Code HTTP (500 par défaut)</param>
    /// <param name="data">Données supplémentaires</param>
    protected BaseException(
        string message, 
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
        object? data = null) 
        : base(message)
    {
        StatusCode = statusCode;
        Data = data;
    }
}