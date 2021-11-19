using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimpltyTravelBLL
{
   public class ReminderBL:SimplyTravelBL
    {
       static int id;
        public ReminderBL()
        {
           
        }
        //get reminders by id
        public List<ReminderModel> GetRemindersById(int id)
        {
            ReminderBL.id = id;
            List<ReminderModel>r= SimplyTravelDAL.Converts.ReminderConvert.ConvertReminderListToModel(GetDbSet<Remainders>().Where(c => c.idCustomer == id));
            r = r.OrderBy(f => DateTime.Parse(f.Describe.Split('*')[1])).ToList();
            return r;
        }

        //sign up function
        public int AddReminder(ReminderModel r)
        {
            AddToDB<Remainders>(SimplyTravelDAL.Converts.ReminderConvert.ConvertReminderToEF(r));
            return r.CodeRemainder;
        }

        public int checkUpdate(ReminderDisplay[] arr)
        {
            ReminderModel model, toUpdate,toAdd,toDelete;
            List<ReminderModel> r = GetRemindersById(ReminderBL.id);
            foreach(var v in arr)
            {
                List<int> check = r.Select(f => f.CodeRemainder).ToList();
                if(check.Contains(v.CodeRemainder))
                {

                    model = r.FirstOrDefault(f => f.CodeRemainder == v.CodeRemainder);
                    toUpdate = new ReminderModel() { CodeRemainder = model.CodeRemainder, Describe = v.Describe + "*" + v.Date, IdCustomer = model.IdCustomer };
                    UpdateReminder(toUpdate);
                }
                else
                {
                    toAdd = new ReminderModel() { CodeRemainder = v.CodeRemainder, Describe = v.Describe + "*" + v.Date, IdCustomer = id };
                    AddReminder(toAdd);
                }
            }
            foreach (var v in r)
            {
                List<int> check = arr.Select(f => f.CodeRemainder).ToList();
                if (!check.Contains(v.CodeRemainder))
                {
                    toDelete = new ReminderModel() { CodeRemainder = v.CodeRemainder, Describe = v.Describe, IdCustomer = v.IdCustomer };
                    DeleteReminder(toDelete);
                }
            }
            return 1;
        }

            //delete a reminder
        private void DeleteReminder(ReminderModel r)
            {
            DeleteDB<Remainders>(SimplyTravelDAL.Converts.ReminderConvert.ConvertReminderToEF(r));
            }
            private void UpdateReminder(ReminderModel c)
        {
            UpdateDB<Remainders>(SimplyTravelDAL.Converts.ReminderConvert.ConvertReminderToEF(c));
        }

    }
}
