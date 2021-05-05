using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimplyTravelDAL.Converts
{
   public static class SubRegionConvert
    {
        public static Sub_Regions ConvertSubRegionToEF(Sub_RegionModel subRegion)
        {
            return new Sub_Regions
            {
       codeSub_Region =subRegion.CodeSub_Region,
       codeRegion=subRegion.CodeRegion,
       nameSub_Region=subRegion.nameSub_Region
    };
        }
        public static Sub_RegionModel ConvertSubRegionToModel(Sub_Regions subRegion)
        {
            return new Sub_RegionModel
            {
                CodeSub_Region = subRegion.codeSub_Region,
                CodeRegion = subRegion.codeRegion,
                nameSub_Region = subRegion.nameSub_Region
            };
        }



        public static List<Sub_RegionModel> ConvertSubRegionListToModel(IEnumerable<Sub_Regions> SubRegions)
        {
            return SubRegions.Select(c => ConvertSubRegionToModel(c)).OrderBy(n => n.CodeSub_Region).ToList();
        }
    }
}
