using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Timers;
using Models.Models;

namespace SimpltyTravelBLL
{
    public class CustomerBL : SimplyTravelBL
    {
        public CustomerBL()
        {

        }
        //get all the trips to a specific customer
        public List<TripModel> GetTripsPerCustomer(int id)
        {
            return SimplyTravelDAL.Converts.TripConvert.ConvertTripListToModel(GetDbSet<Trips>().Where(s => s.idCustomer == id));
        }
        //get all the sites that a specific customer was there
        public List<SiteInTripModel> GetSitesPerCustomer(int id)
        {
            SiteInTripBL site = new SiteInTripBL();
            var list = this.GetTripsPerCustomer(id).OrderByDescending(c => c.DateTrip);
            List<SiteInTripModel> returListn = new List<SiteInTripModel>();
            foreach (var v in list)
                returListn.Add(site.GetSitesInTripByCodeTrip(v.CodeTrip));
            return returListn;
        }
        //get a customer by id
        public CustomerModel GetCustomerById(int id)
        {
            List<CustomerModel> customer = SimplyTravelDAL.Converts.CustomerConvert.ConvertCustomerListToModel(GetDbSet<Customers>());
            if (customer != null)
                return customer.Find(c => c.IdCustomer == id);
            return null;
        }
        //sign up function
        public int SignUP(CustomerModel customerNew)
        {
            var customer = GetCustomerById(customerNew.IdCustomer);
            //if the customer doesn't exist in the DB
            if (customer == null)
            {
                AddToDB<Customers>(SimplyTravelDAL.Converts.CustomerConvert.ConvertCustomerToEF(customerNew));
            }
            else
            //if exist, check if the password is not correct return 0
            {
                if (!customer.PasswordCustomer.Equals(customerNew.PasswordCustomer))
                    return 0;
            }
            return customerNew.IdCustomer;
        }
        //sign in function
        public int SignIn(int id, string password)
        {
            var customer = GetCustomerById(id);
            //check if in the user is in the db and his password is correct
            if (customer != null && customer.PasswordCustomer.Equals(password))
                return customer.IdCustomer;
            else
                //if he doesn't in the db-return -1
                if (customer == null)
                return -1;
            else
                //if the password doesn't correct
                return 0;
        }
        //change status to a customer
        private string ChangeStatusCustomer(int id, string status)
        {
            var customer = this.GetCustomerById(id);
            string statusRet = customer.StatusCustomer;
            customer.StatusCustomer = status;
            UpdateDB<Customers>(SimplyTravelDAL.Converts.CustomerConvert.ConvertCustomerToEF(customer));
            return statusRet;
        }
        //delete a customer
        private SimplyTravelBL.Result DeleteCustomer(int id)
        {
            var customer = GetCustomerById(id);
            if (customer == null)
                return SimplyTravelBL.Result.NotFound;
            DeleteDB<Customers>(SimplyTravelDAL.Converts.CustomerConvert.ConvertCustomerToEF(customer));
            return SimplyTravelBL.Result.Found;
        }
        //verify password and mail
        private SimplyTravelBL.Result VerifyPasswordWithMail(int id, string mail)
        {
            var customer = GetCustomerById(id);
            if (customer == null)
                return SimplyTravelBL.Result.NotFound;
            if (customer.Email == mail)
                return SimplyTravelBL.Result.Found;
            return SimplyTravelBL.Result.IncorrrectDetails;
        }
        private SimplyTravelBL.Result UpdateCustomer(CustomerModel c)
        {
            if (GetCustomerById(c.IdCustomer) == null)
                return SimplyTravelBL.Result.NotFound;
            //------------validation 
            UpdateDB<Customers>(SimplyTravelDAL.Converts.CustomerConvert.ConvertCustomerToEF(c));
            return SimplyTravelBL.Result.Found;
        }
        public void ConfirmPassword(int id, string password)
        {
            CustomerModel c;
            using (SimplyTravelEntitiesNew db = new SimplyTravelEntitiesNew())
            {
                c = SimplyTravelDAL.Converts.CustomerConvert.ConvertCustomerToModel(db.Customers.FirstOrDefault(d => d.idCustomer == id));
                c.PasswordCustomer = password;
                c.CheckPassword = password;
                UpdateCustomer(c);
            }
        }
        public int SendEmail(CustomerModel c)
        {
                Random rand = new Random();
                int newPassword = rand.Next(111111, 999999);
                SimpltyTravelBLL.CustomerBL bl = new SimpltyTravelBLL.CustomerBL();
                bl.ConfirmPassword(c.IdCustomer, newPassword.ToString());
                SendMail sendMail = new SendMail("SimplyTravel","Ester0556708556@gmail.com","");
            string body = "";
            string subject = string.Format(" אימות סיסמא למשתמש {0}" , c.NameCustomer) ;
            body += "\nלתשומת לבך, מצורפת סיסמתך החדשה לכניסה למערכת";
            body += string.Format(" :סיסמתך החדשה היא {0}", newPassword);
            //מבצע את השליחה
            bool mailSend = true;
            mailSend = sendMail.SendEMail(new MessageGmail()
            {
                sendTo = c.Email,
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            });

            return 0;
        }
    }

}
