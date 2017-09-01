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
        private readonly IQueryHandler<MerchandiseTypeGetAllQuery, IEnumerable<MerchandiseType>> getAllMerchandiseTypeHandler;

        public MerchandiseTypeServices(
            IQueryHandler<MerchandiseTypeGetAllQuery, IEnumerable<MerchandiseType>> _getAllMerchandiseTypeHandler
        )
        {
            getAllMerchandiseTypeHandler = _getAllMerchandiseTypeHandler;
        }

        public IEnumerable<MerchandiseType> GetAllMerchandiseType()
        {
            return getAllMerchandiseTypeHandler.Handle(new MerchandiseTypeGetAllQuery { });
        }
    }
}