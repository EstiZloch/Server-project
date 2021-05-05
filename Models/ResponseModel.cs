using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
  public  class ResponseModel
    {
        public int CodeResponse { get; set; }
        public Nullable<int> CodeSiteInTrip { get; set; }
        public Nullable<int> Question1 { get; set; }
        public Nullable<int> Question2 { get; set; }
        public Nullable<int> Question3 { get; set; }
        public Nullable<int> Question4 { get; set; }
        public Nullable<int> Question5 { get; set; }
        public string Notes { get; set; }
    }
}
