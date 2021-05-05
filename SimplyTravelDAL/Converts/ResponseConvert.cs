using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimplyTravelDAL.Converts
{
  public static  class ResponseConvert
    {
        public static Responses ConvertResponseToEF(ResponseModel response)
        {
            return new Responses
            {
        codeResponse=response.CodeResponse,
        codeSiteInTrip=response.CodeSiteInTrip,
        question1=response.Question1,
        question2 =response.Question2,
        question3 =response.Question3,
       question4 =response.Question4,
        question5 =response.Question5,
        notes =response.Notes

    };
        }
        public static ResponseModel ConvertResponseToModel(Responses response)
        {
            return new ResponseModel
            {
                CodeResponse = response.codeResponse,
                CodeSiteInTrip = response.codeSiteInTrip,
                Question1 = response.question1,
                Question2 = response.question2,
                Question3 = response.question3,
                Question4 = response.question4,
                Question5 = response.question5,
                Notes = response.notes
            };
        }



        public static List<ResponseModel> ConvertRespnseListToModel(IEnumerable<Responses> responses)
        {
            return responses.Select(c => ConvertResponseToModel(c)).OrderBy(n => n.CodeResponse).ToList();
        }
    }
}
