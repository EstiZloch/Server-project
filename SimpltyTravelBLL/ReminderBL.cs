using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimpltyTravelBLL
{
    class ReminderBL
    {
        DBConnection db;
        SimplyTravelBL s = new SimplyTravelBL();
        public ReminderBL()
        {
            db = new DBConnection();
        }
        //get reminder by id
        private ReminderModel GetReminderById(int id)
        {
            return SimplyTravelDAL.Converts.ReminderConvert.ConvertReminderToModel(db.GetDbSet<Remainders>().First(c => c.idCustomer == id));
        }
        //sign up function
        public SimplyTravelBL.Result SignUP(int id,string des)
        {
            //check if reminder exist in DB
            if (GetReminderById(id) != null)
            {
                //if exist
                return SimplyTravelBL.Result.IncorrrectDetails;
            }
            if (!Validation.LegalId(id) || !Validation.IsHebrew(des))
                return SimplyTravelBL.Result.IncorrrectDetails;
            //------------validation 
            ReminderModel c = new ReminderModel() { IdCustomer = id, Describe = des, CodeRemainder=1 };
            if (db.GetDbSet<Remainders>().ToList().Count > 0)
                c.CodeRemainder = db.GetDbSet<Remainders>().ToList().Last().codeRemainder + 1;
            //add new custemer to the customers list
            s.AddToDB<Remainders>(SimplyTravelDAL.Converts.ReminderConvert.ConvertReminderToEF(c));
            return SimplyTravelBL.Result.Found;
            //load the page with his details
        }
        //delete a reminder
        private SimplyTravelBL.Result DeleteReminder(int id)
        {
            ReminderModel r = GetReminderById(id);
            if (r == null)
                return SimplyTravelBL.Result.NotFound;
            s.DeleteDB<Remainders>(SimplyTravelDAL.Converts.ReminderConvert.ConvertReminderToEF(r));
            return SimplyTravelBL.Result.Found;
        }
        private SimplyTravelBL.Result UpdateReminder(ReminderModel c)
        {
            int i=0;
            int? j=c.IdCustomer;
            if (j.HasValue)
                i = (int)j;
            if (GetReminderById(i) == null)
                return SimplyTravelBL.Result.NotFound;
            //------------validation 
            s.UpdateDB<Remainders>(SimplyTravelDAL.Converts.ReminderConvert.ConvertReminderToEF(c));
            return SimplyTravelBL.Result.Found;
        }

    }
}
