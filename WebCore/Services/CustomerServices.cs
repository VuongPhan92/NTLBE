using Core.PageResult;
using Data;
using Domain.IServices;
using Infrastructure.Decorator;
using Infrastructure.Queries;
using System;
using System.Collections.Generic;
using WebCore.Queries;

namespace WebCore.Services
{
    public class CustomerServices : IService<Customer>, ICustomerServices
    {
        private readonly IQueryHandler<CustomerGetAllQuery, IEnumerable<Customer>> getAllCustomerHandler;
        private readonly IQueryHandler<CustomerSearchQuery, PagedListResult<Customer>> searchCustomerdHandler;
        private readonly IQueryHandler<GetCustomerByPhoneQuery, Customer> getCustomerByPhoneHandler;
        public CustomerServices(IQueryHandler<CustomerGetAllQuery, IEnumerable<Customer>> _getAllCustomerHandler,
                  IQueryHandler<CustomerSearchQuery, PagedListResult<Customer>> _searchCustomerdHandler,
                  IQueryHandler<GetCustomerByPhoneQuery, Customer> _getCustomerByPhoneHandler
            )
        {
            getAllCustomerHandler = _getAllCustomerHandler;
            searchCustomerdHandler = _searchCustomerdHandler;
            getCustomerByPhoneHandler = _getCustomerByPhoneHandler;
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
        public Customer GetCustomerByPhone(string phone)
        {
            return getCustomerByPhoneHandler.Handle(new GetCustomerByPhoneQuery { Phone= phone });
        }
    }
}