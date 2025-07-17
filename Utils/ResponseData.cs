using System.Net;

namespace backEnd.Utils
{
    public class ResponseData
    {
        public string? Message {  get; set; }

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        public object ? Data { get; set; } = null;

        public static ResponseData With(string message, HttpStatusCode statusCode, object? data = null)
        {
            return new ResponseData { Message = message, StatusCode = statusCode, Data = data };
        }

        public static ResponseData Success(string message)
        {
            return new ResponseData {Message = message};
        }

        public static ResponseData Success(object data)
        {
            return new ResponseData { Message = "Récuperation effectuée avec succès.", Data = data };
        }

        public static ResponseData Success(string message, object data)
        {
            return new ResponseData { Message = message, Data = data };
        }

        public static ResponseData Error(Exception ex)
        {
            return new ResponseData { Message = "Erreur interne du serveur, contacter un administrateur.", StatusCode = HttpStatusCode.InternalServerError, Data = ex.Message };
        }

        public static ResponseData Error(string message, object data)
        {
            return new ResponseData { Message = message, Data = data, StatusCode = HttpStatusCode.InternalServerError};
        }
    }
}
