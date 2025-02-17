using BankCardProject.Data;
using BankCardProject.Helpers;
using BankCardProject.Models;
using BankCardProject.Repositories.Interfaces;
using BankCardProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using System.Net;

namespace BankCardProject.Services.Implementations
{
    public class ErrorLogService : IErrorLogService
    {
        private readonly IErrorLogRepository _errorLogRepository;

        public ErrorLogService(IErrorLogRepository errorLogRepository)
        {
            _errorLogRepository = errorLogRepository;
        }


        public async Task LogErrorAsync(Exception ex, bool isError)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

            if (ex is CustomException customEx)
            {
                statusCode = customEx.StatusCode;
            }

            var log = new ErrorLog
            {
                LogType = isError ? "ErrorLog" : "WarningLog",  // Log tipi: "ErrorLog" veya "WarningLog"
                ErrorType = ex.GetType().Name,                   // Hata tipi
                StatusCode = (int)statusCode,                    // HTTP durum kodu
                Message = ex.Message,                            // Hata mesajı
                StackTrace = ex.StackTrace,                      // Hata detayları
                CreatedAt = DateTime.UtcNow,                     // Log kaydının zamanı

                // Opsiyonel alanlar (ilgili bilgiler varsa set edilebilir)
                //HttpMethod = string.Empty,    // Örnek: "GET", "POST", vb.
                //RequestPath = string.Empty,   // İstek yapılan URL
                //ClientIP = string.Empty,      // İstemcinin IP adresi
                //ResponseTime = 0              // Yanıt süresi (ms)
            };

            await _errorLogRepository.SaveLogAsync(log);
        }
    }

}
