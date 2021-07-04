using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimpltyTravelBLL
{
    class SiteKindBL:SimplyTravelBL
    {
     
        public SiteKindBL()
        {
        }
        //get sitekind by kind
        public SiteKindModel GetSiteKindByKind(string kind)
        {
            return SimplyTravelDAL.Converts.SiteKindConvert.ConvertSiteKindToModel(GetDbSet<SitesKind>().First(c => c.nameSiteKind == kind));
        }
        //get sitekind by code
        public SiteKindModel GetSiteKindByCode(int code)
        {
            return SimplyTravelDAL.Converts.SiteKindConvert.ConvertSiteKindToModel(GetDbSet<SitesKind>().First(c => c.codeSiteKind == code));
        }
        //add a siteKind
        public int AddSiteKind(string kind)
        {
            //check if sitekind exist in DB
            if (GetSiteKindByKind(kind) != null)
            {
                //if exist
                return 0;
            }
            //if (!Validation.LegalId(id) || !Validation.IsPassword(id, password))
            //    return SimplyTravelBL.Result.IncorrrectDetails;
            //------------validation 
            SiteKindModel c = new SiteKindModel() { NameSiteKind=kind,CodeSiteKind=1};
            if (GetDbSet<SitesKind>().ToList().Count > 0)
                c.CodeSiteKind = GetDbSet<SitesKind>().ToList().Last().codeSiteKind + 1;
            //add new sitekind to the sitekinds list
            AddToDB<SitesKind>(SimplyTravelDAL.Converts.SiteKindConvert.ConvertSiteKindToEF(c));
            return c.CodeSiteKind;
        }
        //delete a siteKind
        private int DeleteSiteKind(string kind)
        {
            var site = GetSiteKindByKind(kind);
            if (site == null)
                return 0;
            DeleteDB<SitesKind>(SimplyTravelDAL.Converts.SiteKindConvert.ConvertSiteKindToEF(site));
            return 1;
        }
        private int UpdateSiteKind(SiteKindModel c)
        {
            if (GetSiteKindByKind(c.NameSiteKind) == null)
                return 0;
            //------------validation 
            UpdateDB<SitesKind>(SimplyTravelDAL.Converts.SiteKindConvert.ConvertSiteKindToEF(c));
            return 1;
        }
    }
}
