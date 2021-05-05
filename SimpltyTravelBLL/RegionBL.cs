using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimpltyTravelBLL
{
    class RegionBL
    {
        DBConnection db;
        SimplyTravelBL s = new SimplyTravelBL();
        public RegionBL()
        {
            db = new DBConnection();
        }
        //get a region by code
        private RegionModel GetRegionById(int code)
        {
            return SimplyTravelDAL.Converts.RegionConvert.ConvertRegionToModel(db.GetDbSet<Regions>().First(c => c.codeRegion == code));
        }
        //get a region by name
        private RegionModel GetRegionByName(string name)
        {
            return SimplyTravelDAL.Converts.RegionConvert.ConvertRegionToModel(db.GetDbSet<Regions>().First(c => c.nameRegion == name));
        }
        //get all the subRegion in a specific region
        public List<Sub_RegionModel> GetSitesPerTrip(int codeR)
        {
            return SimplyTravelDAL.Converts.SubRegionConvert.ConvertSubRegionListToModel(db.GetDbSet<Sub_Regions>().Where(s => s.codeRegion == codeR).ToList());
        }
        //sign up function
        public SimplyTravelBL.Result AddRegion(string name)
        {
            //check if region exist in DB
            if (GetRegionByName(name) != null)
            {
                //if exist
                return SimplyTravelBL.Result.NotFound;
            }
            if (!Validation.IsHebrew(name))
                return SimplyTravelBL.Result.IncorrrectDetails;
            //------------validation 
            RegionModel c = new RegionModel() { CodeRegion = 1, NameRegion = name };
            if (db.GetDbSet<Regions>().ToList().Count > 0)
                c.CodeRegion = db.GetDbSet<Regions>().ToList().Last().codeRegion + 1;
            //add new region to the regions list
            s.AddToDB<Regions>(SimplyTravelDAL.Converts.RegionConvert.ConvertRegionToEF(c));
            return SimplyTravelBL.Result.Found;
        }
        //delete a region
        private SimplyTravelBL.Result Deleteregion(string name)
        {
            var reg = GetRegionByName(name);
            if (reg == null)
                return SimplyTravelBL.Result.NotFound;
            s.DeleteDB<Regions>(SimplyTravelDAL.Converts.RegionConvert.ConvertRegionToEF(reg));
            //לבדוק אם צריך להוסיף פה את העדכון של הקודים שאחרי האיבר הנמחק
            return SimplyTravelBL.Result.Found;
        }
        private SimplyTravelBL.Result UpdateRegion(RegionModel r)
        {
            if (GetRegionById(r.CodeRegion) == null)
                return SimplyTravelBL.Result.NotFound;
            //------------validation 
            s.UpdateDB<Regions>(SimplyTravelDAL.Converts.RegionConvert.ConvertRegionToEF(r));
            return SimplyTravelBL.Result.Found;
        }

    }
}
