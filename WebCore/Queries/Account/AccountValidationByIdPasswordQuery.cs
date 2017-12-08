using Data;
using Infrastructure.Queries;

namespace WebCore.Queries
{
    public class AccountValidationByIdPasswordQuery : IQuery<Account>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
