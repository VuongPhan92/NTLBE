using Data;
using Domain.IServices;
using Domain.ViewModels;
using Infrastructure.Decorator;
using Infrastructure.Queries;
using System.Collections.Generic;
using WebCore.Command;
using WebCore.Queries;

namespace WebCore.Services
{
    public class MerchandiseTypeServices : IService<MerchandiseType>, IMerchandiseTypeServices
    {
        private readonly IQueryHandler<MerchandiseTypeGetAllQuery, IEnumerable<MerchandiseType>> getAllMerchandiseTypeHandler;
        private readonly ICommandHandler<MerchandiseTypeAddCommand> addMerchandiseTypeHandler;
        private readonly ICommandHandler<MerchandiseTypeDeleteCommand> deleteMerchandiseTypeHandler;
    
        public MerchandiseTypeServices(
            IQueryHandler<MerchandiseTypeGetAllQuery, IEnumerable<MerchandiseType>> _getAllMerchandiseTypeHandler,
            ICommandHandler<MerchandiseTypeAddCommand> _addMerchandiseTypeHandler,
            ICommandHandler<MerchandiseTypeDeleteCommand> _deleteMerchandiseTypeHandler
        )
        {
            getAllMerchandiseTypeHandler = _getAllMerchandiseTypeHandler;
            addMerchandiseTypeHandler = _addMerchandiseTypeHandler;
            deleteMerchandiseTypeHandler = _deleteMerchandiseTypeHandler;
          
    }

        public IEnumerable<MerchandiseType> GetAllMerchandiseType()
        {
            return getAllMerchandiseTypeHandler.Handle(new MerchandiseTypeGetAllQuery { });
        }

        public void AddMerchandise(MerchandiseTypeVM merchandiseTypeVM)
        {
            var merchandiseType = new MerchandiseType();
            merchandiseType.MerchandiseType1 = merchandiseTypeVM.MerchandiseType1;
            merchandiseType.CalculationUnit = merchandiseTypeVM.CalculationUnit;
            merchandiseType.Description = merchandiseTypeVM.Description;
            addMerchandiseTypeHandler.Handle(new MerchandiseTypeAddCommand { MerchandiseType = merchandiseType });
        }

        public void DeleteMerchandise(int id)
        {

            deleteMerchandiseTypeHandler.Handle(new MerchandiseTypeDeleteCommand { Id = id });
        }
    }
}