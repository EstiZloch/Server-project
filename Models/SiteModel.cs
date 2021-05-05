using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
  public  class SiteModel
    {
        public int CodeSite { get; set; }
        public Nullable<int> CodeSiteKind { get; set; }
        public string NameSite { get; set; }
        public string Address { get; set; }
        public Nullable<int> CodeSub_Region { get; set; }
        public Nullable<int> ExtraLevel { get; set; }
        public Nullable<int> MinAge { get; set; }
        public Nullable<int> MaxAge { get; set; }
        public Nullable<int> MisLiterWater { get; set; }
        public Nullable<bool> Free_notFree { get; set; }
        public Nullable<int> SumToPay { get; set; }
        public Nullable<bool> Car_bus { get; set; }
        public string Coordination { get; set; }
        public Nullable<double> TimeSpend { get; set; }
        public string StatusSite { get; set; }
    }
}
