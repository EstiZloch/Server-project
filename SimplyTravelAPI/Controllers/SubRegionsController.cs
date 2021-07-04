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
    [RoutePrefix("api/subRegions")]
    public class SubRegionsController : ApiController
    {
        SubRegionBL r = new SubRegionBL();
        RegionBL region = new RegionBL();
        [AcceptVerbs("GET", "POST")]
        [Route("getSubRegions/{name}")]
        [HttpGet]
        public List<Sub_RegionModel> getSubRegions(string name)
        {
            int code = region.GetRegionByName(name).CodeRegion;
            return r.GetListSubRegionByCode(code);
        }

    }
}
