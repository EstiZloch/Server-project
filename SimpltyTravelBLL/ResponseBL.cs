using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimpltyTravelBLL
{
    class ResponseBL
    {
        DBConnection db;
        SimplyTravelBL s = new SimplyTravelBL();
        public ResponseBL()
        {
            db = new DBConnection();
        }
        //get a response by IdCustomer and code site
        private bool GetResponseByCodeToSiteToSpecifiecCustomer(int code,int id)
        {
            CustomerBL c = new CustomerBL();
            List<SiteInTripModel> siteInTrip = c.GetSitesPerCustomer(id);
            SiteInTripModel site = siteInTrip.First(s => s.CodeSite == code);
            if (site==null)
                return false;
            if(GetResponseByCode(site.CodeSite.Value).Question4==0)
            return false;
            return true;
        }
        //get a response by CodeSiteInTrip
        private ResponseModel GetResponseByCode(int code)
        {
            return SimplyTravelDAL.Converts.ResponseConvert.ConvertResponseToModel(db.GetDbSet<Responses>().First(c => c.codeSiteInTrip == code));
        }
        //add a response
        public SimplyTravelBL.Result AddResponse(int code,int q1,int q2,int q3,int q4,string note)
        {
            //check if response exist in DB
            if (GetResponseByCode(code) != null)
            {
                //if exist
                return SimplyTravelBL.Result.NotFound;
            }
            //if (!Validation.LegalId(id) || !Validation.IsPassword(id, password))
            //    return SimplyTravelBL.Result.IncorrrectDetails;
            //------------validation 
            ResponseModel r = new ResponseModel() { CodeResponse = 1, CodeSiteInTrip = code, Question1 = q1, Question2 = q2, Question3 = q3, Question4 = q4, Notes = note };
            //add new response to the responses list
            s.AddToDB<Responses>(SimplyTravelDAL.Converts.ResponseConvert.ConvertResponseToEF(r));
            return SimplyTravelBL.Result.Found;
        }
        //delete a customer
        private SimplyTravelBL.Result DeleteResponse(int code)
        {
            var response = GetResponseByCode(code);
            if (response == null)
                return SimplyTravelBL.Result.NotFound;
            s.DeleteDB<Responses> (SimplyTravelDAL.Converts.ResponseConvert.ConvertResponseToEF(response));
            return SimplyTravelBL.Result.Found;
        }
        //update a response
        private SimplyTravelBL.Result UpdateResponse(ResponseModel r)
        {
            if (GetResponseByCode(r.CodeSiteInTrip.Value) == null)
                return SimplyTravelBL.Result.NotFound;
            //------------validation 
            s.UpdateDB<Responses>(SimplyTravelDAL.Converts.ResponseConvert.ConvertResponseToEF(r));
            return SimplyTravelBL.Result.Found;
        }
    }
}
