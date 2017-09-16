using Data;
using Infrastructure.Queries;
using Infrastructure.Repository;
using System.Collections.Generic;
using System.Linq;

namespace WebCore.Queries
{
    public class DeliveryTypeGetAllQueryHandler : IQueryHandler<DeliveryTypeGetAllQuery, IEnumerable<DeliveryType>>
    {
        public IEnumerable<DeliveryType> Handle(DeliveryTypeGetAllQuery query)
        {
            var uow = new UnitOfWork<EF>();
            var result = uow.Repository<DeliveryType>().GetAll().Where(p => !p.DeletedDate.HasValue);
            uow.Dispose();
            return result;             
        }
    }
}