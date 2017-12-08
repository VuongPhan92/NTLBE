using Domain.IServices;
using Domain.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("NgocTrang/Api/MerchandiseType")]
    public class MerchandiseTypeController : BaseController
    {
        private IMerchandiseTypeServices iMerchandiseTypeServices;
        public MerchandiseTypeController(IMerchandiseTypeServices _iMerchandiseTypeServices)
        {
            iMerchandiseTypeServices = _iMerchandiseTypeServices;
        }

        //GET: NgocTrang/Api/MerchandiseType/GetAll
        [Route("GetAll")]
        [HttpGet]
        public HttpResponseMessage GetAllMerchandise()
        {
            try
            {
                var merchandiseType = iMerchandiseTypeServices.GetAllMerchandiseType();
                if(merchandiseType.Count() >0)
                {
                    return GetResponse(merchandiseType, HttpStatusCode.OK);
                }
                else
                {
                    return GetResponse(HttpStatusCode.NotFound,"Cannot get all merchandise types");
                }
            }
            catch (Exception ex)
            {
                return GetResponse(HttpStatusCode.ExpectationFailed,ex.Message);
            }

        }

        //POST: NgocTrang/Api/MerchandiseType/Add
        [Route("Add")]
        [HttpPost]
        public HttpResponseMessage Add(MerchandiseTypeVM merchandiseTypeVM)
        {
            try
            {
                iMerchandiseTypeServices.AddMerchandise(merchandiseTypeVM);
                return PostResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return PostResponse(HttpStatusCode.ExpectationFailed,ex.Message);
            }
        }

        //POST: NgocTrang/Api/MerchandiseType/Delete/id
        [Route("Delete/{id}")]
        [HttpPost]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                iMerchandiseTypeServices.DeleteMerchandise(id);
                return PostResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return PostResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }
    }
}
