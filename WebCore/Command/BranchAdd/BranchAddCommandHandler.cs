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

namespace WebCore.Command
{
    public class BranchAddCommandHandler : ICommandHandler<BranchAddCommand>
    {
        private readonly IActivityService iActivityServices;
        public BranchAddCommandHandler(IActivityService _iActivityServices)
        {
            iActivityServices = _iActivityServices;
        }
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
                    uow.Repository<Branch>().Add(command.Branch);
                    uow.SubmitChanges();

                    //log
                    var activity = new Activity();
                    activity.Source = "Chi nhánh";
                    activity.Source_Id = command.Branch.Id;
                    activity.Action = "Tạo";
                    activity.Current_value = command.Branch.Name;
                    activity.CreatedDate = command.Branch.CreatedDate;
                    activity.CreatedBy = "Admin";
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
