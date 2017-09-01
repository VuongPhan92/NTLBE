using Data;
using Infrastructure.Queries;
using Infrastructure.Repository;

namespace WebCore.Queries.GetBranchById
{
    public class BranchGetByIdQueyHandler : IQueryHandler<BranchGetByIdQuery, Branch>
    {
        public Branch Handle(BranchGetByIdQuery query)
        {
            var uow = new UnitOfWork<EF>();
            var result = uow.Repository<Branch>().Get(query.Id);
            uow.Dispose();
            return result;
        }
    }
}
