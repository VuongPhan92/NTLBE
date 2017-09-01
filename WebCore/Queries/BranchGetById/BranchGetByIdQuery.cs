using Data;
using Infrastructure.Queries;

namespace WebCore.Queries
{
    public class BranchGetByIdQuery : IQuery<Branch>
    {
        public int Id { get; set; }
    }
}
