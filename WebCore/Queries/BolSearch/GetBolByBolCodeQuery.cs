using Data;
using Infrastructure.Queries;

namespace WebCore.Queries
{
    public class GetBolByBolCodeQuery:IQuery<BillOfLanding>
    {
        public string BolCode { get; set; }
    }
}
