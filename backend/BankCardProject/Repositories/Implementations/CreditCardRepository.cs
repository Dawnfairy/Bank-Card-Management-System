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
            if (_context == null)
            {
                throw new DataAccessException(Resources.ERR1008);
            }
            var cardList = await _context.CreditCards.Where(card => card.IsActive == true).ToListAsync();
            return cardList;
        }

        public async Task<CreditCard?> GetByIdAsync(int id)
        {
            if (_context == null)
            {
                throw new DataAccessException(Resources.ERR1008);
            }
            return await _context.CreditCards.SingleOrDefaultAsync(c => c.Id == id && c.IsActive);
        }
        
        public async Task<CreditCard> CreateAsync(CreditCard card)
        {
            if (_context == null)
            {
                throw new DataAccessException(Resources.ERR1008);
            }
            card.IsActive = true;
            await _context.CreditCards.AddAsync(card);
            await _context.SaveChangesAsync();
            return card;

        }

        public async Task UpdateAsync(CreditCard card)
        {
            if (_context == null)
            {
                throw new DataAccessException(Resources.ERR1008);
            }
            _context.CreditCards.Update(card);
            
            await _context.SaveChangesAsync();
            
        }

        public async Task<bool> ExistsAsync(string cardNumber)
        {
            if (_context == null)
            {
                throw new DataAccessException(Resources.ERR1008);
            }
            bool isExists = await _context.CreditCards
                            .AnyAsync(c => c.CardNumber == cardNumber && c.IsActive);

            return isExists;
        }
    }
}
