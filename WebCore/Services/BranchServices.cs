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
    public class BranchServices : IService<Branch>, IBranchServices
    {
        private readonly IQueryHandler<BranchGetAllQuery, IEnumerable<Branch>> getAllBranchHander;
        private readonly IQueryHandler<BranchGetByIdQuery, Branch> getBranchByIdHandler;
        private readonly ICommandHandler<BranchAddCommand> addBranchHandler;
        public BranchServices(
            IQueryHandler<BranchGetAllQuery, IEnumerable<Branch>> _getAllBranchHander,
            IQueryHandler<BranchGetByIdQuery, Branch> _getBranchByIdHandler,
            ICommandHandler<BranchAddCommand> _addBranchHandler
            )
        {
            getAllBranchHander = _getAllBranchHander;
            getBranchByIdHandler = _getBranchByIdHandler;
            addBranchHandler = _addBranchHandler;
        }

        public IEnumerable<Branch> GetAllBranches()
        {
            //var logger = new ActivityLogForQueryHandlerDecorator<GetAllBranchQuery, IEnumerable<Branch>>(getAllBranchHander);
            return getAllBranchHander.Handle(new BranchGetAllQuery { });
        }
        public Branch GetBranchById(int id) {
            return getBranchByIdHandler.Handle(new BranchGetByIdQuery { Id = id });
        }
        public void AddBranch(BranchVM branchVM)
        {
            var branch = new Branch();
            branch.Name = branchVM.Name;
            branch.Address = branchVM.Address;
            branch.BranchCode = branchVM.BranchCode;
            branch.Email = branchVM.Email;
            branch.Phone = branchVM.Phone;
            branch.Description = branchVM.Description;
            //cache , decor, log
            addBranchHandler.Handle(new BranchAddCommand { Branch = branch });
        }
    }
}