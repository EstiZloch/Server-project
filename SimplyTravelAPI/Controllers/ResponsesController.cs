using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SimpltyTravelBLL;
using Models;
namespace SimplyTravelGui.Controllers
{
    [RoutePrefix("api/Responses")]
    public class ResponsesController : ApiController
    {
        ResponseBL rBl = new ResponseBL();
        [AcceptVerbs("GET", "POST")]
        [Route("getResponseForTrip/{code}/{id}")]
        [HttpGet]
        public List<ResponseModel> getResponseForTrip(int code,int id)
        {
            return rBl.GetResponsesByCodeAndId(code, id);
        }
        [AcceptVerbs("GET", "POST")]
        [Route("Update")]
        [HttpPost]
        public int Update(ResponseModel c)
        {
            return rBl.UpdateResponse(c);
        }
    }
}
