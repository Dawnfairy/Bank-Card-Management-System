using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankCardProject.Models
{
    public class ErrorLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string LogType { get; set; } // "ErrorLog", "RequestLog", "SecurityLog"

        public string HttpMethod { get; set; } // GET, POST, PUT, DELETE
        public string RequestPath { get; set; } // API isteğinin yapıldığı URL

        public string ClientIP { get; set; } // İstemcinin IP adresi
        public double ResponseTime { get; set; } // Yanıt süresi (ms)

        public string ErrorType { get; set; } // ValidationError, DatabaseError, UnauthorizedAccess, etc.
        public int StatusCode { get; set; } // HTTP 400, 500 gibi

        [Required]
        public string Message { get; set; } // Hata veya olay mesajı

        public string StackTrace { get; set; } // Opsiyonel: Hatanın detayları
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Log kaydının zamanı
    }
}
