using Domain.IServices;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("NgocTrang/Api/Employee")]
    public class EmployeeController : BaseController
    {
        private IAccountServices iAccountServices;
        private IEmployeeServices iEmployeeServices;
        public EmployeeController(IEmployeeServices _iEmployeeServices, IAccountServices _iAccountServices)
        {
            iEmployeeServices = _iEmployeeServices;
            iAccountServices = _iAccountServices;
        }

        //GET: NgocTrang/Api/Employee/GetAll
        [Route("GetAll")]
        [HttpGet]
        public HttpResponseMessage GetAllEmployee()
        {
            try
            {
                var employeeInfo = iEmployeeServices.GetAllEmployee();
                return GetResponse(employeeInfo, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return PostResponse(HttpStatusCode.NotAcceptable);
            }
        }

        //POST: NgocTrang/Api/Employee/AddEmployee
        [Route("AddEmployee")]
        [HttpPost]
        public HttpResponseMessage AddEmployee(EmployeeVM employeeVm)
        {
            try
            {
                iEmployeeServices.AddEmployee(employeeVm);
                
                return PostResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return PostResponse(HttpStatusCode.NotAcceptable);
            }
        }

        //POST: NgocTrang/Api/Employee/DeleteEmployee
        [Route("DeleteEmployee")]
        [HttpPost]
        public HttpResponseMessage DeleteEmployee(int empId)
        {
            try
            {
                iEmployeeServices.DeleteEmployee(empId);
                return PostResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return PostResponse(HttpStatusCode.NotAcceptable);
            }
        }
    }
}
