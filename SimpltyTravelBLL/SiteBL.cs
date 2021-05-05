using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimpltyTravelBLL
{
    class SiteBL
    {

        DBConnection db;
        SimplyTravelBL s = new SimplyTravelBL();
        //arr of the default time spend in the sites    
        double[] arrTimesSpend = new double[9] { 0.5,1,0.5,3,3,2,2,3,4 };
        public SiteBL()
        {
            db = new DBConnection();
        }
        //get site by location
        private SiteModel GetSiteByDatum_point(string location)
        {
            return SimplyTravelDAL.Converts.SiteConvert.ConvertSiteToModel(db.GetDbSet<Sites>().First(c => c.coordination == location));
        }
        //get the average time spend in the sites
        public int GetAvgTime()
        {
            double avg= double.Parse( db.GetDbSet<Sites>().Average(a=>a.timeSpend).ToString());
            return int.Parse(avg.ToString());
        }
        //get site by code
        private SiteModel GetSiteByCode(int code)
        {
            return SimplyTravelDAL.Converts.SiteConvert.ConvertSiteToModel(db.GetDbSet<Sites>().First(c => c.codeSite == code));
        }
        //get sites by code kind site
        public List<SiteModel> GetSitesByCodeKindSite(int codeKindSite)
        {
            return SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(db.GetDbSet<Sites>().Where(c => c.codeSiteKind == codeKindSite).ToList());
        }
        //add a site
        public SimplyTravelBL.Result AddSite(SiteModel site)
        { 
            //check if site exist in DB
            if (GetSiteByDatum_point(site.Coordination)!=null)
            {
                //if exist
                return SimplyTravelBL.Result.NotFound;
            }
            // if (Validation. || !Validation.IsPassword(id, password))
            //   return SimplyTravelBL.Result.IncorrrectDetails;
            //------------validation 
            //add new site to the sites list
            int index = int.Parse(site.CodeSiteKind.ToString());
          //  site.TimeSpend =arrTimesSpend[index];
            s.AddToDB<Sites>(SimplyTravelDAL.Converts.SiteConvert.ConvertSiteToEF(site));
            return SimplyTravelBL.Result.Found;
        }
        //change status to a site
        private string ChangeStatusSite(int code, string status)
        {
            var site = this.GetSiteByCode(code);
            string statusRet = site.StatusSite;
            site.StatusSite = status;
            s.UpdateDB<Sites>(SimplyTravelDAL.Converts.SiteConvert.ConvertSiteToEF(site));
            return statusRet;
        }
        //delete a site
        private SimplyTravelBL.Result DeleteSite(int code)
        {
            var site = GetSiteByCode(code);
            if (site == null)
                return SimplyTravelBL.Result.NotFound;
            s.DeleteDB<Sites>(SimplyTravelDAL.Converts.SiteConvert.ConvertSiteToEF(site));
            return SimplyTravelBL.Result.Found;
        }
        private SimplyTravelBL.Result UpdateSite(SiteModel c)
        {
            if (GetSiteByCode(c.CodeSite) == null)
                return SimplyTravelBL.Result.NotFound;
            //------------validation 
            s.UpdateDB<Sites>(SimplyTravelDAL.Converts.SiteConvert.ConvertSiteToEF(c));
            return SimplyTravelBL.Result.Found;
        }
    }
}
