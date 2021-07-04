using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimpltyTravelBLL
{
    class TripBL:SimplyTravelBL
    {
       
        public TripBL()
        {
        }
        //get a trip by id and date
        private TripModel GetTripByIdAndDate(int id,DateTime date)
        {
            return SimplyTravelDAL.Converts.TripConvert.ConvertTripToModel(GetDbSet<Trips>().First(c => c.idCustomer == id && c.dateTrip==date));
        }
        //get all the site in a specific trip
        public List<SiteInTripModel> GetSitesPerTrip(int CodeTrip)
        {
           return SimplyTravelDAL.Converts.SiteInTripConvert.ConvertSiteInTripListToModel(GetDbSet<SitesInTrip>().Where(s => s.codeTrip == CodeTrip).ToList());
        }
        //add a trip
        public int AddTrip(int id, DateTime date)
        {
            //check if trip exist in DB
            if (GetTripByIdAndDate(id,date) != null)
            {
                //if exist
                return 0;
            }
            //if (!Validation.LegalId(id) || !Validation.IsPassword(id, password))
            //    return SimplyTravelBL.Result.IncorrrectDetails;
            //------------validation 
            TripModel c = new TripModel() { IdCustomer = id, DateTrip = date, CodeTrip=1 };
            if (GetDbSet<Trips>().ToList().Count > 0)
                c.CodeTrip = GetDbSet<Trips>().ToList().Last().codeTrip + 1;
            //add new trip to the trips list
            AddToDB<Trips>(SimplyTravelDAL.Converts.TripConvert.ConvertTripToEF(c));
            return c.CodeTrip;
        }
        //delete a trip
        private int DeleteTrip(int id,DateTime date)
        {
            var trip = GetTripByIdAndDate(id,date);
            if (trip == null)
                return 0;
            DeleteDB<Trips>(SimplyTravelDAL.Converts.TripConvert.ConvertTripToEF(trip));
            return 1;
        }
        //update a trip
        private int UpDateTrip(TripModel t)
        {
            int i = 0;
            int? j = t.IdCustomer;
            if (j.HasValue)
                i = (int)j;
            if (GetTripByIdAndDate(i,t.DateTrip.Value) == null)
                return 0;
            //------------validation 
            UpdateDB<Trips>(SimplyTravelDAL.Converts.TripConvert.ConvertTripToEF(t));
            return 1;
        }
    }
}
