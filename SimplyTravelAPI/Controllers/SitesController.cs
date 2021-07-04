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
    //post- כל מה שעושה שינויים בדטה בייס
    //get-מה שלא עושה שינויים בדטה בייס
    //  [Route("API/[Controller]")]
    [RoutePrefix("api/sites")]
    public class SitesController : ApiController
    {
        Algurithm a = new Algurithm();
        SiteBL siteBl = new SiteBL();
        [AcceptVerbs("GET", "POST")]
        [Route("signUp")]
        [HttpPost]
        public string AddSite(SiteModel s)
        {
            return this.AddNewSite(s);
        }
        public string AddNewSite(SiteModel s)
        {
            return siteBl.AddSite(s);
        }
        [AcceptVerbs("GET", "POST")]
        [Route("getMin/{code}")]
        [HttpGet]
        //access to the user's account.
        public int GetMin(int code)
        {
            return siteBl.GetMinAge(code);
        }
        [AcceptVerbs("GET", "POST")]
        [Route("getMax/{code}")]
        [HttpGet]
        //access to the user's account.
        public int GetMax(int code)
        {
            return siteBl.GetMaxAge(code);
        }
        [AcceptVerbs("GET", "POST")]
        [Route("getMis/{code}")]
        [HttpGet]
        //access to the user's account.
        public int GetMisLiter(int code)
        {
            return siteBl.GetMisLiter(code);
        }
        [AcceptVerbs("GET", "POST")]
        [Route("getTime/{code}")]
        [HttpGet]
        //access to the user's account.
        public double GetTimeSpend(int code)
        {
            return siteBl.GetTimeSpend(code);
        }
        [AcceptVerbs("GET", "POST")]
        [Route("getAllSites")]
        [HttpGet]
        public List<SiteToDisplay> GetAllSites()
        {
            return siteBl.GetAllSites();
        }
        [AcceptVerbs("GET", "POST")]
        [Route("plan/{code}/{min}/{max}/{add}/{half}/{car}")]
        [HttpGet]
        public Dictionary<int,List<SiteModel>> Plan(int code,int min,int max,string add,bool half,bool car)
        {
          return  a.CreateTravels(min, max, car, half, code, add);
        }
    }
}
