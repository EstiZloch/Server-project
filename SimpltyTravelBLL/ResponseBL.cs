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
        public bool GetResponseByCodeToSiteToSpecifiecCustomer(int code,int id,List<SiteInTripModel>siteInTrip)
        {
            CustomerBL c = new CustomerBL();
           List< SiteInTripModel> site = siteInTrip.Where(s => s.CodeSite == id).ToList();
            if(site.Count==0)
                return true;
            var response = GetResponseByCode(site[0].CodeSite.Value);
            if (response==null)
                return true;
            if(response.Question4 == 0)
            return false;
            return true;
        }
        //get a response by CodeSiteInTrip
        private ResponseModel GetResponseByCode(int code)
        {
            List<ResponseModel> responses = SimplyTravelDAL.Converts.ResponseConvert.ConvertRespnseListToModel(GetDbSet<Responses>().Where(c => c.codeSiteInTrip == code));
            if (responses.Count == 0)
                return null;
            return responses[0];
              
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
