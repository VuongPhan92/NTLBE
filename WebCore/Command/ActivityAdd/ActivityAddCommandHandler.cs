using Data;
using Domain.Command;
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
    public class ActivityAddCommandHandler : ICommandHandler<ActivityAddCommand>
    {
        public void Handle(ActivityAddCommand command)
        {
            using ( var uow = new UnitOfWork<EF>())
            {
                try
                {     
                    uow.Repository<Activity>().Add(command.Activity);
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
