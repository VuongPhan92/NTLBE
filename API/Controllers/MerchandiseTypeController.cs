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
                iMerchandiseTypeServices.GetAllMerchandiseType();
                return GetResponse(iMerchandiseTypeServices.GetAllMerchandiseType(), HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return PostResponse(HttpStatusCode.NotAcceptable);
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
            catch (Exception)
            {
                return PostResponse(HttpStatusCode.NotAcceptable);
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
            catch (Exception)
            {
                return PostResponse(HttpStatusCode.NotAcceptable);
            }

        }
    }
}
