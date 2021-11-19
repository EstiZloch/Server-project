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
    [RoutePrefix("api/trips")]
    public class TripsController : ApiController
    {
        SiteBL sbl = new SiteBL();
        TripBL tripBl = new TripBL();
        [AcceptVerbs("GET", "POST")]
        [Route("addTrip")]
        [HttpPost]
        public int addTrip(TripModel t)
        {
            return tripBl.AddTrip(t);
        }
        [AcceptVerbs("GET", "POST")]
        [Route("getTrips/{id}")]
        [HttpGet]
        public List<string>[] getTrips(int id)
        {
            int i = 0;
            List<TripModel> r= tripBl.GetAllTripsToCustomer(id);
            r = r.OrderByDescending(t => t.DateTrip).ToList();
            List<string>[] result = new List<string>[r.Count];
            foreach(var v in r)
            {
                result[i++] =new List<string>() { v.CodeTrip.ToString(), v.NameTrip.ToString(), DateTime.Parse(v.DateTrip.ToString()).ToShortDateString() };
            }
           return result;
        }
        [AcceptVerbs("GET", "POST")]
        [Route("getDetailTrip/{id}")]
        [HttpGet]
        public List<string>[] getDetailTrip(int id)
        {

            List<TripModel> trips = tripBl.GetAllTripsToCustomer(id); 
            List<string>[] results = new List<string>[trips.Count];
            List<SiteInTripModel> sites = new List<SiteInTripModel>();
            for(int i=0;i<trips.Count;i++)
            {
                results[i] = new List<string>();
                sites = tripBl.GetSitesPerTrip(trips[i].CodeTrip);
                foreach(var s in sites)
                results[i].Add(sbl.GetSiteByCode((int)s.CodeSite).NameSite);
            }
            return results;
        }
        
    }
}
