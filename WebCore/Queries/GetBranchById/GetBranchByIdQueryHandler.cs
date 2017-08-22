using Data;
using Infrastructure.Queries;
using Infrastructure.Repository;

namespace WebCore.Queries.GetBranchById
{
    public class GetBranchByIdQueryHandler : IQueryHandler<GetBranchByIdQuery, Branch>
    {
        public Branch Handle(GetBranchByIdQuery query)
        {
            var uow = new UnitOfWork<EF>();
            var result = uow.Repository<Branch>().Get(query.Id);
            uow.Dispose();
            return result;
        }
    }
}
