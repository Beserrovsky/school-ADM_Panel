using FelipeB_App3BI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class FuncionarioDAO
    {

        public List<FuncionarioModel> GetAll()
        {
            List<FuncionarioModel> funcionarios = new List<FuncionarioModel>();

            using (DB db = new DB())
            {
                MySqlDataReader dr = db.RunAndRead($"Select * from funcionarios_view", new MySqlParameter[0]);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        funcionarios.Add(new FuncionarioModel()
                        {
                            CPF = dr.GetString(0),
                            Nome = dr.GetString(1),
                            Telefone = dr.GetString(2),
                            Endereco = new Endereco()
                            {
                                Logradouro = dr.GetString(3),
                                Numero = dr.GetInt32(4),
                                Cidade = dr.GetString(5),
                                Estado = dr.GetString(6),
                            }
                        });
                    }
                }

                dr.Close();
            }

            return funcionarios;
        }

        public int Count()
        {
            int funcionarios_count = 0;

            using (DB db = new DB())
            {
                MySqlDataReader dr = db.RunAndRead($"Select COUNT(*) from funcionarios_view", new MySqlParameter[0]);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        funcionarios_count = dr.GetInt32(0);
                    }
                }

                dr.Close();
            }

            return funcionarios_count;
        }

        public FuncionarioModel Get(string cpf)
        {
            FuncionarioModel funcionario = null;

            using (DB db = new DB())
            {
                MySqlDataReader dr = db.RunAndRead($"Select * from funcionarios_view WHERE funcionarios_view.CPF=@cpf", new MySqlParameter[] { new MySqlParameter("cpf", cpf) });

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        funcionario = new FuncionarioModel()
                        {
                            CPF = dr.GetString(0),
                            Nome = dr.GetString(1),
                            Telefone = dr.GetString(2),
                            Endereco = new Endereco()
                            {
                                Logradouro = dr.GetString(3),
                                Numero = dr.GetInt32(4),
                                Cidade = dr.GetString(5),
                                Estado = dr.GetString(6),
                            }
                        };
                    }
                }

                dr.Close();
            }

            return funcionario;
        }

        public void AddAgente(AgenteModel agente)
        {
            using (DB db = new DB())
            {
                MySqlDataReader dr = db.RunAndRead($"Select * from funcionarios_view WHERE funcionarios_view.CPF=@cpf", new MySqlParameter[] { new MySqlParameter("cpf", agente.CPF) });

                bool already_created = dr.HasRows;

                dr.Close();

                if (already_created) return;

                db.Run($"INSERT INTO Funcionario VALUES (@cpf)", new MySqlParameter[] { new MySqlParameter("cpf", agente.CPF) });
            }
        }

        public void DelAgente(AgenteModel agente)
        {
            using (DB db = new DB())
            {
                MySqlDataReader dr = db.RunAndRead($"Select * from funcionarios_view WHERE funcionarios_view.CPF=@cpf", new MySqlParameter[] { new MySqlParameter("cpf", agente.CPF) });

                bool already_created = dr.HasRows;

                dr.Close();

                if (!already_created) return;

                db.Run($"DELETE FROM Funcionario WHERE Funcionario.CPF=@cpf", new MySqlParameter[] { new MySqlParameter("cpf", agente.CPF) });
            }
        }
    }
}
