using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimpltyTravelBLL
{
   public class SiteBL:SimplyTravelBL
    {

     
        //dictionary of the default time spend in the sites    
        Dictionary<int, double> TimeSpend = new Dictionary<int, double>()
        {
            { 1,0.5  },
            { 2,0.5  },
            { 3,1  },
            { 4,2  },
            { 5,25  },
            { 6,3  },
            { 7,3  },
            { 8,3  },
            { 9,4  }
        };
        public SiteBL()
        {
        }
        //get site by name
        public SiteModel GetSiteByName(string name)
        {
            List<SiteModel> site = SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(GetDbSet<Sites>());
            if (site != null)
                return site.Find(c => c.NameSite == name);
            return null;
        }
        //get the average time spend in the sites
        public int GetAvgTime()
        {
            double avg= double.Parse( GetDbSet<Sites>().Average(a=>a.timeSpend).ToString());
            return (int)avg;
        }
        //get site by code
        private SiteModel GetSiteByCode(int code)
        {
            return SimplyTravelDAL.Converts.SiteConvert.ConvertSiteToModel(GetDbSet<Sites>().First(c => c.codeSite == code));
        }
        //get sites by code kind site
        public List<SiteModel> GetSitesByCodeKindSite(int codeKindSite)
        {
            return SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(GetDbSet<Sites>().Where(c => c.codeSiteKind == codeKindSite).ToList());
        }
        //add a site
        public string AddSite(SiteModel site)
        { 
            //check if site exist in DB
            if (GetSiteByName(site.NameSite)!=null)
            {
                //if exist
                return "";
            }
            // if (Validation. || !Validation.IsPassword(id, password))
            //   return SimplyTravelBL.Result.IncorrrectDetails;
            //------------validation 
            //add new site to the sites list
            //  site.TimeSpend =arrTimesSpend[index];
            AddToDB<Sites>(SimplyTravelDAL.Converts.SiteConvert.ConvertSiteToEF(site));
            return site.NameSite;
        }

        public List<SiteToDisplay> GetAllSites()
        {
            SiteKindBL skBl = new SiteKindBL();
            Sub_RegionModel s = new Sub_RegionModel();
            SubRegionBL subBl = new SubRegionBL();
            RegionBL rBl = new RegionBL();
            List<SiteModel> sites = SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(GetDbSet<Sites>());
            List<SiteToDisplay> sitesToResults = new List<SiteToDisplay>();
            for(int i=0;i<sites.Count;i++)
            {
                s = subBl.GetSubRegionByCode((int)sites[i].CodeSub_Region);
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
                }) ;
            }
            return sitesToResults;
        }

        public double GetTimeSpend(int code)
        {
       return TimeSpend[code];
        }

        public int GetMisLiter(int code)
        {
            if (code == 4 || code == 5)
                return 1;
            if (code == 2 || code == 3)
                return 0;
            return 1;
        }

        public int GetMaxAge(int code)
        {
            if (code == 4 || code == 5)
                return 40;
            if (code == 6 || code == 7)
                return 30;
            return 120;
        }

        public int GetMinAge(int code)
        {
            if (code == 4 || code == 5)
                return 10;
            if (code == 6 || code == 7)
                return 10;
            if (code == 9)
                return 15;
            return 0;
        }

        //change status to a site
        public string ChangeStatusSite(int code, string status)
        {
            var site = this.GetSiteByCode(code);
            string statusRet = site.StatusSite;
            site.StatusSite = status;
            UpdateDB<Sites>(SimplyTravelDAL.Converts.SiteConvert.ConvertSiteToEF(site));
            return statusRet;
        }
        //delete a site
        private int DeleteSite(int code)
        {
            var site = GetSiteByCode(code);
            if (site == null)
                return 0;
            DeleteDB<Sites>(SimplyTravelDAL.Converts.SiteConvert.ConvertSiteToEF(site));
            return 1;
        }
        private int UpdateSite(SiteModel c)
        {
            if (GetSiteByCode(c.CodeSite) == null)
                return 0;
            //------------validation 
            UpdateDB<Sites>(SimplyTravelDAL.Converts.SiteConvert.ConvertSiteToEF(c));
            return 1;
        }
    }
}
