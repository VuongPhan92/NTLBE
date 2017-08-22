using Core.PageResult;
using Data;
using Infrastructure.Queries;

namespace WebCore.Queries
{
    public class SearchCustomerQuery:IQuery<PagedListResult<Customer>>
    {
        public SearchQuery<Customer> SearchQuery { get; set; }
    }
}
