using Data;
using Infrastructure.Queries;
using Infrastructure.Repository;
using System.Collections.Generic;
using System.Linq;
namespace WebCore.Queries
{
    public class BolGetAllQueryHandler : IQueryHandler<BolGetAllQuery, IEnumerable<BillOfLanding>>
    {
       
        public IEnumerable<BillOfLanding> Handle(BolGetAllQuery query)
        {
            var uow = new UnitOfWork<EF>();
            var result = uow.Repository<BillOfLanding>().GetAll();
            uow.Dispose();
            return result;
        }
    }
}