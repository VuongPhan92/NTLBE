using Domain.IServices;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("NgocTrang/Api/Statistic")]
    public class StatisticController : BaseController
    {
        private readonly IStatisticServices iStatisticServices;
        public StatisticController(IStatisticServices _iStatisticServices)
        {
            iStatisticServices = _iStatisticServices;
        }
        [Route("GetBolData")]
        [HttpGet]
        public HttpResponseMessage GetBolBarChartData()
        {
            var data = iStatisticServices.GetBolOnDayBarChartData();
            try
            {
                if (data != null)
                {
                    return GetResponse(data, HttpStatusCode.OK);
                }
                else
                {
                    return GetResponse("No data", HttpStatusCode.NotFound);
                }
            }
            catch
            {
                return GetResponse("Not implement", HttpStatusCode.NotImplemented);
            }
        }

        [Route("GetBolOnLocationData")]
        [HttpGet]
        public HttpResponseMessage GetBolOnLocationData()
        {
            var data = iStatisticServices.GetBolOnLocationData();
            try
            {
                if (data != null)
                {
                    return GetResponse(data, HttpStatusCode.OK);
                }
                else
                {
                    return GetResponse("No data", HttpStatusCode.NotFound);
                }
            }
            catch (Exception)
            {

                return GetResponse("Not implement", HttpStatusCode.NotImplemented);
            }
        }

        [Route("GetMerchandiseOnLocationData")]
        [HttpGet]
        public HttpResponseMessage GetMerchandiseOnLocationData()
        {
            var data = iStatisticServices.GetMerchandiseOnLocationData();
            try
            {
                if (data != null)
                {
                    return GetResponse(data, HttpStatusCode.OK);
                }
                else
                {
                    return GetResponse("No data", HttpStatusCode.NotFound);
                }
            }
            catch (Exception)
            {

                return GetResponse("Not implement", HttpStatusCode.NotImplemented);
            }
        }
    }
}
