using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimplyTravelDAL.Converts
{
    public static class RegionConvert
    {
        public static Regions ConvertRegionToEF(RegionModel region)
        {
            return new Regions
            {
        codeRegion =region.CodeRegion,
        nameRegion =region.NameRegion
             };
        }
        public static RegionModel ConvertRegionToModel(Regions region)
        {
            return new RegionModel
            {
                CodeRegion = region.codeRegion,
                NameRegion = region.nameRegion
            };
        }



        public static List<RegionModel> ConvertRegionrListToModel(IEnumerable<Regions> regions)
        {
            return regions.Select(c => ConvertRegionToModel(c)).OrderBy(n => n.CodeRegion).ToList();
        }
    }
}
