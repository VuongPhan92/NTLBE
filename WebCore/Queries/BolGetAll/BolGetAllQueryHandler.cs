using Data;
using Infrastructure.Queries;
using Infrastructure.Repository;
using System.Collections.Generic;

namespace WebCore.Queries
{
    public class BolGetAllQueryHandler : IQueryHandler<BolGetAllQuery, IEnumerable<BillOfLanding>>
    {
       
        public IEnumerable<BillOfLanding> Handle(BolGetAllQuery query)
        {
            var uow = new UnitOfWork<EF>();
            string[] test = new string[2];
            test[0] = "Branch";
            test[1] = "Customer";
            var result = uow.Repository<BillOfLanding>().GetAll("Branches,Customers");
            uow.Dispose();
            return result;
        }
    }
}