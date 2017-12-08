using Data;
using Infrastructure.Queries;

namespace WebCore.Queries
{
    public class GetBolByIdQuery:IQuery<BillOfLanding>
    {
        public int Id { get; set; }
    }
}
