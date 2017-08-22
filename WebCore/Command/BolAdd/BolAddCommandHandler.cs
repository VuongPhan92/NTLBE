using Data;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using System.Data.Entity.Validation;
using System.Diagnostics;
namespace WebCore.Command
{
    public class BolAddCommandHandler : ICommandHandler<BolAddCommand>
    {
        public void Handle(BolAddCommand command)
        {
            using ( var uow = new UnitOfWork<EF>())
            {
                try
                {
                    foreach (var branch in command.Branches)
                    {
                        uow.Repository<Branch>().Attach(branch);
                        command.BOL.Branches.Add(branch);
                    }
                    foreach (var customer in command.Customers)
                    {
                        uow.Repository<Customer>().Attach(customer);
                        command.BOL.Customers.Add(customer);
                    }
                    
                    uow.Repository<BillOfLanding>().Add(command.BOL);
                    uow.SubmitChanges();
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
                }
            }         
        }
    }
}