using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimpltyTravelBLL
{
    class TripBL
    {
        DBConnection db;
        SimplyTravelBL s = new SimplyTravelBL();
        public TripBL()
        {
            db = new DBConnection();
        }
        //get a trip by id and date
        private TripModel GetTripByIdAndDate(int id,DateTime date)
        {
            return SimplyTravelDAL.Converts.TripConvert.ConvertTripToModel(db.GetDbSet<Trips>().First(c => c.idCustomer == id && c.dateTrip==date));
        }
        //get all the site in a specific trip
        public List<SiteInTripModel> GetSitesPerTrip(int CodeTrip)
        {
           return SimplyTravelDAL.Converts.SiteInTripConvert.ConvertSiteInTripListToModel(db.GetDbSet<SitesInTrip>().Where(s => s.codeTrip == CodeTrip).ToList());
        }
        //add a trip
        public SimplyTravelBL.Result AddTrip(int id, DateTime date)
        {
            //check if trip exist in DB
            if (GetTripByIdAndDate(id,date) != null)
            {
                //if exist
                return SimplyTravelBL.Result.NotFound;
            }
            //if (!Validation.LegalId(id) || !Validation.IsPassword(id, password))
            //    return SimplyTravelBL.Result.IncorrrectDetails;
            //------------validation 
            TripModel c = new TripModel() { IdCustomer = id, DateTrip = date, CodeTrip=1 };
            if (db.GetDbSet<Trips>().ToList().Count > 0)
                c.CodeTrip = db.GetDbSet<Trips>().ToList().Last().codeTrip + 1;
            //add new trip to the trips list
            s.AddToDB<Trips>(SimplyTravelDAL.Converts.TripConvert.ConvertTripToEF(c));
            return SimplyTravelBL.Result.Found;
        }
        //delete a trip
        private SimplyTravelBL.Result DeleteCustomer(int id,DateTime date)
        {
            var trip = GetTripByIdAndDate(id,date);
            if (trip == null)
                return SimplyTravelBL.Result.NotFound;
            s.DeleteDB<Trips>(SimplyTravelDAL.Converts.TripConvert.ConvertTripToEF(trip));
            return SimplyTravelBL.Result.Found;
        }
        //update a trip
        private SimplyTravelBL.Result UpDateTrip(TripModel t)
        {
            int i = 0;
            int? j = t.IdCustomer;
            if (j.HasValue)
                i = (int)j;
            if (GetTripByIdAndDate(i,t.DateTrip.Value) == null)
                return SimplyTravelBL.Result.NotFound;
            //------------validation 
            s.UpdateDB<Trips>(SimplyTravelDAL.Converts.TripConvert.ConvertTripToEF(t));
            return SimplyTravelBL.Result.Found;
        }
    }
}
