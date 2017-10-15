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
    public class BolUpdateStatusByBolCodeCommandHandler : ICommandHandler<BolUpdateStatusByBolCodeCommand>
    {
        protected readonly IStatusServices statusServices;
        private readonly IActivityService iActivityServices;
        public BolUpdateStatusByBolCodeCommandHandler(IStatusServices _statusServices,
            IActivityService _iActivityServices
            )
        {
            statusServices = _statusServices;
            iActivityServices = _iActivityServices;
        }
        public void Handle(BolUpdateStatusByBolCodeCommand command)
        {
            using (var uow = new UnitOfWork<EF>())
            {
                try
                {
                    //update            
                    var bolEntity = uow.Repository<BillOfLanding>().Find(p=>p.BolCode.Equals(command.BolCode)).FirstOrDefault();
                    var statusCodeList = statusServices.GetAllStatusCode().OrderBy(p => p.Id).ToList();
                    var previousValue = statusCodeList.Where(p => p.Id == bolEntity.StatusCode).Select(p => p.StatusName).FirstOrDefault();
                    if (bolEntity.StatusCode != statusCodeList[statusCodeList.Count() - 1].Id)
                    {
                        bolEntity.StatusCode = bolEntity.StatusCode + 1;
                        bolEntity.ReceiveDate = DateTime.Now;

                    }
                    uow.Repository<BillOfLanding>().Update(bolEntity);
                    uow.SubmitChanges();
                    //log
                    var activity = new Activity();
                    activity.Source = "Trạng thái đơn hàng";
                    activity.Source_Id = bolEntity.Id;
                    activity.Action = "Đổi";
                    activity.Previous_value = previousValue;
                    activity.Current_value = statusCodeList.Where(p => p.Id == bolEntity.StatusCode).Select(p => p.StatusName).FirstOrDefault();
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
            }
        }
    }
}
