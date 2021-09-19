using FelipeB_App3BI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace FelipeB_App3BI.DB
{
    public class FuncionarioDAO
    {
        public IEnumerable<FuncionarioModel> Get()
        {
            using (Database db = new Database())
            {
                List<FuncionarioModel> funcionarios = new List<FuncionarioModel>();

                MySqlDataReader dr = db.RunAndRead("Select * from funcionarios_view", new MySqlParameter[0]);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        funcionarios.Add(ReadRecord(dr));
                    }
                }

                dr.Close();

                return funcionarios;
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

        protected FuncionarioModel ReadRecord(MySqlDataReader dr)
        {
            FuncionarioModel f = new FuncionarioModel
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
            return f;
        }
    }
}
