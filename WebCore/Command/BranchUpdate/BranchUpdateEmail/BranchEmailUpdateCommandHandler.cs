using Data;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace WebCore.Command
{
    public class BranchEmailUpdateCommandHandler : ICommandHandler<BranchEmailUpdateCommand>
    {
        public void Handle(BranchEmailUpdateCommand command)
        {
            using (var uow = new UnitOfWork<EF>())
            {
                try
                {
                    //update
                    var branch = uow.Repository<Branch>().Get(command.Id);
                    branch.Email = command.Email;
                    uow.Repository<Branch>().Update(branch);
                    uow.SubmitChanges();
                    //log
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
