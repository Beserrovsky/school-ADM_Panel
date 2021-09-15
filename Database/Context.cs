using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Database
{
    public class Context
    {
        public static IEnumerable<T> Get<T>(string condition=null) where T : new()
        {
            Type type = typeof(T);
            List<T> result = new List<T>();

            using (Database db = new Database())
            {
                MySqlDataReader dr = db.RunAndRead($"Select * from {type.Name} {condition ?? ""}", type);

                var props = type.GetProperties();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        T item = new T();
                        for (int i = 0; i < props.Length; i++)
                        {
                            try
                            {
                                props[i].SetValue(item, dr.GetValue(dr.GetOrdinal(props[i].Name)));
                            }
                            catch (IndexOutOfRangeException e) { Debug.WriteLine(e); }
                        }
                        result.Add(item);
                    }
                }

                dr.Close();
            }

            return result;
        }

        public static T Patch<T>(string condition, T obj) where T : new()
        {
            using (Database db = new Database())
            {
                MySqlDataReader dr = db.RunAndRead($"UDATE {} {condition ?? ""}", obj);

                var props = type.GetProperties();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        result = dr.GetInst32(0);
                    }
                }

                dr.Close();
            }

            return obj;
        }

        public static T Post<T>(string condition, T obj) where T : new()
        {
            using (Database db = new Database())
            {
                MySqlDataReader dr = db.RunAndRead($"UDATE {} {condition ?? ""}", obj);

                var props = type.GetProperties();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        result = dr.GetInt32(0);
                    }
                }

                dr.Close();
            }

            return obj;
        }

        public static T Delete<T>(string condition, T obj) where T : new()
        {
            using (Database db = new Database())
            {
                MySqlDataReader dr = db.RunAndRead($"UDATE {} {condition ?? ""}", obj);

                var props = type.GetProperties();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        result = dr.GetInt32(0);
                    }
                }

                dr.Close();
            }

            public static int Count<T>(string condition = null) where T : new()
        {
            Type type = typeof(T);
            int result = 0;

            using (Database db = new Database())
            {
                MySqlDataReader dr = db.RunAndRead($"Select COUNT(*) from {type.Name} {condition ?? ""}", type);

                var props = type.GetProperties();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        result = dr.GetInt32(0);
                    }
                }

                dr.Close();
            }

            return result;
        }
    }
}