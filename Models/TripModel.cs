using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class TripModel
    {
        public int CodeTrip { get; set; }
        public Nullable<int> IdCustomer { get; set; }
        public Nullable<System.DateTime> DateTrip { get; set; }
        public string NameTrip { get; set; }
    }
}
