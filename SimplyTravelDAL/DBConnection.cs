using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using Models;
namespace SimplyTravelDAL
{
    public class DBConnection
    {
        public DBConnection()
        {

        }
        public List<T> GetDbSet<T>() where T : class
        {
            using (SimplyTravelEntitiesNew s = new SimplyTravelEntitiesNew())
            {
                return s.Set<T>().ToList();
            }
        }
        public enum ExecuteActions
        {
            Insert,
            Update,
            Delete
        }
       
        public void Execute<T>(T entity, ExecuteActions exAction) where T : class
        {
            try
            {
                using (SimplyTravelEntitiesNew s = new SimplyTravelEntitiesNew())
                {
                    var model = s.Set<T>();
                    switch (exAction)
                    {
                        case ExecuteActions.Insert:
                            model.Add(entity);
                            break;
                        case ExecuteActions.Update:
                            model.Attach(entity);
                            s.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                            break;
                        case ExecuteActions.Delete:
                            model.Attach(entity);
                            s.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                            break;
                        default:
                            break;
                    }
                    s.SaveChanges();
                }
            }
            catch(Exception e)
            { }
        }
        //private object[] GetKeys<T>(T entity, SimplyTravelEntities context) where T : class
        //{
        //    ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;
        //    ObjectSet<T> set = objectContext.CreateObjectSet<T>();
        //    var keyNames = set.EntitySet.ElementType
        //                                                .KeyMembers
        //                                                .Select(k => k.Name).ToArray();
        //    Type type = typeof(T);

        //    object[] keys = new object[keyNames.Length];
        //    for (int i = 0; i < keyNames.Length; i++)
        //    {
        //        keys[i] = type.GetProperty(keyNames[i]).GetValue(entity, null);
        //    }
        //    return keys;
        //}

    }
}