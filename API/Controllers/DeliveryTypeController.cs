using Domain.IServices;
using System;
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
                var deliveryTypes = iDeliveryTypeServices.GetAllDeliveryType();
                if(deliveryTypes.Count() >0)
                {
                    return GetResponse(deliveryTypes, HttpStatusCode.OK);
                }
                else
                {
                    return GetResponse(HttpStatusCode.NotFound, "Cannot get all delivery types");
                }
            }
            catch (Exception ex)
            {
                return GetResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }

        }
    }
}
