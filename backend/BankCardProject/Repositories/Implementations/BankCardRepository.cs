using BankCardProject.Data;
using BankCardProject.Exceptions;
using BankCardProject.Models;
using BankCardProject.Properties;
using BankCardProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankCardProject.Repositories.Implementations
{
    public class BankCardRepository : IBankCardRepository
    {
        private readonly AppDbContext _context;

        public BankCardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BankCard>> GetAllAsync()
        {
            return await _context.BankCards.Where(card => card.IsActive == true).ToListAsync();
        }

        public async Task<BankCard?> GetByIdAsync(int id)
        {
            return await _context.BankCards.SingleOrDefaultAsync(c => c.Id == id && c.IsActive);
        }

        public async Task<BankCard> CreateAsync(BankCard card)
        {
            card.IsActive = true;
            await _context.BankCards.AddAsync(card);
            await _context.SaveChangesAsync();
            return card;
        }

        public async Task UpdateAsync(BankCard card)
        {
            _context.BankCards.Update(card);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> ExistsAsync(string cardNumber)
        {
            return await _context.BankCards.AnyAsync(c => c.CardNumber == cardNumber && c.IsActive);
        }
    }
}
  