using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimpltyTravelBLL
{
    class SubRegionBL
    {
        DBConnection db;
        SimplyTravelBL s = new SimplyTravelBL();
        public SubRegionBL()
        {
            db = new DBConnection();
        }
        //get subregion by name and code region
        private Sub_RegionModel GetSubRegionByNameAndCode(string name,int code)
        {
            return SimplyTravelDAL.Converts.SubRegionConvert.ConvertSubRegionToModel(db.GetDbSet<Sub_Regions>().First(c=>c.nameSub_Region==name && c.codeRegion==code));
        }
        //add a sub-region
        public SimplyTravelBL.Result SignUP(string name, int code)
        {
            //check if subRegion exist in DB
            if (GetSubRegionByNameAndCode(name, code) != null)
            {
                //if exist
                return SimplyTravelBL.Result.NotFound;
            }
            //if (!Validation.LegalId(id) || !Validation.IsPassword(id, password))
            //    return SimplyTravelBL.Result.IncorrrectDetails;
            //------------validation 
            Sub_RegionModel r = new Sub_RegionModel() { CodeRegion = code, nameSub_Region = name, CodeSub_Region= 1};
            if (db.GetDbSet<Sub_Regions>().ToList().Count > 0)
                r.CodeSub_Region = db.GetDbSet<Sub_Regions>().ToList().Last().codeSub_Region + 1;
            //add new subRegion to the subRegions list
            s.AddToDB<Sub_Regions>(SimplyTravelDAL.Converts.SubRegionConvert.ConvertSubRegionToEF(r));
            return SimplyTravelBL.Result.Found;
        }
        //delete a subRegion
        private SimplyTravelBL.Result DeleteSub_Region(string name,int CodeRegion)
        {
            var sub = GetSubRegionByNameAndCode(name,CodeRegion);
            if (sub == null)
                return SimplyTravelBL.Result.NotFound;
            s.DeleteDB<Sub_Regions>(SimplyTravelDAL.Converts.SubRegionConvert.ConvertSubRegionToEF(sub));
            return SimplyTravelBL.Result.Found;
        }
        //update a subRegion
        private SimplyTravelBL.Result UpdateSub_Region(Sub_RegionModel c)
        {
            if (GetSubRegionByNameAndCode(c.nameSub_Region, c.CodeRegion.Value) == null)
                return SimplyTravelBL.Result.NotFound;
            //------------validation 
            s.UpdateDB<Sub_Regions>(SimplyTravelDAL.Converts.SubRegionConvert.ConvertSubRegionToEF(c));
            return SimplyTravelBL.Result.Found;
        }
    }
}
