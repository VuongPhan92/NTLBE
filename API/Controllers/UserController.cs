using Domain;
using Domain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
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
                if (userInfo != null)
                {
                    var harcodeEmp = new UserViewModel();
                    harcodeEmp.Id = userInfo.Id.ToString();
                    harcodeEmp.UserName = userInfo.Username;
                    harcodeEmp.FullName = userInfo.Username;
                    harcodeEmp.Role = "Emp";
                    return GetResponse(harcodeEmp, HttpStatusCode.OK);
                }
                else
                {
                    return PostResponse(HttpStatusCode.ExpectationFailed);
                }
            }
            catch(Exception ex)
            {
                return PostResponse(HttpStatusCode.ExpectationFailed);
            }
        }
    }
}
