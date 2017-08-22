using Data;
using Domain.ViewModels;
using System.Collections.Generic;

namespace Domain.IServices
{
    public interface IBolServices
    {
        IEnumerable<BillOfLanding> GetAllBol();
        void CreateNewBol(BolVM command,CustomerVM subComamnd);
        List<Customer> ValidateCustomerInfo(BillOfLanding bol, CustomerVM subComamnd);
    }
}