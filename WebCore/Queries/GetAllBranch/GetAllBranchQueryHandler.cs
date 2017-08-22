using Data;
using Infrastructure.Queries;
using Infrastructure.Repository;
using System.Collections.Generic;

namespace WebCore.Queries
{
    public class GetAllBranchQueryHandler : IQueryHandler<GetAllBranchQuery, IEnumerable<Branch>>
    {
      
        public IEnumerable<Branch> Handle(GetAllBranchQuery query)
        {
            var uow = new UnitOfWork<EF>();
            var result = uow.Repository<Branch>().GetAll();
            uow.Dispose();
            return result;
        }
    }
}