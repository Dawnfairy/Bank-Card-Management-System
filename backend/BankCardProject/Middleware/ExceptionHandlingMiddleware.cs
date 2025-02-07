using System.Net;
using System.Text.Json;
using BankCardProject.Exceptions;
using BankCardProject.Helpers;
using BankCardProject.Services.Interfaces;

namespace BankCardProject.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;


        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var logLevel = GetLogLevel(ex);

                var messageTemplate = (logLevel == LogLevel.Error || logLevel == LogLevel.Critical)
    ? "Beklenmeyen bir hata oluştu: {Message}"
    : "Hata yakalandı: {Message}";

                _logger.Log(logLevel, ex, messageTemplate, ex.Message);


                using var scope = context.RequestServices.CreateScope();
                var errorLogService = scope.ServiceProvider.GetRequiredService<IErrorLogService>();
                await errorLogService.LogErrorAsync(ex);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            int errorCode = 0;
            string message = "Bilinmeyen bir hata oluştu.";


            if (ex is CustomException customEx)
            {
                statusCode = customEx.StatusCode;
                errorCode = customEx.ErrorCode;
                message = customEx.Message;
            }

            // HTTP Status Code ayarla
            context.Response.StatusCode = (int)statusCode;

            // JSON hata yanıtı oluştur
            var errorResponse = new
            {
                StatusCode = (int)statusCode,
                ErrorCode = errorCode,
                Message = message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }

        private static LogLevel GetLogLevel(Exception ex)
        {
            return ex switch
            {
                // Kullanıcı hataları - Warning olarak loglansın
                InvalidParameterException => LogLevel.Warning,
                BadRequestException => LogLevel.Warning,
                NotFoundException => LogLevel.Warning,
                DuplicateRecordException => LogLevel.Warning,
                UnauthorizedException => LogLevel.Warning,

                // Veri erişimi veya kritik hatalar - Error olarak loglansın
                DataAccessException => LogLevel.Error,
                OperationFailedException => LogLevel.Error,

                // Genel diğer hatalar - Critical olarak loglansın
                _ => LogLevel.Critical
            };
        }
    }
}
