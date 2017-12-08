using Data;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using System;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace WebCore.Command.BranchUpdate
{
    public class BranchUpdateNameCommandHandler : ICommandHandler<BranchUpdateNameCommand>
    {
        public void Handle(BranchUpdateNameCommand command)
        {
            using (var uow = new UnitOfWork<EF>())
            {
                try
                {
                    //update
                    var branch = uow.Repository<Branch>().Get(command.Id);
                    branch.Name = command.Name;
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
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
