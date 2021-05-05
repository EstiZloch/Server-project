using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimplyTravelDAL.Converts
{
   public static class ReminderConvert
    {
        public static Remainders ConvertReminderToEF(ReminderModel reminder)
        {
            return new Remainders
            {
                codeRemainder = reminder.CodeRemainder,
            idCustomer = reminder.IdCustomer,
            describe = reminder.Describe
    };
        }
        public static ReminderModel ConvertReminderToModel(Remainders reminder)
        {
            return new ReminderModel
            {
                CodeRemainder = reminder.codeRemainder,
                IdCustomer = reminder.idCustomer,
                Describe = reminder.describe
            };
        }



        public static List<ReminderModel> ConvertReminderListToModel(IEnumerable<Remainders> reminders)
        {
            return reminders.Select(c => ConvertReminderToModel(c)).OrderBy(n => n.CodeRemainder).ToList();
        }
    }
}
