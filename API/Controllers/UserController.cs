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
        public HttpResponseMessage Login(string username, string password)
        {
            iAccountServices.ValidateAccount(username, password);
            return PostResponse(HttpStatusCode.OK);
        }
    }
}
