using Data;
using Domain.Command;
using Domain.IServices;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCore.Command.EmployeeDelete
{
    class EmployeeDeleteCommandHandler : ICommandHandler<EmployeeDeleteCommand>
    {
        private readonly IActivityService iActivityServices;
        public EmployeeDeleteCommandHandler(IActivityService _iActivityServices)
        {
            iActivityServices = _iActivityServices;
        }
        public void Handle(EmployeeDeleteCommand command)
        {
            using (var uow = new UnitOfWork<EF>())
            {
                try
                {
                    //delete
                    var employee = uow.Repository<Employee>().Get(command.Id);
                    employee.DeletedDate = System.DateTime.Now;
                    uow.Repository<Employee>().Update(employee);
                    uow.SubmitChanges();
                    //log
                    var activity = new Activity();
                    activity.Source = "Nhân Viên";
                    activity.Source_Id = command.Id;
                    activity.Action = "Xóa";
                    activity.Current_value = "";
                    activity.Previous_value = employee.FullName;
                    activity.CreatedDate = employee.DeletedDate;
                    //activity.CreatedBy = "Admin";
                    iActivityServices.AddActivity(new ActivityAddCommand { Activity = activity });
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
