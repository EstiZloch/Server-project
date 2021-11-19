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
            List<SiteInTripModel> listToAdd = new List<SiteInTripModel>();
            List<SiteInTripModel> returListn = new List<SiteInTripModel>();
            foreach (var v in list)
            {
                listToAdd = site.GetSitesInTripByCodeTrip(v.CodeTrip);
                foreach (var a in listToAdd)
                    returListn.Add(a);
            }
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
        public int UpdateCustomer(CustomerModel c)
        {
            if (GetCustomerById(c.IdCustomer) == null)
                return 0;
            //------------validation 
            UpdateDB<Customers>(SimplyTravelDAL.Converts.CustomerConvert.ConvertCustomerToEF(c));
            return 1;

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
            c = GetCustomerById(c.IdCustomer);
            if (c == null)
                return -1;
                bl.ConfirmPassword(c.IdCustomer, newPassword.ToString());
                SendMail sendMail = new SendMail("SimplyTravel", "SimplyTravelSite@gmail.com", "Simply207");
            string body = "";
            string subject = string.Format("אימות סיסמא למשתמש {0}" , c.NameCustomer) ;
            body += "\nלתשומת לבך, מצורפת סיסמתך החדשה לכניסה למערכת";
            body += string.Format(" :סיסמתך החדשה היא {0}", newPassword);
            //מבצע את השליחה
            bool mailSend = true;
            
            mailSend = sendMail.SendEMail(new MessageGmail()
            {
                sendTo = GetDbSet<Customers>().FirstOrDefault(s=>s.idCustomer==c.IdCustomer).email ,
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            });

            return c.IdCustomer;
        }
        public int CheckPassword(CustomerModel c)
        {
            CustomerModel customer = GetCustomerById(c.IdCustomer);
            if (customer == null)
                return -1;
            if (c.NameCustomer != customer.PasswordCustomer)
                return -1;
            customer.PasswordCustomer = c.PasswordCustomer;
            customer.CheckPassword = c.PasswordCustomer;
            UpdateCustomer(customer);
            return customer.IdCustomer;
        }

    }

}
