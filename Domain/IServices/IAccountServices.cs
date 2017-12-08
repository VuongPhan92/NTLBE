using Data;

namespace Domain.IServices
{
    public interface IAccountServices
    {
        Account ValidateAccount(string username, string password);
    }
}
