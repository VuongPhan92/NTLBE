using Data;
using Infrastructure.Queries;
using Infrastructure.Repository;
using System;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace WebCore.Queries
{
    public class AccountValidationByIdPasswordQueryHandler : IQueryHandler<AccountValidationByIdPasswordQuery, Account>
    {
        public Account Handle(AccountValidationByIdPasswordQuery query)
        {
            using (var uow = new UnitOfWork<EF>())
            {
                try
                {
                    var temp = uow.Repository<Account>().GetById(p => p.Username == query.Username);
                    if (temp.Password == query.Password)
                    {
                        return temp;
                    }
                    return null;
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }
                    return null;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
