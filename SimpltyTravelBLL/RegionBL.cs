using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimpltyTravelBLL
{
   public class RegionBL:SimplyTravelBL
    {
      
        public RegionBL()
        {
 
        }
        //get a region by code
        public RegionModel GetRegionById(int code)
        {
            return SimplyTravelDAL.Converts.RegionConvert.ConvertRegionToModel(GetDbSet<Regions>().First(c => c.codeRegion == code));
        }
        //get a list of the region names
        public List<RegionModel> GetListRegionNames()
        {
            return SimplyTravelDAL.Converts.RegionConvert.ConvertRegionrListToModel(GetDbSet<Regions>().ToList());
        }
        //get a region by name
        public RegionModel GetRegionByName(string name)
        {
            return SimplyTravelDAL.Converts.RegionConvert.ConvertRegionToModel(GetDbSet<Regions>().First(c => c.nameRegion == name));
        }
        //get all the subRegion in a specific region
        public List<Sub_RegionModel> GetSubRegionOfRegion(int codeR)
        {
            return SimplyTravelDAL.Converts.SubRegionConvert.ConvertSubRegionListToModel(GetDbSet<Sub_Regions>().Where(s => s.codeRegion == codeR).ToList());
        }
        //sign up function
        public int AddRegion(string name)
        {
            //check if region exist in DB
            if (GetRegionByName(name) != null)
            {
                //if exist
                return 0;
            }
            //------------validation 
            RegionModel c = new RegionModel() { CodeRegion = 1, NameRegion = name };
            if (GetDbSet<Regions>().ToList().Count > 0)
                c.CodeRegion = GetDbSet<Regions>().ToList().Last().codeRegion + 1;
            //add new region to the regions list
            AddToDB<Regions>(SimplyTravelDAL.Converts.RegionConvert.ConvertRegionToEF(c));
            return c.CodeRegion;
        }
        //delete a region
        private int Deleteregion(string name)
        {
            var reg = GetRegionByName(name);
            if (reg == null)
                return 0;
            DeleteDB<Regions>(SimplyTravelDAL.Converts.RegionConvert.ConvertRegionToEF(reg));
            //לבדוק אם צריך להוסיף פה את העדכון של הקודים שאחרי האיבר הנמחק
            return 1;
        }
        private int UpdateRegion(string newName)
        {
            RegionModel r = GetRegionByName(newName);
            if ( r== null)
                return 0;
            //------------validation 
            UpdateDB<Regions>(SimplyTravelDAL.Converts.RegionConvert.ConvertRegionToEF(r));
            return 1;
        }

    }
}
