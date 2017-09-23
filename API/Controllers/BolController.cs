using Domain.IServices;
using Domain.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using System.Collections.Generic;

namespace API.Controllers
{
    [RoutePrefix("NgocTrang/Api/Bol")]
    public class BolController : BaseController
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
            IStatusServices _iStatusServices)
        
        {
            iBolServices = _iBolServices;
            iBranchServices = _iBranchServices;
            iCustomerServices = _iCustomerServices;
            iMerchandiseTypeServices = _iMerchandiseTypeServices;
            iDeliveryTypeServices = _iDeliveryTypeServices;
            iStatusServices = _iStatusServices;
        }

        #endregion constructor 4 injection
 
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
       
        //GET NgocTrang/Api/Bol/GetAllBol   
        [Route("GetAllBol")]
        [HttpGet]
        public HttpResponseMessage GetAllBol()
        {       
            try
            {
                var bolList = iBolServices.GetAllBol();
                foreach(var item in bolList)
                {
                    foreach(var sub in item.Branches)
                    {
                        sub.BillOfLandings = null;
                    }
                    foreach (var sub in item.Customers)
                    {
                        sub.BillOfLandings = null;
                    }
                }
                if (bolList.Count() >0)
                {
                    var test = bolList.ToList();
                    return GetResponse(test, HttpStatusCode.OK);
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

        //POST: NgocTrang/Api/Bol/UpdateStatus
        [Route("UpdateStatus")]
        [HttpPost]
        public HttpResponseMessage UpdateStatus(int bolId)
        {
            try
            {
                iBolServices.UpdateStatus(bolId);
                var temp = bolId;
                return PostResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return PostResponse(HttpStatusCode.NotAcceptable);
            }
        }
    





    }
}