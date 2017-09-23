using Data;
using Domain.ViewModels;
using System.Collections.Generic;

namespace Domain.IServices
{
    public interface IBranchServices
    {
        IEnumerable<Branch> GetAllBranches();
        Branch GetBranchById(int id);
        void AddBranch(BranchVM branchVM);
        void UpdateBranchName(int id, string name);
        void UpdateBranchAddress(int id, string address);
        void UpdateBranchPhone(int id, string phone);
        void UpdateBranchEmail(int id, string email);
        void DeleteBranch(int id);
        void UpdateBranchCode(int id, string branchCode);
       
    }
}
