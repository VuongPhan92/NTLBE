using Core.PageResult;
using Data;
using Domain.IServices;
using Domain.ViewModels;
using Infrastructure.Decorator;
using Infrastructure.Queries;
using System.Collections.Generic;
using WebCore.Command;
using WebCore.Queries;
using System;

namespace WebCore.Services
{
    public class CustomerServices : IService<Customer>, ICustomerServices
    {
        private readonly IQueryHandler<CustomerGetAllQuery, IEnumerable<Customer>> getAllCustomerHandler;
        private readonly IQueryHandler<CustomerSearchQuery, PagedListResult<Customer>> searchCustomerdHandler;

        public CustomerServices(IQueryHandler<CustomerGetAllQuery, IEnumerable<Customer>> _getAllCustomerHandler,
                  IQueryHandler<CustomerSearchQuery, PagedListResult<Customer>> _searchCustomerdHandler
            )
        {
            getAllCustomerHandler = _getAllCustomerHandler;
            searchCustomerdHandler = _searchCustomerdHandler;
        }

        public void AddCustomer(string name, string phone, string idNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAllCustomer()
        {
            return getAllCustomerHandler.Handle(new CustomerGetAllQuery { });
        }
        public PagedListResult<Customer> SearchCustomer(SearchQuery<Customer> searchQuery)
        {
            return searchCustomerdHandler.Handle(new CustomerSearchQuery { SearchQuery = searchQuery });
        }
      
       
    }
}