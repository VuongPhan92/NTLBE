using Domain.IServices;
using Domain.ViewModels;
using System;
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
                var branches = iBranchServices.GetAllBranches();
                if (branches != null)
                {
                    return GetResponse(iBranchServices.GetAllBranches(), HttpStatusCode.OK);
                }
                else
                {
                    return GetResponse(HttpStatusCode.ExpectationFailed, "Cannot get all branches");
                }
            }
            catch (Exception ex)
            {
                return GetResponse(HttpStatusCode.ExpectationFailed, ex.Message);
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
            catch (Exception ex)
            {
                return PostResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        //POST: NgocTrang/Api/Bol/UpdateName/1/ABC
        [Route("UpdateName/{id}/{name}")]
        [HttpPost]
        public HttpResponseMessage UpdateBranchName(int id, string name)
        {
            try
            {
                iBranchServices.UpdateBranchName(id, name);
                return PostResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return PostResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        //POST: NgocTrang/Api/Bol/UpdateAddress/1/address
        [Route("UpdateAddress/{id}/{address}")]
        [HttpPost]
        public HttpResponseMessage UpdateBranchAddress(int id, string address)
        {
            try
            {
                iBranchServices.UpdateBranchAddress(id, address);
                return PostResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return PostResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        //POST: NgocTrang/Api/Bol/UpdatePhone/1/012345
        [Route("UpdatePhone/{id}/{phone}")]
        [HttpPost]
        public HttpResponseMessage UpdateBranchPhone(int id, string phone)
        {
            try
            {
                iBranchServices.UpdateBranchName(id, phone);
                return PostResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return PostResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        //POST: NgocTrang/Api/Bol/UpdateEmail/1/abc@gxyz.com
        [Route("UpdateEmail/{id}/{email}")]
        [HttpPost]
        public HttpResponseMessage UpdateBranchEmail(int id, string email)
        {
            try
            {
                iBranchServices.UpdateBranchName(id, email);
                return PostResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return PostResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        //POST: NgocTrang/Api/Bol/UpdateCode
        [Route("UpdateCode/{id}/{branchCode}")]
        [HttpPost]
        public HttpResponseMessage UpdateBranchCode(int id, string branchCode)
        {
            try
            {
                iBranchServices.UpdateBranchCode(id, branchCode);
                return PostResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return PostResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        //POST: NgocTrang/Api/Bol/Delete
        [Route("Delete/{id}")]
        [HttpPost]
        public HttpResponseMessage DeleteBranch(int id)
        {
            try
            {
                iBranchServices.DeleteBranch(id);
                return PostResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return PostResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }
    }
}
