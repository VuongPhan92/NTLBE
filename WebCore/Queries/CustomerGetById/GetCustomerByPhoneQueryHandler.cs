using Data;
using Infrastructure.Queries;
using Infrastructure.Repository;

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
