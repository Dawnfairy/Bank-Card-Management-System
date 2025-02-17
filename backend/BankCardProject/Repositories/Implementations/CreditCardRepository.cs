using BankCardProject.Data;
using BankCardProject.Exceptions;
using BankCardProject.Models;
using BankCardProject.Properties;
using BankCardProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankCardProject.Repositories.Implementations
{
    public class CreditCardRepository : ICreditCardRepository
    {
        private readonly AppDbContext _context;

        public CreditCardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CreditCard>> GetAllAsync()
        {
             return await _context.CreditCards.Where(card => card.IsActive == true).ToListAsync();
        }

        public async Task<CreditCard?> GetByIdAsync(int id)
        {
            return await _context.CreditCards.SingleOrDefaultAsync(c => c.Id == id && c.IsActive);
        }
        
        public async Task<CreditCard> CreateAsync(CreditCard card)
        {
            card.IsActive = true;
            await _context.CreditCards.AddAsync(card);
            await _context.SaveChangesAsync();
            return card;
        }

        public async Task UpdateAsync(CreditCard card)
        {
            _context.CreditCards.Update(card);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string cardNumber)
        {
            return await _context.CreditCards.AnyAsync(c => c.CardNumber == cardNumber && c.IsActive);
        }            
    }
}
