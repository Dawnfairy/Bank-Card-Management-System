using System.Net;

namespace BankCardProject.Helpers
{
    public class CustomException : Exception
    {
        public int ErrorCode { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public CustomException(string message, int errorCode = 0, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }

        public CustomException(string message, int errorCode, HttpStatusCode statusCode, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }
    }
}
