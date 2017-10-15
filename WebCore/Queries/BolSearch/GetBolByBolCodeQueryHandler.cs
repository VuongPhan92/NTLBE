using Data;
using Infrastructure.Queries;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCore.Queries.BolSearch
{
    public class GetBolByBolCodeQueryHandler : IQueryHandler<GetBolByBolCodeQuery, BillOfLanding>
    {
        public BillOfLanding Handle(GetBolByBolCodeQuery query)
        {
            var uow = new UnitOfWork<EF>();
            var result = uow.Repository<BillOfLanding>().GetAll("Branches,Customers,Status").SingleOrDefault(p => !p.DeletedDate.HasValue && p.BolCode.Equals(query.BolCode));
            return result;
        }
    }
}
