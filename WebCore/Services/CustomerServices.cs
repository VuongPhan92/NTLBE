using Core.PageResult;
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
    public class CustomerServices : IService<Customer>, ICustomerServices
    {
        private readonly IQueryHandler<GetAllCustomerQuery, IEnumerable<Customer>> getAllCustomerHandler;
        private readonly IQueryHandler<SearchCustomerQuery, PagedListResult<Customer>> searchCustomerdHandler;
        private readonly IQueryHandler<GetCustomerByIdQuery, Customer> getCustomerByIdHandler;
        private readonly ICommandHandler<AddCustomerCommand> addCustomerHandler;

        public CustomerServices(IQueryHandler<GetAllCustomerQuery, IEnumerable<Customer>> _getAllCustomerHandler,
                  IQueryHandler<SearchCustomerQuery, PagedListResult<Customer>> _searchCustomerdHandler,
                  IQueryHandler<GetCustomerByIdQuery, Customer> _getCustomerByIdHandler,
                    ICommandHandler<AddCustomerCommand> _addCustomerHandler
            )
        {
            getAllCustomerHandler = _getAllCustomerHandler;
            searchCustomerdHandler = _searchCustomerdHandler;
            addCustomerHandler = _addCustomerHandler;
            getCustomerByIdHandler = _getCustomerByIdHandler;
        }
        public IEnumerable<Customer> GetAllCustomer()
        {
            return getAllCustomerHandler.Handle(new GetAllCustomerQuery { });
        }
        public PagedListResult<Customer> SearchCustomer(SearchQuery<Customer> searchQuery)
        {
            return searchCustomerdHandler.Handle(new SearchCustomerQuery { SearchQuery = searchQuery });
        }
        public void AddCustomer(string name,string phone,string idNumber)
        {
            Customer customer = new Customer();
            customer.CustomerName = name;
            customer.CustomerPhone = phone;
            customer.CustomerIdNumber = idNumber;
            addCustomerHandler.Handle(new AddCustomerCommand {Customer= customer});       
        }
        public Customer GetCustomerById (int customerId)
        {
            return getCustomerByIdHandler.Handle(new GetCustomerByIdQuery { Id = customerId });
        }
    }
}