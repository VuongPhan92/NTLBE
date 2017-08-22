using Core.PageResult;
using Data;
using Domain.ViewModels;
using System.Collections.Generic;

namespace Domain.IServices
{
    public interface ICustomerServices
    {
        IEnumerable<Customer> GetAllCustomer();
        PagedListResult<Customer> SearchCustomer(SearchQuery<Customer> searchQuery);
        void AddCustomer(string name,string phone,string idNumber);
        Customer GetCustomerById(int id);
    
    }
}