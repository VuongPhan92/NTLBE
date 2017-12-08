using Data;
using Domain.Command;
using Domain.IServices;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using System;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;

namespace WebCore.Command.BolStatusUpdate
{
    class BolStatusUpdateCommandHandler : ICommandHandler<BolStatusUpdateCommand>
    {
        protected readonly IStatusServices statusServices;
        private readonly IActivityService iActivityServices;
        public BolStatusUpdateCommandHandler(IStatusServices _statusServices,
            IActivityService _iActivityServices
            )
        {
            statusServices = _statusServices;
            iActivityServices = _iActivityServices;
        }
        public void Handle(BolStatusUpdateCommand command)
        {
            using (var uow = new UnitOfWork<EF>())
            {
                try
                {      
                    //update            
                    var bolEntity = uow.Repository<BillOfLanding>().Get(command.Id);
                    var statusCodeList = statusServices.GetAllStatusCode().OrderBy(p=>p.Id).ToList();
                    var previousValue = statusCodeList.Where(p=>p.Id==bolEntity.StatusCode).Select(p=>p.StatusName).FirstOrDefault();
                    if (bolEntity.StatusCode  != statusCodeList[statusCodeList.Count()-1].Id)
                    {
                        bolEntity.StatusCode = bolEntity.StatusCode + 1;
                        bolEntity.ReceiveDate = DateTime.Now;

                    }
                    uow.Repository<BillOfLanding>().Update(bolEntity);        
                    uow.SubmitChanges();
                    //log
                    var activity = new Activity();
                    activity.Source = "Trạng thái đơn hàng";
                    activity.Source_Id = command.Id;
                    activity.Action = "Đổi";
                    activity.Previous_value = previousValue;
                    activity.Current_value = statusCodeList.Where(p=>p.Id== bolEntity.StatusCode).Select(p=>p.StatusName).FirstOrDefault();
                    activity.CreatedDate = System.DateTime.Now;
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
