using Data;
using Infrastructure.Queries;
using Infrastructure.Repository;

namespace WebCore.Queries
{
    public class GetCustomerByIdQueryHandler  : IQueryHandler<GetCustomerByIdQuery, Customer>
    {
        public Customer Handle(GetCustomerByIdQuery query)
        {
            var uow = new UnitOfWork<EF>();
            var result = uow.Repository<Customer>().Get(query.Id);
            uow.Dispose();
            return result;
        }
    }
}
