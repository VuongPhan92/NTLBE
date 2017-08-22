using Core.PageResult;
using Data;
using Infrastructure.Queries;
using Infrastructure.Repository;

namespace WebCore.Queries
{
    public class SearchCustomerQueryHandler : IQueryHandler<SearchCustomerQuery, PagedListResult<Customer>>
    {
        public PagedListResult<Customer> Handle(SearchCustomerQuery query)
        {
            using (var uow = new UnitOfWork<EF>())
            {
                return uow.Repository<Customer>().Search(query.SearchQuery);
            }
        }
    }
}