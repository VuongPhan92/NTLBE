using Core.PageResult;
using Data;
using Infrastructure.Queries;

namespace WebCore.Queries
{
    public class CustomerSearchQuery : IQuery<PagedListResult<Customer>>
    {
        public SearchQuery<Customer> SearchQuery { get; set; }
    }
}
