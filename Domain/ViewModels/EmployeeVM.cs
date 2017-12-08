using System;

namespace Domain.ViewModels
{
    public class EmployeeVM
    {
        public int EmpId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime DOB { get; set; }

    }
}
