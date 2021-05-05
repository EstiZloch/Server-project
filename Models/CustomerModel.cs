using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class CustomerModel
    {
        public int IdCustomer { get; set; }
        public string PasswordCustomer { get; set; }
        public string CheckPassword { get; set; }
        public string Email { get; set; }
        public string NameCustomer { get; set; }
        public Nullable<int> ExtraLevel { get; set; }
        public Nullable<bool> Free_notFree { get; set; }
        public Nullable<int> SumToPay { get; set; }
        public Nullable<int> MinAge { get; set; }
        public Nullable<int> MaxAge { get; set; }
        public Nullable<bool> Car_bus { get; set; }
        public string StatusCustomer { get; set; }
    }
}
