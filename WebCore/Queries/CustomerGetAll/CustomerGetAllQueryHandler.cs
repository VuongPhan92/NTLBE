using Data;
using Infrastructure.Queries;
using Infrastructure.Repository;
using System.Collections.Generic;
using System.Linq;

namespace WebCore.Queries
{
    public class CustomerGetAllQueryHandler : IQueryHandler<CustomerGetAllQuery, IEnumerable<Customer>>
    {
       
        public IEnumerable<Customer> Handle(CustomerGetAllQuery query)
        {
            var uow = new UnitOfWork<EF>();
            var result = uow.Repository<Customer>().GetAll().Where(p => !p.DeletedDate.HasValue);
            uow.Dispose();
            return result;
        }
    }
}
