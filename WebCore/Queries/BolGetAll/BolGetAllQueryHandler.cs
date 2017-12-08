using Data;
using Infrastructure.Queries;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Globalization;
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
                var filterDate = System.DateTime.ParseExact(query.FilterString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var result = uow.Repository<BillOfLanding>().GetAll("Branches,Customers").Where(p => !p.DeletedDate.HasValue && p.CreatedDate.Value.ToShortDateString().Equals(filterDate.ToShortDateString()));
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}