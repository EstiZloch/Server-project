using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimpltyTravelBLL
{
   public class SubRegionBL:SimplyTravelBL
    {
      
        public SubRegionBL()
        {
        }
        //get subregion by name and code region
        public Sub_RegionModel GetSubRegionByNameAndCode(string name,int code)
        {
            return SimplyTravelDAL.Converts.SubRegionConvert.ConvertSubRegionToModel(GetDbSet<Sub_Regions>().First(c=>c.nameSub_Region==name && c.codeRegion==code));
        }
        //get subregion by code sub region
        public Sub_RegionModel GetSubRegionByCode(int codes)
        {
            return SimplyTravelDAL.Converts.SubRegionConvert.ConvertSubRegionToModel(GetDbSet<Sub_Regions>().First(c => c.codeSub_Region==codes));
        }
        //get list of subregion by code region
        public List<Sub_RegionModel> GetListSubRegionByCode( int code)
        {
            return SimplyTravelDAL.Converts.SubRegionConvert.ConvertSubRegionListToModel(GetDbSet<Sub_Regions>().Where(c=> c.codeRegion == code).ToList());
        }
        //add a sub-region
        public int AddSubRegion(string name, int code)
        {
            //check if subRegion exist in DB
            if (GetSubRegionByNameAndCode(name, code) != null)
            {
                //if exist
                return 0;
            }
            //if (!Validation.LegalId(id) || !Validation.IsPassword(id, password))
            //    return SimplyTravelBL.Result.IncorrrectDetails;
            //------------validation 
            Sub_RegionModel r = new Sub_RegionModel() { CodeRegion = code, nameSub_Region = name, CodeSub_Region= 1};
            if (GetDbSet<Sub_Regions>().ToList().Count > 0)
                r.CodeSub_Region = GetDbSet<Sub_Regions>().ToList().Last().codeSub_Region + 1;
            //add new subRegion to the subRegions list
            AddToDB<Sub_Regions>(SimplyTravelDAL.Converts.SubRegionConvert.ConvertSubRegionToEF(r));
            return r.CodeSub_Region;
        }
        //delete a subRegion
        private int DeleteSub_Region(string name,int CodeRegion)
        {
            var sub = GetSubRegionByNameAndCode(name,CodeRegion);
            if (sub == null)
                return 0;
            DeleteDB<Sub_Regions>(SimplyTravelDAL.Converts.SubRegionConvert.ConvertSubRegionToEF(sub));
            return 1;
        }
        //update a subRegion
        private int UpdateSub_Region(Sub_RegionModel c)
        {
            if (GetSubRegionByNameAndCode(c.nameSub_Region, c.CodeRegion.Value) == null)
                return 0;
            //------------validation 
            UpdateDB<Sub_Regions>(SimplyTravelDAL.Converts.SubRegionConvert.ConvertSubRegionToEF(c));
            return 1;
        }
    }
}
