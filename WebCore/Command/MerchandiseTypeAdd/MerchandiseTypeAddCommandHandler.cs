using Data;
using Domain.Command;
using Domain.IServices;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace WebCore.Command.MerchandiseTypeAdd
{
    public class MerchandiseTypeAddCommandHandler : ICommandHandler<MerchandiseTypeAddCommand>
    {
        private readonly IActivityService iActivityServices;
        public MerchandiseTypeAddCommandHandler(IActivityService _iActivityServices)
        {
            iActivityServices = _iActivityServices;
        }
        public void Handle(MerchandiseTypeAddCommand command)
        {
           using (var uow = new UnitOfWork<EF>())
            {
                try
                {
                    //add
                    var merchandiseType = new MerchandiseType();
                    merchandiseType.MerchandiseType1 = command.MerchandiseType.MerchandiseType1;
                    merchandiseType.CalculationUnit = command.MerchandiseType.CalculationUnit;
                    merchandiseType.Description = command.MerchandiseType.Description;
                    merchandiseType.CreatedDate = System.DateTime.Now;
                    uow.Repository<MerchandiseType>().Add(merchandiseType);
                    uow.SubmitChanges();
                    //log
                    var activity = new Activity();
                    activity.Source = "Loại Hàng";
                    activity.Source_Id = merchandiseType.Id;
                    activity.Action = "Tạo";
                    activity.Current_value = merchandiseType.MerchandiseType1;
                    activity.CreatedDate = merchandiseType.CreatedDate;
                    activity.CreatedBy = "Admin";
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
            }
        }
    }
}
