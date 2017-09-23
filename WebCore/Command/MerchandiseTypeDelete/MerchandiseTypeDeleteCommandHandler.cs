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
    class MerchandiseTypeDeleteCommandHandler : ICommandHandler<MerchandiseTypeDeleteCommand>
    {
        private readonly IActivityService iActivityServices;
        public MerchandiseTypeDeleteCommandHandler(IActivityService _iActivityServices)
        {
            iActivityServices = _iActivityServices;
        }
        public void Handle(MerchandiseTypeDeleteCommand command)
        {
            using (var uow = new UnitOfWork<EF>())
            {
                try
                {
                    //delete
                    var merchandiseType = uow.Repository<MerchandiseType>().Get(command.Id);
                    merchandiseType.DeletedDate = System.DateTime.Now;
                    uow.Repository<MerchandiseType>().Update(merchandiseType);
                    uow.SubmitChanges();
                    //log
                    var activity = new Activity();
                    activity.Source = "Loại Hàng";
                    activity.Source_Id = command.Id;
                    activity.Action = "Xóa";
                    activity.Current_value = "";
                    activity.Previous_value = merchandiseType.MerchandiseType1;
                    activity.CreatedDate = merchandiseType.DeletedDate;
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
