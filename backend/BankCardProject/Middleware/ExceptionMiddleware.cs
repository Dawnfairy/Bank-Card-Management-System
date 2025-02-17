using BankCardProject.Exceptions;
using BankCardProject.Models;
using BankCardProject.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Net;

namespace BankCardProject.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IErrorLogService _errorLogService;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UnauthorizedException ex)
            {
                _logger.LogWarning($"Unauthorized access: {ex.Message}");
                await HandleExceptionAsync(context, ex, HttpStatusCode.Unauthorized, "UnauthorizedAccess");
            }
            catch (BadRequestException ex)
            {
                _logger.LogWarning($"Bad Request: {ex.Message}");
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest, "BadRequest");
            }
            catch (InvalidParameterException ex)
            {
                _logger.LogWarning($"Invalid Parameter: {ex.Message}");
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest, "InvalidParameter");
            }
            catch (NotFoundException ex)
            {
                _logger.LogInformation($"Not Found: {ex.Message}");
                await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound, "NotFound");
            }
            catch (DuplicateRecordException ex)
            {
                _logger.LogWarning($"Duplicate Record: {ex.Message}");
                await HandleExceptionAsync(context, ex, HttpStatusCode.Conflict, "DuplicateRecord");
            }
            catch (OperationFailedException ex)
            {
                _logger.LogError($"Operation Failed: {ex.Message}");
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError, "OperationFailed");
            }
            catch (DataAccessException ex)
            {
                _logger.LogError($"Data Access Error: {ex.Message}");
                await HandleExceptionAsync(context, ex, HttpStatusCode.ServiceUnavailable, "DataAccessError");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unhandled Exception: {ex.Message}");
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError, "UnhandledException");
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode, string errorType)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var errorResponse = ApiResponse<string>.ErrorResponse(exception.Message, errorType);

            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }
    }
}