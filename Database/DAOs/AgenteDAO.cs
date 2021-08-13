﻿using FelipeB_App3BI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class AgenteDAO
    {

        public List<AgenteModel> GetAll() 
        {
            List<AgenteModel> agentes = new List<AgenteModel>();

            using (DB db = new DB())
            {
                MySqlDataReader dr = db.RunAndRead($"Select * from agentes_types_view", new MySqlParameter[0]);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        AgenteModel agente = new AgenteModel()
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
                            IsCliente = dr.GetInt32(7) == 1,
                            IsFuncionario = dr.GetInt32(8) == 1
                        };

                        agentes.Add(agente);
                    }
                }

                dr.Close();
            }

            return agentes;
        }

        public int Count()
        {

            int agentes_count = 0;

            using (DB db = new DB()) 
            {

                MySqlDataReader dr = db.RunAndRead("Select COUNT(*) from Agente", new MySqlParameter[0]);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        agentes_count = dr.GetInt32(0);
                    }
                }

            }

            return agentes_count;
        }

        public AgenteModel Get(string cpf)
        {
            AgenteModel agente = null;

            using (DB db = new DB())
            {
                MySqlDataReader dr = db.RunAndRead($"Select * from agentes_types_view WHERE agentes_types_view.CPF=@cpf", new MySqlParameter[] { new MySqlParameter("cpf", cpf) });

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        agente = new AgenteModel()
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

        public void Save(AgenteModel agente)
        {

            using (DB db = new DB())
            {
                MySqlDataReader dr = db.RunAndRead($"Select Endereco_ID from Agente WHERE Agente.CPF=@cpf", new MySqlParameter[] { new MySqlParameter("cpf", agente.CPF) });

                int endereco_id = -1;
                bool created = false;

                if (dr.HasRows) 
                {
                    created = true;
                    while (dr.Read()) endereco_id = dr.IsDBNull(0)? -1 : dr.GetInt32(0);
                }

                dr.Close();

                MySqlParameter[] endereco_parameters = new MySqlParameter[]
                {
                    new MySqlParameter("ID", endereco_id),
                    new MySqlParameter("logradouro", agente.Endereco.Logradouro),
                    new MySqlParameter("cidade", agente.Endereco.Cidade),
                    new MySqlParameter("estado_uf", agente.Endereco.Estado),
                    new MySqlParameter("numero", agente.Endereco.Numero),
                };


                // Update Endereco
                if (endereco_id!=-1) // Endereco já criado
                {

                    db.Run($"UPDATE Endereco SET Logradouro=@logradouro, Cidade=@cidade, Estado_UF=@estado_uf, Numero=@numero WHERE ID=@ID", endereco_parameters);
                }
                else
                {

                    endereco_id = Convert.ToInt32(db.ExecuteScalar("INSERT INTO Endereco(Logradouro, Cidade, Estado_UF, Numero) VALUES (@logradouro, @cidade, @estado_uf, @numero)", endereco_parameters));
                }

                MySqlParameter[] agente_parameters = new MySqlParameter[]
                {
                    new MySqlParameter("cpf", agente.CPF),
                    new MySqlParameter("nome", agente.Nome),
                    new MySqlParameter("telefone", agente.Telefone),
                    new MySqlParameter("endereco_id", endereco_id)
                };

                // Update Agente
                if (created)
                {

                    db.Run($"UPDATE Agente SET Nome=@nome, Telefone=@telefone, Endereco_ID=@endereco_id WHERE CPF=@cpf", agente_parameters);
                }
                else
                {

                    db.Run("INSERT INTO Agente(CPF, Nome, Telefone, Endereco_ID) VALUES (@cpf, @nome, @telefone, @endereco_id)", agente_parameters);
                }
            }
        }

        public void Delete(string cpf) 
        {
            throw new NotImplementedException();
        }

    }
}
