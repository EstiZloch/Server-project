using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
  public  class Customer
    {
        public string IdCustomer { get; set; }
        public string PasswordCustomer { get; set; }
        public string CheckPassword { get; set; }
        public string Email { get; set; }
        public string NameCustomer { get; set; }
        public int ExtraLevel { get; set; }
        public bool Free_notFree { get; set; }
        public int SumToPay { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public bool Car_bus { get; set; }
        public string StatusCustomer { get; set; }
        public Customer(string id, string pass, string check, string mail, string name,int level,bool free,int sum,int min,int max,bool bus,string status)
        {
            this.IdCustomer = id;
            this.PasswordCustomer = pass;
            this.CheckPassword = check;
            this.Email = mail;
            this.NameCustomer = name;
            this.ExtraLevel = level;
            this.Free_notFree = free;
            this.SumToPay = sum;
            this.MinAge = min;
            this.MaxAge = max;
            this.Car_bus = bus;
            this.StatusCustomer = status;
    }
        public Customer(string id,string password)
        { 
            this.IdCustomer = id;
            this.PasswordCustomer = password;
            this.CheckPassword = null;
            this.Email = null;
            this.NameCustomer = null;
            this.ExtraLevel = 0;
            this.Free_notFree = false;
            this.SumToPay = 0;
            this.MinAge = 0;
            this.MaxAge = 0;
            this.Car_bus = false;
            this.StatusCustomer = null;
        }
    }
}
