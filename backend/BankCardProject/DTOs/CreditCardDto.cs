
namespace BankCardProject.DTOs
{
    public class CreditCardDto
    {

        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CVV { get; set; }
        public string BankName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal AvailableBalance { get; set; }
        public decimal MinimumPayment { get; set; }
        public double InterestRate { get; set; }
        public DateTime BillingDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool Installments { get; set; }
    }
}
