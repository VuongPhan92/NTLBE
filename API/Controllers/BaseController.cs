using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class BaseController : ApiController
    {
        protected HttpResponseMessage GetResponse(object obj, HttpStatusCode httpCode,string errMessage="")
        {
            HttpResponseMessage response = Request.CreateResponse();
            response.StatusCode = httpCode;
            if (HttpStatusCode.OK.Equals(httpCode))
            {
                response.Content = new StringContent(JsonConvert.SerializeObject(obj));
            }
            else
            {
                response.Content = new StringContent(errMessage);
            }
            return response;
        }
        protected HttpResponseMessage GetResponse(HttpStatusCode httpCode, string errMassage = "")
        {
            HttpResponseMessage response = Request.CreateResponse();
            response.StatusCode = httpCode;
            if (HttpStatusCode.OK.Equals(httpCode))
            {
                response.Content = new StringContent("Get request successfully handle!");
            }
            else
            {
                response.Content = new StringContent(errMassage);
            }
            return response;
        }

        protected HttpResponseMessage PostResponse(HttpStatusCode httpCode)
        {
            HttpResponseMessage response = Request.CreateResponse();
            response.StatusCode = httpCode;
            if (HttpStatusCode.OK.Equals(httpCode))
            {
                response.Content = new StringContent("Post request successfully handle!");
            }
            else
            {
                response.Content = new StringContent("Post request encounter error");
            }
            return response;
        }
        protected HttpResponseMessage PostResponse(HttpStatusCode httpCode, string errMesssage = " ")
        {
            HttpResponseMessage response = Request.CreateResponse();
            response.StatusCode = httpCode;
            if (HttpStatusCode.OK.Equals(httpCode))
            {
                response.Content = new StringContent("Post request successfully handle!");
            }
            else
            {
                response.Content = new StringContent(errMesssage);
            }
            return response;
        }
    }
}