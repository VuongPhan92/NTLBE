using Data;
using Domain.Command;
using Domain.IServices;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using System;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace WebCore.Command.EmployeeAdd
{
    public class EmployeeAddCommandHandler : ICommandHandler<EmployeeAddCommand>
    {
        private readonly IActivityService iActivityServices;
        public EmployeeAddCommandHandler(IActivityService _iActivityServices)
        {
            iActivityServices = _iActivityServices;
        }
        public void Handle(EmployeeAddCommand command)
        {
            using (var uow = new UnitOfWork<EF>())
            {
                try
                {
                    //add
                    var employee = new Employee();
                    employee.FullName = command.EmployeeInfo.FullName;
                    employee.DOB = command.EmployeeInfo.DOB;
                    employee.Address = command.EmployeeInfo.Address;
                    employee.Phone = command.EmployeeInfo.Phone;
                    employee.CreatedDate = System.DateTime.Now;
                    uow.Repository<Employee>().Add(employee);
                    uow.SubmitChanges();
                    //log
                    var activity = new Activity();
                    activity.Source = "Nhân Viên";
                    activity.Source_Id = employee.EmpId;
                    activity.Action = "Tạo";
                    activity.Current_value = employee.FullName;
                    activity.CreatedDate = employee.CreatedDate;
                    //activity.CreatedBy = "Admin";
                    iActivityServices.AddActivity(new ActivityAddCommand { Activity = activity });
                    //confirm
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
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
