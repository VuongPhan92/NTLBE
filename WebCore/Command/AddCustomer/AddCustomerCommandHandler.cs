using Data;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace WebCore.Command
{
    public class AddCustomerCommandHandler:ICommandHandler<AddCustomerCommand>
    {

        public void Handle(AddCustomerCommand command)
        {
            using (var uow=new UnitOfWork<EF>())
            {
                try
                { 
                    uow.Repository<Customer>().Add(command.Customer);
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
