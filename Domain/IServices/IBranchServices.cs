using Data;
using System.Collections.Generic;

namespace Domain.IServices
{
    public interface IBranchServices
    {
        IEnumerable<Branch> GetAllBranches();
        Branch GetBranchById(int id);
    }
}
