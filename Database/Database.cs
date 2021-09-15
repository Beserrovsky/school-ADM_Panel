using FelipeB_App3BI.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    class Database : IDisposable
    {
        private readonly MySqlConnection conn;
        private readonly string connectionStr;

        public Database() 
        {
            connectionStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            conn = new MySqlConnection(connectionStr);
            conn.Open();
        }

        public void Run(string command)
        {
            MySqlCommand cmd = new MySqlCommand(command, conn);
            cmd.ExecuteNonQuery();
        }

        public void Run<T>(string command, T obj) 
        {
            MySqlParameter[] parameters = GetSqlParameters(obj);

            MySqlCommand cmd = new MySqlCommand(command, conn);
            foreach (MySqlParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }

            cmd.ExecuteNonQuery();
        }

        public MySqlDataReader RunAndRead(string command)
        {
            MySqlCommand cmd = new MySqlCommand(command, conn);
            return cmd.ExecuteReader();
        }

        public MySqlDataReader RunAndRead<T>(string command, T obj)
        {
            MySqlParameter[] parameters = GetSqlParameters(obj);

            MySqlCommand cmd = new MySqlCommand(command, conn);
            foreach (MySqlParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }

            return cmd.ExecuteReader();
        }

        private MySqlParameter[] GetSqlParameters<T>(T model)
        {
            if (model == null) throw new NullReferenceException();

            var props = model.GetType().GetProperties();

            MySqlParameter[] parameters = new MySqlParameter[props.Length];

            for (int i = 0; i < props.Length; i++)
            {
                parameters[i] = new MySqlParameter(props[i].Name.ToLower(), props[i].GetValue(model));
            }

            return parameters;
        }

        public void Dispose()
        {
            if (conn.State == ConnectionState.Open) conn.Close();
        }
    }
}
