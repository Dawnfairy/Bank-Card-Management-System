using BankCardProject.Models;

namespace BankCardProject.Repositories.Interfaces
{
    public interface IErrorLogRepository
    {
        Task SaveLogAsync(ErrorLog log);
    }
}
