using Data;
using Domain.IServices;
using Infrastructure.Decorator;
using Infrastructure.Queries;
using System;
using WebCore.Queries;

namespace WebCore.Services
{
    public class AccountServices: IService<Account>, IAccountServices
    {
        private readonly IQueryHandler<AccountValidationByIdPasswordQuery, Account> accountValidateHandler;
        public AccountServices(IQueryHandler<AccountValidationByIdPasswordQuery, Account> _accountValidateHandler)
        {
            accountValidateHandler = _accountValidateHandler;
        }
        
        public Account ValidateAccount(string username, string password)
        {
            try
            {
                return accountValidateHandler.Handle(new AccountValidationByIdPasswordQuery { Username = username, Password = password });
                //log
                //track ip
            }
            catch (Exception)
            {
                //log error
                //track ip
                return null;
            }
        }
    }
}
