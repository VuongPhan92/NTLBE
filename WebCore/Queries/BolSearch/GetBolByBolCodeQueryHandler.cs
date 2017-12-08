using Data;
using Infrastructure.Queries;
using Infrastructure.Repository;
using System.Linq;

namespace WebCore.Queries.BolSearch
{
    public class GetBolByBolCodeQueryHandler : IQueryHandler<GetBolByBolCodeQuery, BillOfLanding>
    {
        public BillOfLanding Handle(GetBolByBolCodeQuery query)
        {
            var uow = new UnitOfWork<EF>();
            
            var result = uow.Repository<BillOfLanding>().GetAll("DeliveryType1,Branches,Customers,Status,MerchandiseType1").SingleOrDefault(p => !p.DeletedDate.HasValue && p.BolCode.Equals(query.BolCode));
            return result;
        }
    }
}
