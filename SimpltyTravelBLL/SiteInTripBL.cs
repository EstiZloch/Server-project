using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimpltyTravelBLL
{
    class SiteInTripBL:SimplyTravelBL
    {
      
        public SiteInTripBL()
        {
        }
        //get site in trip by code trip
        public SiteInTripModel GetSitesInTripByCodeTrip(int codeT)
        {
            return SimplyTravelDAL.Converts.SiteInTripConvert.ConvertSiteInTripToModel(GetDbSet<SitesInTrip>().First(c => c.codeTrip == codeT));
        }
        //get site in trip by code site and code trip
        public SiteInTripModel GetSiteInTripByCodeSiteAndCodeTrip(int codeT,int codeS)
        {
            return SimplyTravelDAL.Converts.SiteInTripConvert.ConvertSiteInTripToModel(GetDbSet<SitesInTrip>().First(c => c.codeSite == codeS && c.codeTrip==codeT));
        }
        //add a site in trip
        public int AddSiteInTrip(int codeT, int codeS)
        {
            //check if site in trip exist in DB
            if (GetSiteInTripByCodeSiteAndCodeTrip(codeT,codeS) != null)
            {
                //if exist
                return 0;
            }
            //if (!Validation.LegalId(id) || !Validation.IsPassword(id, password))
            //    return SimplyTravelBL.Result.IncorrrectDetails;
            ////------------validation 
            SiteInTripModel c = new SiteInTripModel() { CodeTrip=codeT,CodeSite=codeS,CodeSiteInTrip=1 };
            if (GetDbSet<SitesInTrip>().ToList().Count > 0)
                c.CodeSiteInTrip = GetDbSet<SitesInTrip>().ToList().Last().codeSiteInTrip + 1;
            //add new siteInTrip to the sitesInTrip list
            AddToDB<SitesInTrip>(SimplyTravelDAL.Converts.SiteInTripConvert.ConvertSiteInTripToEF(c));
            return c.CodeSiteInTrip;
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
