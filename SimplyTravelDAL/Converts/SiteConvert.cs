using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimplyTravelDAL.Converts
{
   public static class SiteConvert
    {
        public static Sites ConvertSiteToEF(SiteModel sites)
        {
            return new Sites
            {
        codeSite =sites.CodeSite,
        codeSiteKind =sites.CodeSiteKind,
       nameSite=sites.NameSite,
       adress =sites.Address,
       codeSub_Region =sites.CodeSub_Region,
       extraLevel=sites.ExtraLevel,
       minAge=sites.MinAge,
       maxAge=sites.MaxAge,
       misLiterWater =sites.MisLiterWater,
        free_notFree=sites.Free_notFree,
       sumToPay=sites.SumToPay,
        car_bus =sites.Car_bus,
        coordination =sites.Coordination,
        timeSpend =sites.TimeSpend,
         statusSite=sites.StatusSite
    };
        }
        public static SiteModel ConvertSiteToModel(Sites sites)
        {
            return new SiteModel
            {
                CodeSite = sites.codeSite,
                CodeSiteKind = sites.codeSiteKind,
                NameSite = sites.nameSite,
                Address = sites.adress,
                CodeSub_Region = sites.codeSub_Region,
                ExtraLevel = sites.extraLevel,
                MinAge = sites.minAge,
                MaxAge = sites.maxAge,
                MisLiterWater = sites.misLiterWater,
                Free_notFree = sites.free_notFree,
                SumToPay = sites.sumToPay,
                Car_bus = sites.car_bus,
                Coordination = sites.coordination,
                TimeSpend = sites.timeSpend,
                StatusSite = sites.statusSite
            };
        }



        public static List<SiteModel> ConvertSiteListToModel(IEnumerable<Sites> sites)
        {
            return sites.Select(c => ConvertSiteToModel(c)).OrderBy(n => n.CodeSite).ToList();
        }
    }
}
