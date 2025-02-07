using System.Text.Json;
using System.Net;

namespace BankCardProject.Helpers
{
    public static class ErrorHelper
    {
        public static string FormatErrorMessage(Exception ex)
        {
            if (ex is CustomException customEx)
            {
                return $"Hata Kodu: {customEx.ErrorCode} - {customEx.Message}\nDetay: {customEx.StackTrace}";
            }
            return $"Hata: {ex.Message}\nDetay: {ex.StackTrace}";
        }

        public static string FormatJsonErrorResponse(Exception ex)
        {
            int errorCode = 0;
            string message = "Bilinmeyen bir hata oluştu.";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

            if (ex is CustomException customEx)
            {
                errorCode = customEx.ErrorCode;
                message = customEx.Message;
                statusCode = customEx.StatusCode;
            }

            var errorResponse = new
            {
                StatusCode = (int)statusCode,
                ErrorCode = errorCode,
                Message = message
            };

            return JsonSerializer.Serialize(errorResponse);
        }
    }
}
