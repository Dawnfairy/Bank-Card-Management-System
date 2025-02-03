using BankCardProject.Data;
using BankCardProject.Models;
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
            var cardList = await _context.CreditCards.ToListAsync();
            return cardList;
        }

        public async Task<CreditCard?> GetByIdAsync(int id)
        {
            return await _context.CreditCards
                .SingleOrDefaultAsync(c => c.Id == id);
        }
        
        public async Task<bool> CreateAsync(CreditCard card)
        {
            try
            {
                await _context.CreditCards.AddAsync(card);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(CreditCard card)
        {
            try
            {
                _context.CreditCards.Update(card);
                var changes = await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.CreditCards.FindAsync(id);
            if (existing != null)
            {
                _context.CreditCards.Remove(existing);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
