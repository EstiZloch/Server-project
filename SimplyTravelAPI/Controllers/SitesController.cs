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
        [Route("update")]
        [HttpPost]
        public int Update(SiteModel s)
        {
            return siteBl.UpdateSite(s);
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
        [Route("plan/{code}/{min}/{max}/{add}/{half}/{car}/{idcus}")]
        [HttpGet]
        public List<string>[] Plan(int code,int min,int max,string add,bool half,bool car,int idcus)
        {
          return  a.CreateTravels(min, max, car, half, code, add,idcus);
        }
        [AcceptVerbs("GET", "POST")]
        [Route("getSiteDetails/{name}")]
        [HttpGet]

        public List<SiteToDisplay> getSiteDetails([FromUri] string[] name)
        {
            SiteKindBL skBl = new SiteKindBL();
            SubRegionBL subBl = new SubRegionBL();
            RegionBL rBl = new RegionBL();
            string [] arr = name[0].Split(',');
            List<SiteModel> sites=new List<SiteModel> ();
            List<SiteToDisplay> sitesToResults=new List<SiteToDisplay> ();
            Sub_RegionModel s = new Sub_RegionModel();
            foreach (var n in arr)
                sites.Add(siteBl.GetSiteByName(n));
            for (int i = 0; i < sites.Count; i++)
            {
                if (sites[i] != null)
                    s = subBl.GetSubRegionByCode((int)sites[i].CodeSub_Region);
                if (sites[i] != null)
                    sitesToResults.Add(new SiteToDisplay()
                {
                    CodeSite = sites[i].CodeSite,
                    NameSiteKind = skBl.GetSiteKindByCode((int)sites[i].CodeSiteKind).NameSiteKind,
                    NameSite = sites[i].NameSite,
                    Adress = sites[i].Adress,
                    NameSub_Region = s.nameSub_Region,
                    NameRegion = rBl.GetRegionById((int)s.CodeRegion).NameRegion,
                    ExtraLevel = (int)sites[i].ExtraLevel,
                    MinAge = (int)sites[i].MinAge,
                    MaxAge = (int)sites[i].MaxAge,
                    MisLiterWater = (int)sites[i].MisLiterWater,
                    TimeSpend = (int)sites[i].TimeSpend,
                    StatusSite = sites[i].StatusSite == "true" ? "פעיל" : "לא פעיל",
                    Car_bus = (bool)sites[i].Car_bus ? "תחבורה ציבורית" : "הגעה עצמית",
                    Free_notFree = (bool)sites[i].Free_notFree ? "חינם" : "לא בחינם"
                });
            }
            return sitesToResults;
        }
        [AcceptVerbs("GET", "POST")]
        [Route("getSiteDetailsByCode/{code}")]
        [HttpGet]
        public SiteModel getSiteDetailsByCode(int code)
        {
            return siteBl.GetSiteByCode(code);

            }
        [AcceptVerbs("GET", "POST")]
        [Route("changeStatus")]
        [HttpPost]
        public void changeStatus(SiteToDisplay site)
        {
             siteBl.ChangeStatusSite(site.CodeSite);
        }
    }
}
