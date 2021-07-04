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
    [RoutePrefix("api/regions")]
    public class RegionsController : ApiController
    {
        RegionBL r = new RegionBL();
            [AcceptVerbs("GET", "POST")]
            [Route("getRegions")]
            [HttpGet]
            public List<RegionModel> getRegions()
            {
            return  r.GetListRegionNames();
            }

        }
    }

