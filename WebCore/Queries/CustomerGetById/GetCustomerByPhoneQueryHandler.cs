using Data;
using Infrastructure.Queries;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCore.Queries
{
    public class GetCustomerByIdQueryHandler : IQueryHandler<GetCustomerByPhoneQuery, Customer>
    {
        public Customer Handle(GetCustomerByPhoneQuery query)
        {
            var uow = new UnitOfWork<EF>();
            return uow.Repository<Customer>().GetById(p => p.Phone.Equals(query.Phone));
        }
    }
}
