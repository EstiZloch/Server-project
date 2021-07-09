using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimpltyTravelBLL
{
  public  class SiteInTripBL:SimplyTravelBL
    {
      
        public SiteInTripBL()
        {
        }
        //get site in trip by code trip
        public List<SiteInTripModel> GetSitesInTripByCodeTrip(int codeT)
        {
            return SimplyTravelDAL.Converts.SiteInTripConvert.ConvertSiteInTripListToModel(GetDbSet<SitesInTrip>().Where(s=>s.codeTrip==codeT).ToList());
         
        }
        //get site in trip by code site and code trip
        public SiteInTripModel GetSiteInTripByCodeSiteAndCodeTrip(int codeT,int codeS)
        {
            return SimplyTravelDAL.Converts.SiteInTripConvert.ConvertSiteInTripToModel(GetDbSet<SitesInTrip>().First(c => c.codeSite == codeS && c.codeTrip==codeT));
        }
        //add a site in trip;
        public int AddSiteInTrip(SiteInTripToAdd c)
        {
            SiteBL sBL = new SiteBL();
            SiteInTripModel s = new SiteInTripModel() { CodeTrip = c.CodeTrip, CodeSite = sBL.GetSiteByName(c.NameSite).CodeSite };
            //add new siteInTrip to the sitesInTrip list
            AddToDB<SitesInTrip>(SimplyTravelDAL.Converts.SiteInTripConvert.ConvertSiteInTripToEF(s));
            return c.CodeTrip;
        }
        //delete a site in trip
        private int DeleteSitIntrip(int codeS,int codeT)
        {
            var siteIn = GetSiteInTripByCodeSiteAndCodeTrip(codeT,codeS);
            if (siteIn == null)
                return 0;
            DeleteDB<SitesInTrip>(SimplyTravelDAL.Converts.SiteInTripConvert.ConvertSiteInTripToEF(siteIn));
            return 1;
        }
        private int UpdateSiteInTrip(SiteInTripModel c)
        {
            if (GetSiteInTripByCodeSiteAndCodeTrip(c.CodeTrip.Value,c.CodeSite.Value) == null)
                return 0;
            //------------validation 
            UpdateDB<SitesInTrip>(SimplyTravelDAL.Converts.SiteInTripConvert.ConvertSiteInTripToEF(c));
            return 1;
        }
    }
}
