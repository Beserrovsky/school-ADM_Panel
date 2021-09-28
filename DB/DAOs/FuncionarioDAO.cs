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
            using (Database db = new Database())
            {
                MySqlDataReader dr = db.RunAndRead("SELECT * FROM funcionario WHERE agente_cpf = @id", new MySqlParameter[] { new MySqlParameter("id", ID) });
                bool exists = dr.HasRows;
                dr.Close();
                return exists;
            }
        }

        public string Post(string ID)
        {
            using (Database db = new Database())
            {
                db.Run("INSERT INTO funcionario VALUES(@id)", new MySqlParameter[] { new MySqlParameter("id", ID) });
                return ID;
            }
        }

        public string Delete(string ID)
        {
            using (Database db = new Database())
            {
                db.Run("DELETE FROM funcionario WHERE agente_cpf = @id", new MySqlParameter[] { new MySqlParameter("id", ID) });
                return ID;
            }
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
                    CEP = dr.GetString(3),
                    Estado = dr.GetString(4),
                    Cidade = dr.GetString(5),
                    Logradouro = dr.GetString(6),
                },
                Numero = dr.GetInt32(7)
            };
            return f;
        }
    }
}
