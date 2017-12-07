using Data;
using Infrastructure.Queries;
using Infrastructure.Repository;
using System.Collections.Generic;
using System.Linq;

namespace WebCore.Queries
{
    public class MerchandiseTypeGetAllQueryHandler : IQueryHandler<MerchandiseTypeGetAllQuery, IEnumerable<MerchandiseType>>
    {
        public IEnumerable<MerchandiseType> Handle(MerchandiseTypeGetAllQuery query)
        {
            var uow = new UnitOfWork<EF>();
            
            var result = uow.Repository<MerchandiseType>().GetAll().Where(p => !p.DeletedDate.HasValue);
            uow.Dispose();
            return result;
        }
    }
}