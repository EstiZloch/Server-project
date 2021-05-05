using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimplyTravelDAL.Converts
{
   public static class SiteKindConvert
    {
        public static SitesKind ConvertSiteKindToEF(SiteKindModel sitekind)
        {
            return new SitesKind
            {
        codeSiteKind =sitekind.CodeSiteKind,
        nameSiteKind=sitekind.NameSiteKind
    };
        }
        public static SiteKindModel ConvertSiteKindToModel(SitesKind sitekind)
        {
            return new SiteKindModel
            {
                CodeSiteKind = sitekind.codeSiteKind,
                NameSiteKind = sitekind.nameSiteKind
            };
        }



        public static List<SiteKindModel> ConvertSiteKindListToModel(IEnumerable<SitesKind> SitesKind)
        {
            return SitesKind.Select(c => ConvertSiteKindToModel(c)).OrderBy(n => n.CodeSiteKind).ToList();
        }
    }
}
