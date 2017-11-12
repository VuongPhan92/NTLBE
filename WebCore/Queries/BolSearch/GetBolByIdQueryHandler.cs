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
    public class GetBolByIdQueryHandler : IQueryHandler<GetBolByIdQuery, BillOfLanding>
    {
        public BillOfLanding Handle(GetBolByIdQuery query)
        {
            var uow = new UnitOfWork<EF>();
            var result = uow.Repository<BillOfLanding>().GetAll("Branches,Customers,Status,MerchandiseType1").SingleOrDefault(p => !p.DeletedDate.HasValue && p.Id==query.Id);
            return result;
        }
    }
}
