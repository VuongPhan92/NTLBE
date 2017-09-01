using Data;
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

namespace WebCore.Command.BolStatusUpdate
{
    class BolStatusUpdateCommandHandler : ICommandHandler<BolStatusUpdateCommand>
    {
        protected readonly IStatusServices statusServices;
        public BolStatusUpdateCommandHandler(IStatusServices _statusServices)
        {
            statusServices = _statusServices;
        }
        public void Handle(BolStatusUpdateCommand command)
        {
            using (var uow = new UnitOfWork<EF>())
            {
                try
                {                  
                    var bolEntity = uow.Repository<BillOfLanding>().GetById(p=>p.Id == command.Id);
                    var statusCodeList = statusServices.GetAllStatusCode().OrderBy(p=>p.Id).ToList();
                    if (bolEntity.StatusCode  != statusCodeList[statusCodeList.Count()-1].Id)
                    {
                        bolEntity.StatusCode = bolEntity.StatusCode + 1;
                    }
                    uow.Repository<BillOfLanding>().Update(bolEntity);        
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
