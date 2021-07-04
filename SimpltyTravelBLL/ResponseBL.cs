using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimpltyTravelBLL
{
    class ResponseBL:SimplyTravelBL
    {
   
        
        public ResponseBL()
        {
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
            return SimplyTravelDAL.Converts.ResponseConvert.ConvertResponseToModel(GetDbSet<Responses>().First(c => c.codeSiteInTrip == code));
        }
        //add a response
        public int AddResponse(int code,int q1,int q2,int q3,int q4,string note)
        {
            //check if response exist in DB
            if (GetResponseByCode(code) != null)
            {
                //if exist
                return 0;
            }
            //if (!Validation.LegalId(id) || !Validation.IsPassword(id, password))
            //    return SimplyTravelBL.Result.IncorrrectDetails;
            //------------validation 
            ResponseModel r = new ResponseModel() { CodeResponse = 1, CodeSiteInTrip = code, Question1 = q1, Question2 = q2, Question3 = q3, Question4 = q4, Notes = note };
            //add new response to the responses list
            AddToDB<Responses>(SimplyTravelDAL.Converts.ResponseConvert.ConvertResponseToEF(r));
            return r.CodeResponse;
        }
        //delete a response
        private int DeleteResponse(int code)
        {
            var response = GetResponseByCode(code);
            if (response == null)
                return 0;
            DeleteDB<Responses> (SimplyTravelDAL.Converts.ResponseConvert.ConvertResponseToEF(response));
            return 1;
        }
        //update a response
        private int UpdateResponse(ResponseModel r)
        {
            if (GetResponseByCode(r.CodeSiteInTrip.Value) == null)
                return 0;
            //------------validation 
            UpdateDB<Responses>(SimplyTravelDAL.Converts.ResponseConvert.ConvertResponseToEF(r));
            return 1;
        }
    }
}
