using System.Net;

namespace BackEnd.Core.Exceptions;

/// <summary>
/// Exception générique pour les uploads de fichiers
/// </summary>
public class FileUploadException : BaseException
{
    public FileUploadException(string userMessage)
        : base(
            userMessage,
            HttpStatusCode.BadRequest)
    {
    }
}

/// <summary>
/// Exception pour les types de fichiers non autorisés
/// </summary>
public class InvalidFileTypeException : FileUploadException
{
    public InvalidFileTypeException()
        : base("Le type de fichier n'est pas autorisé.")
    {
    }
}

/// <summary>
/// Exception pour les fichiers trop volumineux
/// </summary>
public class FileSizeExceededException : FileUploadException
{
    public FileSizeExceededException(int maxSizeMB)
        : base($"Le fichier est trop volumineux (maximum {maxSizeMB} Mo).")
    {
    }
}

/// <summary>
/// Exception pour les fichiers non trouvés
/// </summary>
public class FileNotFoundException : BaseException
{
    public FileNotFoundException()
        : base(
            "Le fichier demandé n'a pas été trouvé.",
            HttpStatusCode.NotFound)
    {
    }
}