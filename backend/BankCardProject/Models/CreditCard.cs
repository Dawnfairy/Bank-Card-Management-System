using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankCardProject.Models
{
    public class CreditCard 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CVV { get; set; }
        public string BankName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        [Precision(18, 2)]
        public decimal CreditLimit { get; set; }  // Kartın limiti

        [Precision(18, 2)]
        public decimal AvailableBalance { get; set; }  // Kullanılabilir bakiye

        [Precision(18, 2)]
        public decimal MinimumPayment { get; set; }  // Minimum ödeme tutarı
        public double InterestRate { get; set; }  // Faiz oranı
        public DateTime BillingDate { get; set; }  // Ekstre tarihi
        public DateTime DueDate { get; set; }  // Son ödeme tarihi
        public bool Installments { get; set; }  // Taksit desteği var mı?
    }
}
