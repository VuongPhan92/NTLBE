using Domain.IServices;
using Domain.ViewModels;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("NgocTrang/Api/User")]
    public class UserController : BaseController
    {
        private IAccountServices iAccountServices;
        public UserController(IAccountServices _iAccountServices)
        {
            iAccountServices = _iAccountServices;
        }

        //POST: NgocTrang/Api/User/Login
        [Route("Login")]
        [HttpPost]
        public HttpResponseMessage Login(UserViewModel credential)
        {
            try
            {
                var userInfo = iAccountServices.ValidateAccount(credential.UserName, credential.Password);
                if(userInfo!=null)
                {
                    var employee = new UserViewModel();
                    employee.Id = userInfo.Id.ToString();
                    employee.FullName = userInfo.Username;
                    employee.Role = userInfo.Role;
                    return PostResponse(employee, HttpStatusCode.OK);
                }
                else
                {
                    return PostResponse(HttpStatusCode.NotFound, "Cannot found user with such credentials");
                }
            }
           
            catch(Exception ex)
            {
                return PostResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }
    }
}
