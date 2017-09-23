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
        private readonly ICommandHandler<BranchUpdateNameCommand> updateBranchNameHandler;
        private readonly ICommandHandler<BranchAddressUpdateCommand> updateBranchAddressHandler;
        private readonly ICommandHandler<BranchPhoneUpdateCommand> updateBranchPhoneHandler;
        private readonly ICommandHandler<BranchEmailUpdateCommand> updateBranchEmailHandler;
        private readonly ICommandHandler<BranchDeleteCommand> deleteBranchHandler;
        private readonly ICommandHandler<BranchUpdateCodeCommand> updateBranchCodeHandler;

        public BranchServices(
            IQueryHandler<BranchGetAllQuery, IEnumerable<Branch>> _getAllBranchHander,
            IQueryHandler<BranchGetByIdQuery, Branch> _getBranchByIdHandler,
            ICommandHandler<BranchAddCommand> _addBranchHandler,
            ICommandHandler<BranchUpdateNameCommand> _updateBranchNameHandler,
            ICommandHandler<BranchAddressUpdateCommand> _updateBranchAddressHandler,
            ICommandHandler<BranchPhoneUpdateCommand> _updateBranchPhoneHandler,
            ICommandHandler<BranchEmailUpdateCommand> _updateBranchEmailHandler,
            ICommandHandler<BranchDeleteCommand> _deleteBranchHandler,
            ICommandHandler<BranchUpdateCodeCommand> _updateBranchCodeHandler
            )
        {
            getAllBranchHander = _getAllBranchHander;
            getBranchByIdHandler = _getBranchByIdHandler;
            addBranchHandler = _addBranchHandler;
            updateBranchNameHandler = _updateBranchNameHandler;
            updateBranchAddressHandler = _updateBranchAddressHandler;
            updateBranchPhoneHandler = _updateBranchPhoneHandler;
            updateBranchEmailHandler = _updateBranchEmailHandler;
            deleteBranchHandler = _deleteBranchHandler;
            updateBranchCodeHandler = _updateBranchCodeHandler;

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

        public void UpdateBranchName(int id, string name)
        {
            updateBranchNameHandler.Handle(new BranchUpdateNameCommand { Id = id, Name = name });
        }

        public void UpdateBranchAddress(int id, string address)
        {
            updateBranchAddressHandler.Handle(new BranchAddressUpdateCommand { Id = id, Address = address });
        }

        public void UpdateBranchPhone(int id, string phone)
        {
            updateBranchPhoneHandler.Handle(new BranchPhoneUpdateCommand { Id = id, Phone = phone });
        }

        public void UpdateBranchEmail(int id, string email)
        {
            updateBranchEmailHandler.Handle(new BranchEmailUpdateCommand { Id = id, Email = email });
        }

        public void DeleteBranch(int id)
        {
            deleteBranchHandler.Handle(new BranchDeleteCommand { Id = id });
        }

        public void UpdateBranchCode(int id, string branchCode)
        {
            updateBranchCodeHandler.Handle(new BranchUpdateCodeCommand { Id = id , BranchCode = branchCode });
        }
    }
}