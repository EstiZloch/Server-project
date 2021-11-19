using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimpltyTravelBLL
{
   public class ResponseBL:SimplyTravelBL
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
        //get responses by code and id
        public List<ResponseModel> GetResponsesByCodeAndId(int code,int id)
        {
            SiteInTripBL bl = new SiteInTripBL();
            List<SiteInTripModel> sitesInTrip = bl.GetSitesInTripByCodeTrip(code);
            List<ResponseModel> responses = new List<ResponseModel>();
            ResponseModel r;
            foreach (var v in sitesInTrip)
            {
                r = GetResponseByCode(v.CodeSiteInTrip);
                if (r != null)
                    responses.Add(r);
                else
                {
                    r = new ResponseModel() { CodeSiteInTrip = v.CodeSiteInTrip, Question1 = 0, Question2 = 0, Question3 = 0, Question4 = 1, Question5 = 0, Notes = "הכנס הערה" };
                    AddResponse(r);
                    responses.Add(r);
                }
            }
            return responses;
        }
        //add a response
        public int AddResponse(ResponseModel response)
        {
            //check if response exist in DB
            if (GetResponseByCode(response.CodeResponse) != null)
            {
                //if exist
                return 0;
            }
            //add new response to the responses list
            AddToDB<Responses>(SimplyTravelDAL.Converts.ResponseConvert.ConvertResponseToEF(response));
            return 1;
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
        public int UpdateResponse(ResponseModel r)
        {
            if (GetResponseByCode(r.CodeSiteInTrip.Value) == null)
                return 0;
            //------------validation 
            UpdateDB<Responses>(SimplyTravelDAL.Converts.ResponseConvert.ConvertResponseToEF(r));
            return 1;
        }
    }
}
