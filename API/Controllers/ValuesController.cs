using Domain.IServices;
using System.Web.Http;

namespace API.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }
        private IBolServices iBolServices;
        public ValuesController(IBolServices _a)
        {
            iBolServices = _a;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }

    public class Value
    {
        
        private IBolServices iBolServices;
        public Value(IBolServices _a)
        {
            iBolServices = _a;
        }

    }
}