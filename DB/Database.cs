﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelipeB_App3BI.DB
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

        public void Run(string command, MySqlParameter[] parameters)  // Protect against SQL injection
        {
            MySqlCommand cmd = new MySqlCommand(command, conn);
            foreach (MySqlParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }

            cmd.ExecuteNonQuery();
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
