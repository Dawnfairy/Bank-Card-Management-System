using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankCardProject.Models
{
    public class BankCard
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CVV { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }  // Hesap numarası
        public string IBAN { get; set; }  // IBAN numarası
        
        [Precision(18, 2)]
        public decimal Balance { get; set; }  // Kart bakiyesi
        
        [Precision(18, 2)]
        public decimal WithdrawalLimit { get; set; }  // Günlük para çekme limiti
        public bool IsContactless { get; set; }  // Temassız ödeme desteği
    }
}
