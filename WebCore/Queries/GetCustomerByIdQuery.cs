using Data;
using Infrastructure.Queries;

namespace WebCore.Queries
{
    public class GetCustomerByIdQuery : IQuery<Customer>
    {
        public int Id { get; set; }
    }
}
