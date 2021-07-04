using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class SiteToDisplay
    {
        public int CodeSite { get; set; }
        public string NameSiteKind { get; set; }
        public string NameSite { get; set; }
        public string Adress { get; set; }
        public string NameRegion { get; set; }
        public string NameSub_Region { get; set; }
        public int ExtraLevel { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public int MisLiterWater { get; set; }
        public string Free_notFree { get; set; }
        public string Car_bus { get; set; }
        public double TimeSpend { get; set; }
        public string StatusSite { get; set; }
    }
}
