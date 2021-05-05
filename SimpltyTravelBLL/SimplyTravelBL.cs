using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimpltyTravelBLL
{
   public class SimplyTravelBL
    {
        DBConnection db;
        List<Customers> list;
        public SimplyTravelBL()
        {
            db = new DBConnection();
        }
        public enum Result
        {
            IncorrrectDetails,
            NotFound,
            Found
        }
        public void AddToDB<T>(T entity) where T:class
        {
            db.Execute<T>(entity, DBConnection.ExecuteActions.Insert);
        }
        public void DeleteDB<T>(T entity) where T : class
        {
            db.Execute<T>(entity, DBConnection.ExecuteActions.Delete);
        }
        public void UpdateDB<T>(T entity) where T : class
        {
            db.Execute<T>(entity, DBConnection.ExecuteActions.Update);
        }
    }
}
