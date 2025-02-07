using BankCardProject.Data;
using BankCardProject.Helpers;
using BankCardProject.Models;
using BankCardProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BankCardProject.Services.Implementations
{
    public class ErrorLogService : IErrorLogService
    {
        private readonly AppDbContext _dbContext;

        public ErrorLogService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task LogErrorAsync(Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            int errorCode = 0;

            if (ex is CustomException customEx)
            {
                statusCode = customEx.StatusCode;
                errorCode = customEx.ErrorCode;
            }

            var log = new ErrorLog
            {
                ErrorType = ex.GetType().Name,  // Hata tipi
                StatusCode = (int)statusCode,   // HTTP kodu
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                CreatedAt = DateTime.Now
            };


            _dbContext.ErrorLogs.Add(log);
            await _dbContext.SaveChangesAsync();
        }
    }

}
