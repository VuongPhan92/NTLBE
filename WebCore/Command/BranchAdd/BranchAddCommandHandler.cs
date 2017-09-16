using Data;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCore.Command
{
    public class BranchAddCommandHandler : ICommandHandler<BranchAddCommand>
    {
        public void Handle(BranchAddCommand command)
        {
            using (var uow = new UnitOfWork<EF>())
            {
                try
                {
                    var branch = new Branch();
                    branch.Name = command.Branch.Name;
                    branch.Address = command.Branch.Address;
                    branch.BranchCode = command.Branch.BranchCode;
                    branch.Email = command.Branch.Email;
                    branch.Phone = command.Branch.Phone;
                    branch.Description = command.Branch.Description;
                    branch.CreatedDate = System.DateTime.Now;
                    uow.Repository<Branch>().Add(branch);
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
