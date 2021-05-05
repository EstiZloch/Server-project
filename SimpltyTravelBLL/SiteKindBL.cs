using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimpltyTravelBLL
{
    class SiteKindBL
    {
        DBConnection db;
        SimplyTravelBL s = new SimplyTravelBL();
        public SiteKindBL()
        {
            db = new DBConnection();
        }
        //get sitekind by kind
        private SiteKindModel GetSiteKindByKind(string kind)
        {
            return SimplyTravelDAL.Converts.SiteKindConvert.ConvertSiteKindToModel(db.GetDbSet<SitesKind>().First(c => c.nameSiteKind == kind));
        }
        //add a siteKind
        public SimplyTravelBL.Result AddSiteKind(string kind)
        {
            //check if sitekind exist in DB
            if (GetSiteKindByKind(kind) != null)
            {
                //if exist
                return SimplyTravelBL.Result.NotFound;
            }
            //if (!Validation.LegalId(id) || !Validation.IsPassword(id, password))
            //    return SimplyTravelBL.Result.IncorrrectDetails;
            //------------validation 
            SiteKindModel c = new SiteKindModel() { NameSiteKind=kind,CodeSiteKind=1};
            if (db.GetDbSet<SitesKind>().ToList().Count > 0)
                c.CodeSiteKind = db.GetDbSet<SitesKind>().ToList().Last().codeSiteKind + 1;
            //add new sitekind to the sitekinds list
            s.AddToDB<SitesKind>(SimplyTravelDAL.Converts.SiteKindConvert.ConvertSiteKindToEF(c));
            return SimplyTravelBL.Result.Found;
        }
        //delete a siteKind
        private SimplyTravelBL.Result DeleteSiteKind(string kind)
        {
            var site = GetSiteKindByKind(kind);
            if (site == null)
                return SimplyTravelBL.Result.NotFound;
            s.DeleteDB<SitesKind>(SimplyTravelDAL.Converts.SiteKindConvert.ConvertSiteKindToEF(site));
            return SimplyTravelBL.Result.Found;
        }
        private SimplyTravelBL.Result UpdateSiteKind(SiteKindModel c)
        {
            if (GetSiteKindByKind(c.NameSiteKind) == null)
                return SimplyTravelBL.Result.NotFound;
            //------------validation 
            s.UpdateDB<SitesKind>(SimplyTravelDAL.Converts.SiteKindConvert.ConvertSiteKindToEF(c));
            return SimplyTravelBL.Result.Found;
        }
    }
}
