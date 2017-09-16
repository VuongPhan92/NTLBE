using Data;
using Domain.ViewModels;
using Infrastructure.Queries;
using Infrastructure.Repository;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
namespace WebCore.Queries
{
    public class BolGetAllQueryHandler : IQueryHandler<BolGetAllQuery, IEnumerable<BillOfLanding>>
    {

        public IEnumerable<BillOfLanding> Handle(BolGetAllQuery query)
        {
            try
            {
                var uow = new UnitOfWork<EF>();
                var result = uow.Repository<BillOfLanding>().GetAll("Branches,Customers").Where(p => !p.DeletedDate.HasValue);
            //    result.Select(p => new BolManageVM
            //    {
            //        Id = p.Id,
            //        AdditionalFee = p.AdditionalFee,
            //        BolCode = p.BolCode,
            //        CollectInBehalf = p.CollectInBehalf,
            //        MerchandiseTypeId = p.MerchandiseType.Value,
            //        Weight = (float)p.Weight.Value,
            //        Quantity = p.Quantity.Value,
            //        Liabilities = p.Liabilities,
            //        Prepaid = p.Prepaid,
            //        PrepaidTemp = p.Prepaid.ToString(),
            //        ReceiveDate = p.ReceiveDate.Value.ToString(),
            //        ReceiveTime = p.ReceiveTime.Value.ToString(),
            //        SendAddress = p.SendAddress,
            //        IsGuarantee = p.IsGuarantee.Value,
            //        StatusCode = p.StatusCode.Value,
            //        MixedValue = p.MixValue,
            //        DeclareValue = p.DeclareValue,
            //        Total = p.Total,
            //        IsOnHand = p.IsOnHand.Value,
            //        Start = p.Start.Value,
            //        Contact = p.Contact.Value,
            //    }
            //);
                uow.Dispose();
                return result;
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
                return null;
            }
        }
    }
}