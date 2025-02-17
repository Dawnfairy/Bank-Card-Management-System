using BankCardProject.Data;
using BankCardProject.Models;
using BankCardProject.Repositories.Interfaces;

namespace BankCardProject.Repositories.Implementations
{
    public class ErrorLogRepository : IErrorLogRepository
    {
        private readonly AppDbContext _context;

        public ErrorLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveLogAsync(ErrorLog log)
        {
            await _context.ErrorLogs.AddAsync(log);
            await _context.SaveChangesAsync();
        }
    }
}
