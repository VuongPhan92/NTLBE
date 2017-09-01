using Core.PageResult;
using Data;
using Infrastructure.Queries;
using Infrastructure.Repository;

namespace WebCore.Queries
{
    public class CustomerSearchQueryHandler : IQueryHandler<CustomerSearchQuery, PagedListResult<Customer>>
    {
        public PagedListResult<Customer> Handle(CustomerSearchQuery query)
        {
            using (var uow = new UnitOfWork<EF>())
            {
                return uow.Repository<Customer>().Search(query.SearchQuery);
            }
        }
    }
}