using Domain.IServices;
using System;
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
                if (statusCodeList.Count()>0)
                {
                    return GetResponse(statusCodeList, HttpStatusCode.OK);
                }
                else
                {
                    return GetResponse(HttpStatusCode.NotFound ,"Cannot get all status list");
                }
            }
            catch (Exception ex)
            {
                return GetResponse(HttpStatusCode.NotImplemented, ex.Message);
            }
        }
      
    }
}
