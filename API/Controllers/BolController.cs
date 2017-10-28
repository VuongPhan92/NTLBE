using Domain.IServices;
using Domain.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using System.Collections.Generic;
using Domain.Utility;

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
            vm.DeliveryType = iDeliveryTypeServices.GetAllDeliveryType();
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

        //GET: NgocTrang/Api/Bol/GetCurrentTimeStamp
        [Route("GetCurrentTimeStamp")]
        [HttpGet]
        public string GetCurrentTimeStamp()
        {
            string format = "ddMMyyHHmmss";
            var currentTimeStamp = DateTime.Now.ToString(format);
            return currentTimeStamp;
        }
       
        //GET NgocTrang/Api/Bol/GetAllBol   
        [Route("GetAllBol")]
        [HttpGet]
        public HttpResponseMessage GetAllBol(string conditionQuery)
        {       
            try
            {
                var bolList = iBolServices.GetAllBol(conditionQuery).ToList();
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
                var statusList = iStatusServices.GetAllStatusCode().ToList();
                var data = bolList.ToList();
                foreach (var bol in data)
                {
                    foreach (var statusCode in statusList)
                    {
                        if(bol.StatusCode == statusCode.Id)
                        {
                            bol.Status = statusCode;
                        }
                    }
                }
                return GetResponse(data, HttpStatusCode.OK);
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
                SendNotificationSMS(obj.BillOfLandingInfo.BolCode, obj.BillOfLandingInfo.Contact);          
                return PostResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                    return PostResponse(HttpStatusCode.NotAcceptable);
            }
        }

        //POST: NgocTrang/Api/Bol/UpdateStatus
        [Route("UpdateStatus/{bolId}")]
        [HttpPost]
        public HttpResponseMessage UpdateStatus(int bolId)
        {
            try
            {
                iBolServices.UpdateStatus(bolId);
                
                var isSuccess = SendDeliverySMS(bolId);
                if(isSuccess)
                {
                    return PostResponse(HttpStatusCode.OK);
                }
                return PostResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception)
            {
                return PostResponse(HttpStatusCode.NotAcceptable);
            }
        }

        [Route("UpdateStatusByCode")]
        [HttpPost]
        public HttpResponseMessage UpdateStatusByCode(string bolCode)
        {
            try
            {
                iBolServices.UpdateStatusByBolCode(bolCode);
                var bolId = iBolServices.GetBolByBolCode(bolCode).Id;
                var isSuccess = SendDeliverySMS(bolId);
                if (isSuccess)
                {
                    return PostResponse(HttpStatusCode.OK);
                }
                return PostResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception)
            {
                return PostResponse(HttpStatusCode.NotAcceptable);
            }
        }

        public bool SendNotificationSMS(string bolId,string contact)
        {
            try
            {
                var api = new SpeedSMSAPI("sGiQYd49IWyS2Wvs1YmKLnlsp_8tI8hy");
                var userInfo = api.getUserInfo();
                if (string.IsNullOrEmpty(userInfo))
                {
                    return false;
                }
                var contactArr = new string[] { contact };
                var msg = $"Cam on qui khach da su dung dich vu cua Ngoc Trang, don hang so {bolId} da duoc tao thanh cong";
                api.sendSMS(contactArr, msg, 2, "");
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        public bool SendDeliverySMS(int id)
        {
            try
            {
                var data = iBolServices.GetBolById(id);
                var api = new SpeedSMSAPI("sGiQYd49IWyS2Wvs1YmKLnlsp_8tI8hy");
                if (data.StatusCode.Value == 4)
                {
                    var userInfo = api.getUserInfo();
                    if (string.IsNullOrEmpty(userInfo))
                    {
                        return false;
                    }
                    //sms
                    var customMsg = " khi di dem theo GPLX/CMND, moi thac mac xin vui long goi so dien thoai: ";
                    var branchData = data.Branches.Where(p => p.Id != data.Start).Select(p => new SMScontent
                    {
                        Address = p.Address,
                        Phone = p.Phone
                    }).SingleOrDefault();
                    var smsContent = data.Status.StatusDescription + " " + ConvertVie.Convert(branchData.Address) + customMsg + branchData.Phone;
                    var phones = data.Customers.Select(p => p.Phone).ToArray();
                    api.sendSMS(phones, smsContent, 2, "");
                }
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }

    public class SMScontent
    {
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}