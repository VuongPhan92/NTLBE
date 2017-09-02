using Domain.IServices;
using Domain.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
namespace API.Controllers
{
    [RoutePrefix("NgocTrang/Api/Bol")]
    public class BolController : ApiController
    {                                                                                                               

        #region constructor 4 injection

        private IBolServices iBolServices;
        private ICustomerServices iCustomerServices;
        private IMerchandiseTypeServices iMerchandiseTypeServices;
        private IBranchServices iBranchServices;
        private IDeliveryTypeServices iDeliveryTypeServices;
        private IStatusServices iStatusServices;

        public BolController(
            IBolServices _iBolServices,
            ICustomerServices _iCustomerServices,
            IMerchandiseTypeServices _iMerchandiseTypeServices,
            IBranchServices _iBranchServices,
            IDeliveryTypeServices _iDeliveryTypeServices,
            IStatusServices _iStatusServices
        )
        {
            iBolServices = _iBolServices;
            iBranchServices = _iBranchServices;
            iCustomerServices = _iCustomerServices;
            iMerchandiseTypeServices = _iMerchandiseTypeServices;
            iDeliveryTypeServices = _iDeliveryTypeServices;
            iStatusServices = _iStatusServices;
        }

        #endregion constructor 4 injection

        #region HttpResponse

        //All Request will have the same core respone,status code, the only the return values are different
        private HttpResponseMessage GetResponse(object obj, HttpStatusCode httpCode)
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
        private HttpResponseMessage PostResponse(HttpStatusCode httpCode)
        {
            HttpResponseMessage response = Request.CreateResponse();
            response.StatusCode = httpCode;
            if (HttpStatusCode.OK.Equals(httpCode))
            {
                response.Content = new StringContent("Submit Item Complete!");
            }
            else
            {
                response.Content = new StringContent("Submit Error");
            }
            return response;
        }

        #endregion HttpResponse

        #region HandleRequest
        //GET: NgocTrang/Api/Bol/GetComponent
        [Route("GetComponent")]
        [HttpGet]
        public HttpResponseMessage GetComponent()
        {
            ComponentVM vm = new ComponentVM();
            string format = "ddMMyyHHmmss";
            vm.DeliveryType = iDeliveryTypeServices.GetAllDeliveryType();
            vm.CurrentTimeStamp = DateTime.Now.ToString(format);
            vm.Branch = iBranchServices.GetAllBranches();
            vm.Type = iMerchandiseTypeServices.GetAllMerchandiseType();     
            try
            {
                if (vm.Branch != null && vm.Type != null && vm.DeliveryType!=null)
                {
                    return GetResponse(vm, HttpStatusCode.OK);
                }
                else
                {
                    return GetResponse("Cannot find components for billing services", HttpStatusCode.NotFound);
                }
            }
            catch
            {
                return GetResponse("Not implement", HttpStatusCode.NotImplemented);
            }
        }

        //GET: NgocTrang/Api/Bol/GetStatus
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

        //GET NgocTrang/Api/Bol/GetAllBol   
        [Route("GetAllBol")]
        [HttpGet]
        public HttpResponseMessage GetAllBol(string from = "", string to = "")
        {       
            try
            {
                if (iBolServices.GetAllBol().ToList().Count() >0)
                {
                    return GetResponse(iBolServices.GetAllBol(), HttpStatusCode.OK);
                }
                else
                {
                    return GetResponse("Cannot find all bills of landing", HttpStatusCode.NotFound);
                }
            }
            catch(Exception e )
            {
                return GetResponse(e.Message, HttpStatusCode.NotImplemented);
            }
        }

        //POST: NgocTrang/Api/Bol/Add
        [Route("Add")]
        [HttpPost]
        public HttpResponseMessage Add(TransactionVM obj)
        {       
            try
            {
                var customerInfo = obj.CustomerInfo;
                var billInfo = obj.BillOfLandingInfo;               
                //Create new Bol for customer to confirm Transaction
                if(billInfo.ReceiveTime==null)
                {
                    billInfo.ReceiveTime = "00:00:00";
                }
                iBolServices.CreateNewBol(obj.BillOfLandingInfo,obj.CustomerInfo);           
                return PostResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                    return PostResponse(HttpStatusCode.NotAcceptable);
            }
        }
        #endregion HandleRequest
        //POST: NgocTrang/Api/Bol/UpdateStatus
        [Route("UpdateStatus")]
        [HttpPost]
        public HttpResponseMessage UpdateStatus(int bolId)
        {
            try
            {
                iBolServices.UpdateStatus(bolId);
                return PostResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return PostResponse(HttpStatusCode.NotAcceptable);
            }
        }
        #region Glue code
        private string CreateMerchandiseId(string input,int index, int total)
        {
            return input + "-" + index+1 + "/" + total;
        }
        #endregion Glue code
    }
}