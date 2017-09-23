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
    public class BranchPhoneUpdateCommandHandler : ICommandHandler<BranchPhoneUpdateCommand>
    {
        public void Handle(BranchPhoneUpdateCommand command)
        {
            using(var uow = new UnitOfWork<EF>())
            try
            {
                //update
                var branch = uow.Repository<Branch>().Get(command.Id);
                branch.Phone = command.Phone;
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
