using Data;
using Infrastructure.Queries;
using Infrastructure.Repository;
using System.Collections.Generic;

namespace WebCore.Queries
{
    public class GetAllStatusCodeQueryHandler : IQueryHandler<GetAllStatusCodeQuery, IEnumerable<Status>>
    {
        public IEnumerable<Status> Handle(GetAllStatusCodeQuery query)
        {
            var uow = new UnitOfWork<EF>();
            var result = uow.Repository<Status>().GetAll();
            uow.Dispose();
            return result;
        }
    }
}