using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimplyTravelDAL.Converts
{
  public static  class TripConvert
    {
        public static Trips ConvertTripToEF(TripModel trip)
        {
            return new Trips
            {
        codeTrip =trip.CodeTrip,
        idCustomer =trip.IdCustomer,
        dateTrip=trip.DateTrip,
        nameTrip=trip.NameTrip
    };
        }
        public static TripModel ConvertTripToModel(Trips trip)
        {
            return new TripModel
            {
                CodeTrip = trip.codeTrip,
                IdCustomer = trip.idCustomer,
                DateTrip = trip.dateTrip,
                NameTrip = trip.nameTrip
            };
        }



        public static List<TripModel> ConvertTripListToModel(IEnumerable<Trips> trips)
        {
            return trips.Select(c => ConvertTripToModel(c)).OrderBy(n => n.CodeTrip).ToList();
        }
    }
}
