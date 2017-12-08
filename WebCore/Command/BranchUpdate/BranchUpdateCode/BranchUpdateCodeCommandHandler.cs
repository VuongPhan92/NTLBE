using Data;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using System;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace WebCore.Command
{
    public class BranchUpdateCodeCommandHandler : ICommandHandler<BranchUpdateCodeCommand>
    {
        public void Handle(BranchUpdateCodeCommand command)
        {
            using (var uow = new UnitOfWork<EF>())
            {
                try
                {
                    //update
                    var branch = uow.Repository<Branch>().Get(command.Id);
                    branch.BranchCode = command.BranchCode;
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
