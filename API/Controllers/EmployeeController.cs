using Domain.IServices;
using Domain.ViewModels;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("NgocTrang/Api/Employee")]
    public class EmployeeController : BaseController
    {
        private IEmployeeServices iEmployeeServices;
        public EmployeeController(IEmployeeServices _iEmployeeServices)
        {
            iEmployeeServices = _iEmployeeServices;
        }

        //GET: NgocTrang/Api/Employee/GetAll
        [Route("GetAll")]
        [HttpGet]
        public HttpResponseMessage GetAllEmployee()
        {
            try
            {
                var employeeInfo = iEmployeeServices.GetAllEmployee();
                if(employeeInfo!=null)
                {
                    return GetResponse(employeeInfo, HttpStatusCode.OK);
                }
                else
                {
                    return GetResponse(HttpStatusCode.NotFound,"Cannot get all employee");
                }
            }
            catch (Exception ex)
            {
                return GetResponse(HttpStatusCode.ExpectationFailed,ex.Message);
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
            catch (Exception ex)
            {
                return PostResponse(HttpStatusCode.ExpectationFailed,ex.Message);
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
            catch (Exception ex)
            {
                return PostResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }
    }
}
