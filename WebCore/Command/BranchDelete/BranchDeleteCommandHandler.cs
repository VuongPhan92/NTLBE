using Data;
using Domain.Command;
using Domain.IServices;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace WebCore.Command
{
    public class BranchDeleteCommandHandler : ICommandHandler<BranchDeleteCommand>
    {
        private readonly IActivityService iActivityServices;
        public BranchDeleteCommandHandler(IActivityService _iActivityServices)
        {
            iActivityServices = _iActivityServices;
        }
        public void Handle(BranchDeleteCommand command)
        {
           using(var uow = new UnitOfWork<EF>())
            {
                try
                {
                    //update
                    var branch = uow.Repository<Branch>().Get(command.Id);
                    branch.DeletedDate = System.DateTime.Now;
                    uow.Repository<Branch>().Update(branch);
                    uow.SubmitChanges();
                    //log
                    var activity = new Activity();
                    activity.Source = "Chi nhánh";
                    activity.Source_Id = branch.Id;
                    activity.Action = "Xóa";
                    activity.Current_value = branch.Name;
                    activity.CreatedDate = branch.DeletedDate;
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
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
