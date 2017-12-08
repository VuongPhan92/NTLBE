using Data;
using Domain.ViewModels;
using System.Collections.Generic;

namespace Domain.IServices
{
    public interface IEmployeeServices
    {
        IEnumerable<Employee> GetAllEmployee();
        void AddEmployee(EmployeeVM employeeVM);
        void DeleteEmployee(int id);
    }
}
