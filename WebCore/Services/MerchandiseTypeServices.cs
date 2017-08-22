using Data;
using Domain.IServices;
using Infrastructure.Decorator;
using Infrastructure.Queries;
using System.Collections.Generic;
using WebCore.Queries;

namespace WebCore.Services
{
    public class MerchandiseTypeServices : IService<MerchandiseType>, IMerchandiseTypeServices
    {
        private readonly IQueryHandler<GetAllMerchandiseTypeQuery, IEnumerable<MerchandiseType>> getAllMerchandiseTypeHandler;

        public MerchandiseTypeServices(
            IQueryHandler<GetAllMerchandiseTypeQuery, IEnumerable<MerchandiseType>> _getAllMerchandiseTypeHandler
        )
        {
            getAllMerchandiseTypeHandler = _getAllMerchandiseTypeHandler;
        }

        public IEnumerable<MerchandiseType> GetAllMerchandiseType()
        {
            var logger = new ActivityLogForQueryHandlerDecorator<GetAllMerchandiseTypeQuery, IEnumerable<MerchandiseType>>(getAllMerchandiseTypeHandler);
            return logger.Handle(new GetAllMerchandiseTypeQuery { });
        }
    }
}