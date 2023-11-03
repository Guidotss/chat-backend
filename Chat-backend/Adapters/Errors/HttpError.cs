using Microsoft.AspNetCore.Http.HttpResults;

namespace Chat_backend.Adapters.Errors
{
    public class HttpError : System.Exception
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public HttpError(string message, int statusCode)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
