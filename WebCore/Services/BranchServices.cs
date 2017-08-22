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
        private readonly IQueryHandler<GetAllBranchQuery, IEnumerable<Branch>> getAllBranchHander;
        private readonly IQueryHandler<GetBranchByIdQuery, Branch> getBranchByIdHandler;
        public BranchServices(
            IQueryHandler<GetAllBranchQuery, IEnumerable<Branch>> _getAllBranchHander,
            IQueryHandler<GetBranchByIdQuery, Branch> _getBranchByIdHandler
            )
        {
            getAllBranchHander = _getAllBranchHander;
            getBranchByIdHandler = _getBranchByIdHandler;
        }

        public IEnumerable<Branch> GetAllBranches()
        {
            //var logger = new ActivityLogForQueryHandlerDecorator<GetAllBranchQuery, IEnumerable<Branch>>(getAllBranchHander);
            return getAllBranchHander.Handle(new GetAllBranchQuery { });
        }
        public Branch GetBranchById(int id) {
            return getBranchByIdHandler.Handle(new GetBranchByIdQuery { Id = id });
        }
    }
}