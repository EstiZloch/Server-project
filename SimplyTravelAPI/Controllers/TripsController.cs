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
        TripBL tripBl = new TripBL();
        [AcceptVerbs("GET", "POST")]
        [Route("addTrip")]
        [HttpPost]
        public int addTrip(TripModel t)
        {
            return tripBl.AddTrip(t);
        }

    }
}
