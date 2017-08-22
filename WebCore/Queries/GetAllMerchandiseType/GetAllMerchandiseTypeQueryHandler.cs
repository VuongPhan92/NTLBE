using Data;
using Infrastructure.Queries;
using Infrastructure.Repository;
using System.Collections.Generic;

namespace WebCore.Queries
{
    public class GetAllMerchandiseTypeQueryHandler : IQueryHandler<GetAllMerchandiseTypeQuery, IEnumerable<MerchandiseType>>
    {
        public IEnumerable<MerchandiseType> Handle(GetAllMerchandiseTypeQuery query)
        {
            var uow = new UnitOfWork<EF>();
            var result = uow.Repository<MerchandiseType>().GetAll();
            uow.Dispose();
            return result;
        }
    }
}