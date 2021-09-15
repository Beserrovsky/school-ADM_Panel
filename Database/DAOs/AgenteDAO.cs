using FelipeB_App3BI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Database
{
    public class AgenteDAO
    {

        public IEnumerable<Agente> Get() 
        {
            return Context.Get<Agente>();
        }

        /*

        public int Count()
        {

            int agentes_count = 0;

            using (Database db = new Database()) 
            {

                MySqlDataReader dr = db.RunAndRead("Select COUNT(*) from Agente");

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        agentes_count = dr.GetInt32(0);
                    }
                }

                dr.Close();

            }

            return agentes_count;
        }

        public Agente Get(string cpf)
        {
            Agente agente = null;

            using (Database db = new Database())
            {
                MySqlDataReader dr = db.RunAndRead($"Select * from agentes_types_view WHERE agentes_types_view.CPF=@cpf", new MySqlParameter[] { new MySqlParameter("cpf", cpf) });

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        agente = new Agente()
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
                            },
                            IsCliente = dr.GetInt32(7)==1,
                            IsFuncionario = dr.GetInt32(8)==1
                       };
                    }
                }

                dr.Close();
            }

            return agente;
        }

        public List<string> GetAllStates() 
        {
            List<string> states_uf = new List<string>() { "RO", "AC", "AM", "RR", "PA", "AP", "TO", "MA", "PI", "CE", "RN", "PB", "PE", "AL", "SE", "BA", "MG", "ES", "RJ", "SP", "PR", "SC", "RS", "MS", "MT", "GO", "DF" };

            // just in case....

            using (Database db = new Database()) 
            {
                MySqlDataReader dr = db.RunAndRead("Select UF from Estado ORDER BY Estado.UF ASC", new MySqlParameter[0]);

                if (dr.HasRows) 
                {
                    states_uf = new List<string>();

                    while (dr.Read()) 
                    {
                        states_uf.Add(dr.GetString(0));
                    }
                }

                dr.Close();

            }

            return states_uf;
        }

        public void Save(Agente agente)
        {

            using (Database db = new Database())
            {
                MySqlDataReader dr = db.RunAndRead($"Select Endereco_ID from Agente WHERE Agente.CPF=@cpf", agente);

                int endereco_id = -1;
                bool update = false;

                if (dr.HasRows) 
                {
                    while (dr.Read()) endereco_id = dr.IsDBNull(0)? -1 : dr.GetInt32(0);
                    update = true;
                }

                dr.Close();

                // Update Endereco
                if (endereco_id!=-1) // Endereco já criado
                {

                    db.Run($"UPDATE Endereco SET Logradouro=@logradouro, Cidade=@cidade, Estado_UF=@estado_uf, Numero=@numero WHERE ID=@ID", agente.Endereco);
                }
                else
                {

                    db.Run("INSERT INTO Endereco(Logradouro, Cidade, Estado_UF, Numero) VALUES (@logradouro, @cidade, @estado_uf, @numero)", agente.Endereco);

                    dr = db.RunAndRead("SELECT MAX(ID) FROM Endereco", new MySqlParameter[0]);

                    if (dr.HasRows) 
                    {
                        while (dr.Read()) 
                        {
                            endereco_id = dr.GetInt32(0);
                        }
                    }

                    dr.Close();
                }

                // Update Agente
                if (update)
                {

                    db.Run($"UPDATE Agente SET Nome=@nome, Telefone=@telefone, Endereco_ID=@endereco_id WHERE CPF=@cpf", agente);
                }
                else
                {

                    db.Run("INSERT INTO Agente(CPF, Nome, Telefone, Endereco_ID) VALUES (@cpf, @nome, @telefone, @endereco_id)", agente);
                }

                ClienteDAO clienteDAO = new ClienteDAO();

                bool is_already_cliente = clienteDAO.Get(agente.CPF) != null;

                if (agente.IsCliente && !is_already_cliente) new ClienteDAO().AddAgente(agente.CPF);
                if (!agente.IsCliente && is_already_cliente) new ClienteDAO().DelAgente(agente.CPF);


                FuncionarioDAO funcionarioDAO = new FuncionarioDAO();

                bool is_already_funcionario = funcionarioDAO.Get(agente.CPF) != null;

                if (agente.IsFuncionario && !is_already_funcionario) new FuncionarioDAO().AddAgente(agente.CPF);
                if (!agente.IsFuncionario && is_already_funcionario) new FuncionarioDAO().DelAgente(agente.CPF);
            }
        }

        public bool Exists(string cpf) 
        {
            bool exists = false;

            using (Database db = new Database()) 
            {
                MySqlDataReader dr = db.RunAndRead("Select * from Agente where Agente.CPF=@cpf", new MySqlParameter[] { new MySqlParameter("@cpf", cpf) });

                exists = dr.HasRows;
            }

            return exists;
        }

        public void Delete(string cpf) 
        {
            using (Database db = new Database())
            {

                MySqlDataReader dr = db.RunAndRead($"Select * from Agente WHERE Agente.CPF=@cpf", new MySqlParameter[] { new MySqlParameter("cpf", cpf) });

                bool valid_agente = dr.HasRows;

                dr.Close();

                if (!valid_agente) throw new Exception("CPF não cadastrado como Agente!");

                db.Run($"DELETE FROM Cliente WHERE Cliente.Agente_CPF=@cpf", new MySqlParameter[] { new MySqlParameter("cpf", cpf) });
                db.Run($"DELETE FROM Funcionario WHERE Funcionario.Agente_CPF=@cpf", new MySqlParameter[] { new MySqlParameter("cpf", cpf) });
                db.Run($"DELETE FROM Agente WHERE Agente.CPF=@cpf", new MySqlParameter[] { new MySqlParameter("cpf", cpf) });
            }
        }
        */
    }
}
