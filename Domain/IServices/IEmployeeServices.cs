using Data;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IServices
{
    public interface IEmployeeServices
    {
        IEnumerable<Employee> GetAllEmployee();
        void AddEmployee(EmployeeVM employeeVM);
        void DeleteEmployee(int id);
    }
}
