using Data;
using Infrastructure.Queries;

namespace WebCore.Queries
{
    public class GetCustomerByPhoneQuery : IQuery<Customer>
    {
        public  string Phone { get; set; }
    }
}
