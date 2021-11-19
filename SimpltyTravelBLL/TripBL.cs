using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimpltyTravelBLL
{
   public class TripBL:SimplyTravelBL
    {
       
        public TripBL()
        {
        }
        //get trips to a customer
        public List<TripModel> GetAllTripsToCustomer(int id)
        {
            List<TripModel> trips = SimplyTravelDAL.Converts.TripConvert.ConvertTripListToModel(GetDbSet<Trips>());
            if (trips != null)
                return trips.Where(c => c.IdCustomer == id).ToList();
            return null;
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
        public int AddTrip(TripModel t)
        {
            SiteBL siteBl = new SiteBL();
            SiteInTripBL siteInTripBl = new SiteInTripBL();
            TripModel c = new TripModel() { IdCustomer = t.IdCustomer, DateTrip = DateTime.Today,NameTrip=t.NameTrip };
            //add new trip to the trips list
            AddToDB<Trips>(SimplyTravelDAL.Converts.TripConvert.ConvertTripToEF(c));
            return GetDbSet<Trips>()[GetDbSet<Trips>().Count - 1].codeTrip; 
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
