using BankCardProject.Data;
using BankCardProject.DTOs;
using BankCardProject.Models;
using BankCardProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankCardProject.Repositories.Implementations
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }

      
    }
}
