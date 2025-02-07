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
        public string ErrorType { get; set; } // Hata tipi (örn: NotFoundException, BadRequestException)

        [Required]
        public int StatusCode { get; set; } // HTTP hata kodu (örn: 404, 500)

        [Required]
        public string Message { get; set; }

        public string StackTrace { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
