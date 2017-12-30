using Data;
using Domain.IServices;
using Infrastructure.Decorator;
using Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ViewModels;
using WebCore.Command;
using WebCore.Queries;

namespace WebCore.Services
{
    public class EmployeeServices : IService<Employee>, IEmployeeServices
    {
        private readonly ICommandHandler<EmployeeAddCommand> addEmployeeHandler;
        private readonly ICommandHandler<EmployeeDeleteCommand> deleteEmployeeHandler;
        private readonly IQueryHandler<EmployeeGetAllQuery, IEnumerable<Employee>> getAllEmpHandler;

        public EmployeeServices(
            ICommandHandler<EmployeeAddCommand> _addEmployeeHandler,
            ICommandHandler<EmployeeDeleteCommand> _deleteEmployeeHandler,
            IQueryHandler<EmployeeGetAllQuery, IEnumerable<Employee>> _getAllEmpHandler
        )
        {
            addEmployeeHandler = _addEmployeeHandler;
            deleteEmployeeHandler = _deleteEmployeeHandler;
            getAllEmpHandler = _getAllEmpHandler;
        }

        public void AddEmployee(EmployeeVM employeeVM)
        {
            var employee = new Employee();
            employee.EmpId = employeeVM.EmpId;
            employee.FullName = employeeVM.FullName;
            employee.DOB = employeeVM.DOB;
            employee.Address = employeeVM.Address;
            employee.Phone = employeeVM.Phone;
            addEmployeeHandler.Handle(new EmployeeAddCommand { EmployeeInfo = employee,Password=employeeVM.Password });
        }

        public void DeleteEmployee(int id)
        {
           deleteEmployeeHandler.Handle(new EmployeeDeleteCommand { Id = id });
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
           return getAllEmpHandler.Handle(new EmployeeGetAllQuery());
        }
    }
}

