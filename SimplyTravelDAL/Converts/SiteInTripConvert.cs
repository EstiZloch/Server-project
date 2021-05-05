using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimplyTravelDAL.Converts
{
   public static  class SiteInTripConvert
    {
        public static SitesInTrip ConvertSiteInTripToEF(SiteInTripModel siteInTrip)
        {
            return new SitesInTrip
            {
        codeSiteInTrip=siteInTrip.CodeSiteInTrip,
        codeTrip=siteInTrip.CodeTrip,
        codeSite=siteInTrip.CodeSite
    };
        }
        public static SiteInTripModel ConvertSiteInTripToModel(SitesInTrip siteInTrip)
        {
            return new SiteInTripModel
            {
                CodeSiteInTrip = siteInTrip.codeSiteInTrip,
                CodeTrip = siteInTrip.codeTrip,
                CodeSite = siteInTrip.codeSite
            };
        }



        public static List<SiteInTripModel> ConvertSiteInTripListToModel(IEnumerable<SitesInTrip> sitesInTrip)
        {
            return sitesInTrip.Select(c => ConvertSiteInTripToModel(c)).OrderBy(n => n.CodeSiteInTrip).ToList();
        }
    }
}
