using Data;
using Infrastructure.Queries;
using Infrastructure.Repository;
using System.Collections.Generic;

namespace WebCore.Queries
{
    public class GetAllCustomerQueryHanler:IQueryHandler<GetAllCustomerQuery,IEnumerable<Customer>>
    {
       
        public IEnumerable<Customer> Handle(GetAllCustomerQuery query)
        {
            var uow = new UnitOfWork<EF>();
            var result = uow.Repository<Customer>().GetAll();
            uow.Dispose();
            return result;
        }
    }
}
