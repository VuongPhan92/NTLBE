using Domain.IServices;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("NgocTrang/Api/Branch")]
    public class BranchController : BaseController
    {
        private IBranchServices iBranchServices;
        public BranchController(IBranchServices _iBranchServices)
        {
            iBranchServices = _iBranchServices;
        }


        //GET: NgocTrang/Api/Bol/GetBranches
        [Route("GetAll")]
        [HttpGet]
        public HttpResponseMessage GetAllBranch()
        {
            try
            {
                iBranchServices.GetAllBranches();
                return GetResponse(iBranchServices.GetAllBranches(), HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return PostResponse(HttpStatusCode.NotAcceptable);
            }

        }

        //POST: NgocTrang/Api/Bol/Add
        [Route("Add")]
        [HttpPost]
        public HttpResponseMessage AddBranch(BranchVM branchVM)
        {
            try
            {
                iBranchServices.AddBranch(branchVM);
                return PostResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return PostResponse(HttpStatusCode.NotAcceptable);
            }

        }

        //POST: NgocTrang/Api/Bol/UpdateName
        [Route("UpdateName")]
        [HttpPost]
        public HttpResponseMessage UpdateBranchName(int id, string name)
        {
            try
            {
                iBranchServices.UpdateBranchName(id, name);
                return PostResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {

                return PostResponse(HttpStatusCode.NotAcceptable);
            }
        }

        //POST: NgocTrang/Api/Bol/UpdateAddress
        [Route("UpdateAddress")]
        [HttpPost]
        public HttpResponseMessage UpdateBranchAddress(int id, string address)
        {
            try
            {
                iBranchServices.UpdateBranchAddress(id, address);
                return PostResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {

                return PostResponse(HttpStatusCode.NotAcceptable);
            }
        }

        //POST: NgocTrang/Api/Bol/UpdatePhone
        [Route("UpdatePhone")]
        [HttpPost]
        public HttpResponseMessage UpdateBranchPhone(int id, string phone)
        {
            try
            {
                iBranchServices.UpdateBranchName(id, phone);
                return PostResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {

                return PostResponse(HttpStatusCode.NotAcceptable);
            }
        }

        //POST: NgocTrang/Api/Bol/UpdateEmail
        [Route("UpdateEmail")]
        [HttpPost]
        public HttpResponseMessage UpdateBranchEmail(int id, string email)
        {
            try
            {
                iBranchServices.UpdateBranchName(id, email);
                return PostResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {

                return PostResponse(HttpStatusCode.NotAcceptable);
            }
        }

        //POST: NgocTrang/Api/Bol/UpdateCode
        [Route("UpdateCode")]
        [HttpPost]
        public HttpResponseMessage UpdateBranchCode(int id, string branchCode)
        {
            try
            {
                iBranchServices.UpdateBranchCode(id, branchCode);
                return PostResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {

                return PostResponse(HttpStatusCode.NotAcceptable);
            }
        }

        //POST: NgocTrang/Api/Bol/Delete
        [Route("Delete")]
        [HttpPost]
        public HttpResponseMessage DeleteBranch(int id)
        {
            try
            {
                iBranchServices.DeleteBranch(id);
                return PostResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {

                return PostResponse(HttpStatusCode.NotAcceptable);
            }
        }

    }
}
