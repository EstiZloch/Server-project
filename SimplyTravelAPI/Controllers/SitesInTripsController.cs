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
    [RoutePrefix("api/SitesInTrips")]
    public class SitesInTripsController : ApiController
    {
        SiteInTripBL stBl = new SiteInTripBL();
        [AcceptVerbs("GET", "POST")]
        [Route("addSiteInTrip")]
        [HttpPost]
        public int addSiteInTrip(SiteInTripToAdd t)
        {
            return stBl.AddSiteInTrip(t);
        }
    }
}
