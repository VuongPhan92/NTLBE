using Domain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("NgocTrang/Api/DeliveryType")]
    public class DeliveryTypeController : BaseController
    {
        private IDeliveryTypeServices iDeliveryTypeServices;
        public DeliveryTypeController(IDeliveryTypeServices _iDeliveryTypeServices)
        {
            iDeliveryTypeServices = _iDeliveryTypeServices;
        }

        [Route("GetAll")]
        [HttpGet]
        public HttpResponseMessage GetAllDeliveryType()
        {
            try
            {
                iDeliveryTypeServices.GetAllDeliveryType();
                return GetResponse(iDeliveryTypeServices.GetAllDeliveryType(), HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return PostResponse(HttpStatusCode.NotAcceptable);
            }

        }
    }
}
