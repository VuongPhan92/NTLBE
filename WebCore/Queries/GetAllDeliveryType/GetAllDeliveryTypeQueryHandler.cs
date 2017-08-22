using Data;
using Infrastructure.Queries;
using Infrastructure.Repository;
using System.Collections.Generic;

namespace WebCore.Queries
{
    public class GetAllDeliveryTypeQueryHandler : IQueryHandler<GetAllDeliveryTypeQuery, IEnumerable<DeliveryType>>
    {
        public IEnumerable<DeliveryType> Handle(GetAllDeliveryTypeQuery query)
        {
            var uow = new UnitOfWork<EF>();
            var result = uow.Repository<DeliveryType>().GetAll();
            uow.Dispose();
            return result;             
        }
    }
}