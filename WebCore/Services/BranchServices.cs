using Data;
using Domain.IServices;
using Infrastructure.Decorator;
using Infrastructure.Queries;
using System.Collections.Generic;
using WebCore.Queries;

namespace WebCore.Services
{
    public class BranchServices : IService<Branch>, IBranchServices
    {
        private readonly IQueryHandler<BranchGetAllQuery, IEnumerable<Branch>> getAllBranchHander;
        private readonly IQueryHandler<BranchGetByIdQuery, Branch> getBranchByIdHandler;
        public BranchServices(
            IQueryHandler<BranchGetAllQuery, IEnumerable<Branch>> _getAllBranchHander,
            IQueryHandler<BranchGetByIdQuery, Branch> _getBranchByIdHandler
            )
        {
            getAllBranchHander = _getAllBranchHander;
            getBranchByIdHandler = _getBranchByIdHandler;
        }

        public IEnumerable<Branch> GetAllBranches()
        {
            //var logger = new ActivityLogForQueryHandlerDecorator<GetAllBranchQuery, IEnumerable<Branch>>(getAllBranchHander);
            return getAllBranchHander.Handle(new BranchGetAllQuery { });
        }
        public Branch GetBranchById(int id) {
            return getBranchByIdHandler.Handle(new BranchGetByIdQuery { Id = id });
        }
    }
}