using Data;
using Infrastructure.Queries;
using Infrastructure.Repository;
using System.Collections.Generic;

namespace WebCore.Queries
{
    public class GetAllBolQueryHandler : IQueryHandler<GetAllBolQuery, IEnumerable<BillOfLanding>>
    {
       
        public IEnumerable<BillOfLanding> Handle(GetAllBolQuery query)
        {
            var uow = new UnitOfWork<EF>();
            var result = uow.Repository<BillOfLanding>().GetAll();
            uow.Dispose();
            return result;
        }
    }
}