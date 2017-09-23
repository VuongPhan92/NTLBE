using Domain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("NgocTrang/Api/Status")]
    public class StatusController : BaseController
    {
        private IStatusServices iStatusServices;
        public StatusController(IStatusServices _iStatusServices)
        {
            iStatusServices = _iStatusServices;
        }

        //GET: NgocTrang/Api/Status/GetStatus
        [Route("GetStatus")]
        [HttpGet]
        public HttpResponseMessage GetStatus()
        {
            var statusCodeList = iStatusServices.GetAllStatusCode();
            try
            {
                if (statusCodeList != null)
                {
                    return GetResponse(statusCodeList, HttpStatusCode.OK);
                }
                else
                {
                    return GetResponse("Cannot get Status codes", HttpStatusCode.NotFound);
                }
            }
            catch
            {
                return GetResponse("Not implement", HttpStatusCode.NotImplemented);
            }
        }
     
    }
}
