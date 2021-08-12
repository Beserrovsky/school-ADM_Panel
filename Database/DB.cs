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
    class DB : IDisposable
    {
        private readonly MySqlConnection conn;
        private readonly string connectionStr;

        public DB() 
        {
            connectionStr = ConfigurationManager.ConnectionStrings["connection"].ConnectionString; // Dependecy injection from App.config

            conn = new MySqlConnection(connectionStr);
            conn.Open();
        }

        public void Run(string command, MySqlParameter[] parameters)  // Protect against SQL injection
        {
            MySqlCommand cmd = new MySqlCommand(command, conn);
            foreach (MySqlParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }

            cmd.ExecuteNonQuery();
        }

        public object ExecuteScalar(string command, MySqlParameter[] parameters)
        {
            MySqlCommand cmd = new MySqlCommand(command, conn);
            foreach (MySqlParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }

            return cmd.ExecuteScalar();
        }

        public MySqlDataReader RunAndRead(string command, MySqlParameter[] parameters)
        {
            MySqlCommand cmd = new MySqlCommand(command, conn);
            foreach (MySqlParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }

            return cmd.ExecuteReader();
        }

        public void Dispose()
        {
            if (conn.State == ConnectionState.Open) conn.Close();
        }
    }
}
