using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using SimplyTravelDAL;
namespace SimplyTravelDAL.Converts
{
    public static class CustomerConvert
    {
        public static Customers ConvertCustomerToEF(CustomerModel customer)
        {
            return new Customers
            {
                idCustomer = customer.IdCustomer,
                passwordCustomer = customer.PasswordCustomer,
                checkPassword = customer.CheckPassword,
                email = customer.Email,
                nameCustomer = customer.NameCustomer,
                extraLevel = customer.ExtraLevel,
                free_notFree = customer.Free_notFree,
                sumToPay = customer.SumToPay,
                minAge = customer.MinAge,
                maxAge = customer.MaxAge,
                car_bus = customer.Car_bus,
                statusCustomer = customer.StatusCustomer
            };
        }
        public static CustomerModel ConvertCustomerToModel(Customers customer)
        {
            return new CustomerModel
            {
                IdCustomer = customer.idCustomer,
                PasswordCustomer = customer.passwordCustomer,
                CheckPassword = customer.checkPassword,
                Email = customer.email,
                NameCustomer = customer.nameCustomer,
                ExtraLevel = customer.extraLevel,
                Free_notFree = customer.free_notFree,
                SumToPay = customer.sumToPay,
                MinAge = customer.minAge,
                MaxAge = customer.maxAge,
                Car_bus = customer.car_bus,
                StatusCustomer = customer.statusCustomer
            };
        }



        public static List<CustomerModel> ConvertCustomerListToModel(IEnumerable<Customers> customers)
        {
            return customers.Select(c => ConvertCustomerToModel(c)).OrderBy(n => n.IdCustomer).ToList();
        }
    }
}

