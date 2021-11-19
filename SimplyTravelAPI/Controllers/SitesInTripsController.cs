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
        SiteBL sBl = new SiteBL();
        SiteInTripBL stBl = new SiteInTripBL();
        [AcceptVerbs("GET", "POST")]
        [Route("addSiteInTrip")]
        [HttpPost]
        public int addSiteInTrip(SiteInTripToAdd t)
        {
            return stBl.AddSiteInTrip(t);
        }
        [AcceptVerbs("GET", "POST")]
        [Route("getNamesSitesInTrip/{code}")]
        [HttpGet]
        public List<string> getNamesSitesInTrip(int code)
        {
            List<SiteInTripModel> sites = stBl.GetSitesInTripByCodeTrip(code);
            List<string> results = new List<string>();
            foreach (var v in sites)
                results.Add(sBl.GetSiteByCode((int)v.CodeSite).NameSite);
            return results;
        }
        
    }
}
