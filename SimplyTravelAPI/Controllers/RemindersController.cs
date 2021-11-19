using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SimpltyTravelBLL;
using Models;
namespace SimplyTravelGui.Controllers
{
    [RoutePrefix("api/reminders")]
    public class RemindersController : ApiController
    {
        ReminderBL r = new ReminderBL();
        [AcceptVerbs("GET", "POST")]
        [Route("getReminders/{id}")]
        [HttpGet]
        public List<ReminderModel> getReminders(int id)
        {
            return r.GetRemindersById(id);
        }
        [AcceptVerbs("GET", "POST")]
        [Route("update")]
        [HttpPost]
        public int update(ReminderDisplay[] arr)
        {
            return r.checkUpdate(arr);
        }


    }
}
