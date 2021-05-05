using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
  public  class ReminderModel
    {
        public int CodeRemainder { get; set; }
        public Nullable<int> IdCustomer { get; set; }
        public string Describe { get; set; }
    }
}
