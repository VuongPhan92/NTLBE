using Data;
using Infrastructure.Queries;
using Infrastructure.Repository;
using System.Collections.Generic;
using System.Linq;

namespace WebCore.Queries
{
    public class BranchGetAllQueryHandler : IQueryHandler<BranchGetAllQuery, IEnumerable<Branch>>
    {
        public IEnumerable<Branch> Handle(BranchGetAllQuery query)
        {
            var uow = new UnitOfWork<EF>();
            var result = uow.Repository<Branch>().GetAll().Where(p => !p.DeletedDate.HasValue);
            uow.Dispose();
            return result;
        }
    }
}