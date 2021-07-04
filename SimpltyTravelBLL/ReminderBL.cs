using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimpltyTravelBLL
{
    class ReminderBL:SimplyTravelBL
    {
        
        public ReminderBL()
        {
           
        }
        //get reminder by id
        private ReminderModel GetReminderById(int id)
        {
            return SimplyTravelDAL.Converts.ReminderConvert.ConvertReminderToModel(GetDbSet<Remainders>().First(c => c.idCustomer == id));
        }
        //sign up function
        public int AddReminder(int id,string des)
        {
            //check if reminder exist in DB
            if (GetReminderById(id) != null)
            {
                //if exist
                return 0;
            }
            ReminderModel c = new ReminderModel() { IdCustomer = id, Describe = des, CodeRemainder=1 };
            if (GetDbSet<Remainders>().ToList().Count > 0)
                c.CodeRemainder = GetDbSet<Remainders>().ToList().Last().codeRemainder + 1;
            //add new custemer to the customers list
            AddToDB<Remainders>(SimplyTravelDAL.Converts.ReminderConvert.ConvertReminderToEF(c));
            return c.CodeRemainder;
            //load the page with his details
        }
        //delete a reminder
        private int DeleteReminder(int id)
        {
            ReminderModel r = GetReminderById(id);
            if (r == null)
                return 1;
            DeleteDB<Remainders>(SimplyTravelDAL.Converts.ReminderConvert.ConvertReminderToEF(r));
            return 0;
        }
        private int UpdateReminder(ReminderModel c)
        {
            int i=0;
            int? j=c.IdCustomer;
            if (j.HasValue)
                i = (int)j;
            if (GetReminderById(i) == null)
                return 0;
            //------------validation 
            UpdateDB<Remainders>(SimplyTravelDAL.Converts.ReminderConvert.ConvertReminderToEF(c));
            return 1;
        }

    }
}
