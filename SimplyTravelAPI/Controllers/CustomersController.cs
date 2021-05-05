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
    //post-כל מה שעושה שינויים בדטה בייס
    //get-מה שלא עושה שינויים בדטה בייס
  //  [Route("API/[Controller]")]
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        CustomerBL customerBL = new CustomerBL();
        [AcceptVerbs("GET", "POST", "OPTIONS", "HEAD")]
        [Route("signIn/{id}/{password}")] 
        [HttpGet]
        //access to the user's account.
        public int signIn(int id,string password)
    {
             return customerBL.SignIn(id,password);
    }
        
        [AcceptVerbs("GET", "POST", "OPTIONS","HEAD")]
        [Route("signUp")]
        [HttpPost]
        public int signUp( CustomerModel c)
        {
            c.CheckPassword = c.PasswordCustomer;
            c.MaxAge = 120;
            c.MinAge = 0;
            c.ExtraLevel = 1;
            c.Free_notFree = true;
            c.Car_bus = false;
            c.StatusCustomer = "Active";
            c.SumToPay = 1000;
           return this.AddNewCustomer(c);
        }
        public int AddNewCustomer(CustomerModel c)
        {
            return customerBL.SignUP(c);
        }
    }
}
