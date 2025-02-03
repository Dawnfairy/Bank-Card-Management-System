using BankCardProject.Data;
using BankCardProject.Models;
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
            var cardList = await _context.BankCards.ToListAsync();
            return cardList;
        }

        public async Task<BankCard?> GetByIdAsync(int id)
        {
            return await _context.BankCards
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> CreateAsync(BankCard card)
        {
            try
            {
                await _context.BankCards.AddAsync(card);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(BankCard card)
        {
            try
            {
                _context.BankCards.Update(card);
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
            var existing = await _context.BankCards.FindAsync(id);
            if (existing != null)
            {
                _context.BankCards.Remove(existing);
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
