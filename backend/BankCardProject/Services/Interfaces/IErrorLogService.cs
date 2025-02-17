namespace BankCardProject.Services.Interfaces
{
    public interface IErrorLogService
    {
        Task LogErrorAsync(Exception ex, bool isError);
    }
}
