using Data;
using Infrastructure.Queries;

namespace WebCore.Queries
{
    public class GetBranchByIdQuery : IQuery<Branch>
    {
        public int Id { get; set; }
    }
}
