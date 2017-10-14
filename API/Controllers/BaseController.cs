using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace API.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class BaseController : ApiController
    {
        protected HttpResponseMessage GetResponse(object obj, HttpStatusCode httpCode)
        {
            HttpResponseMessage response = Request.CreateResponse();
            response.StatusCode = httpCode;
            if (HttpStatusCode.OK.Equals(httpCode))
            {
                response.Content = new StringContent(JsonConvert.SerializeObject(obj));
            }
            else if (HttpStatusCode.NotFound.Equals(httpCode))
            {
                response.Content = new StringContent("Not Found");
            }
            else if (HttpStatusCode.NotImplemented.Equals(httpCode))
            {
                response.Content = new StringContent("Not Implemented");
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
            else if(HttpStatusCode.BadRequest.Equals(httpCode))
            {
                response.Content = new StringContent("Message not sent successfully");
            }
            else
            {
                response.Content = new StringContent("Post request encounter error");
            }
            return response;
        }
    }
}