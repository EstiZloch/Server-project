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
    //post- כל מה שעושה שינויים בדטה בייס
    //get-מה שלא עושה שינויים בדטה בייס
  //  [Route("API/[Controller]")]
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        CustomerBL customerBL = new CustomerBL();
        [AcceptVerbs("GET", "POST")]
        [Route("signIn/{id}/{password}")] 
        [HttpGet]
        //access to the user's account.
        public int signIn(int id,string password)
    {
             return customerBL.SignIn(id,password);
    }
        [AcceptVerbs("GET", "POST")]
        [Route("getDetails/{id}")]
        [HttpGet]
        //access to the user's account.
        public CustomerModel getDetails(int id)
        {
            return customerBL.GetCustomerById(id);
        }
        [AcceptVerbs("GET", "POST")]
        [Route("signUp")]
        [HttpPost]
        public int signUp(CustomerModel c)
        {
           return this.AddNewCustomer(c);
        }
        public int AddNewCustomer(CustomerModel c)
        {
            return customerBL.SignUP(c);
        }
        [AcceptVerbs("GET", "POST")]
        [Route("ConfirmPassword")]
        [HttpPost]
        public int ConfirmPassword(CustomerModel c)
        {
           return customerBL.SendEmail(c);
        }
        [AcceptVerbs("GET", "POST")]
        [Route("UpdatePassword")]
        [HttpPost]
        public void UpdatePassword(CustomerModel c)
        {
             customerBL.ConfirmPassword(c.IdCustomer,c.PasswordCustomer);
        }
    }
}
