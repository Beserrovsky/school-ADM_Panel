using FelipeB_App3BI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace FelipeB_App3BI.DB
{
    public class ClienteDAO
    {
        public IEnumerable<ClienteModel> Get() 
        {
            using (Database db = new Database())
            {
                List<ClienteModel> clientes = new List<ClienteModel>();

                MySqlDataReader dr = db.RunAndRead("Select * from clientes_view", new MySqlParameter[0]);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        clientes.Add(ReadRecord(dr));
                    }
                }

                dr.Close();

                return clientes;
            }
        }

        public bool Exists(string ID)
        {
            throw new NotImplementedException();
        }

        public string Post(string ID)
        {
            throw new NotImplementedException();
        }

        public string Delete(string ID)
        {
            throw new NotImplementedException();
        }

        protected ClienteModel ReadRecord(MySqlDataReader dr)
        {
            ClienteModel c = new ClienteModel
            {
                CPF = dr.GetString(0),
                Nome = dr.GetString(1),
                Telefone = dr.GetString(2),
                Endereco = new Endereco()
                {
                    Estado = dr.GetString(3),
                    Cidade = dr.GetString(4),
                    Logradouro = dr.GetString(5),
                    Numero = dr.GetInt32(6)
                }
            };
            return c;
        }
    }
}
