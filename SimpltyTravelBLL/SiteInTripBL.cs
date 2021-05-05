using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimpltyTravelBLL
{
    class SiteInTripBL
    {
        DBConnection db;
        SimplyTravelBL s = new SimplyTravelBL();
        public SiteInTripBL()
        {
            db = new DBConnection();
        }
        //get site in trip by code trip
        public SiteInTripModel GetSitesInTripByCodeTrip(int codeT)
        {
            return SimplyTravelDAL.Converts.SiteInTripConvert.ConvertSiteInTripToModel(db.GetDbSet<SitesInTrip>().First(c => c.codeTrip == codeT));
        }
        //get site in trip by code site and code trip
        public SiteInTripModel GetSiteInTripByCodeSiteAndCodeTrip(int codeT,int codeS)
        {
            return SimplyTravelDAL.Converts.SiteInTripConvert.ConvertSiteInTripToModel(db.GetDbSet<SitesInTrip>().First(c => c.codeSite == codeS && c.codeTrip==codeT));
        }
        //add a site in trip
        public SimplyTravelBL.Result AddSiteInTrip(int codeT, int codeS)
        {
            //check if site in trip exist in DB
            if (GetSiteInTripByCodeSiteAndCodeTrip(codeT,codeS) != null)
            {
                //if exist
                return SimplyTravelBL.Result.NotFound;
            }
            //if (!Validation.LegalId(id) || !Validation.IsPassword(id, password))
            //    return SimplyTravelBL.Result.IncorrrectDetails;
            ////------------validation 
            SiteInTripModel c = new SiteInTripModel() { CodeTrip=codeT,CodeSite=codeS,CodeSiteInTrip=1 };
            if (db.GetDbSet<SitesInTrip>().ToList().Count > 0)
                c.CodeSiteInTrip = db.GetDbSet<SitesInTrip>().ToList().Last().codeSiteInTrip + 1;
            //add new siteInTrip to the sitesInTrip list
            s.AddToDB<SitesInTrip>(SimplyTravelDAL.Converts.SiteInTripConvert.ConvertSiteInTripToEF(c));
            return SimplyTravelBL.Result.Found;
        }
        //delete a site in trip
        private SimplyTravelBL.Result DeleteSitIntrip(int codeS,int codeT)
        {
            var siteIn = GetSiteInTripByCodeSiteAndCodeTrip(codeT,codeS);
            if (siteIn == null)
                return SimplyTravelBL.Result.NotFound;
            s.DeleteDB<SitesInTrip>(SimplyTravelDAL.Converts.SiteInTripConvert.ConvertSiteInTripToEF(siteIn));
            return SimplyTravelBL.Result.Found;
        }
        private SimplyTravelBL.Result UpdateSiteInTrip(SiteInTripModel c)
        {
            if (GetSiteInTripByCodeSiteAndCodeTrip(c.CodeTrip.Value,c.CodeSite.Value) == null)
                return SimplyTravelBL.Result.NotFound;
            //------------validation 
            s.UpdateDB<SitesInTrip>(SimplyTravelDAL.Converts.SiteInTripConvert.ConvertSiteInTripToEF(c));
            return SimplyTravelBL.Result.Found;
        }
    }
}
