﻿using FelipeB_App3BI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Database
{
    public class ClienteDAO
    {

        public List<ClienteModel> GetAll()
        {
            List<ClienteModel> clientes = new List<ClienteModel>();

            using (DB db = new DB()) 
            {
                MySqlDataReader dr = db.RunAndRead($"Select * from clientes_view", new MySqlParameter[0]);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        clientes.Add(new ClienteModel() 
                        { 
                            CPF = dr.GetString(0),
                            Nome = dr.GetString(1),
                            Telefone = dr.GetString(2),
                            Endereco= new Endereco() 
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

            return clientes;
        }

        public int Count()
        {
            int clientes_count = 0;

            using (DB db = new DB())
            {
                MySqlDataReader dr = db.RunAndRead($"Select COUNT(*) from clientes_view", new MySqlParameter[0]);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        clientes_count = dr.GetInt32(0);
                    }
                }

                dr.Close();
            }

            return clientes_count;
        }

        public ClienteModel Get(string cpf)
        {
            ClienteModel cliente = null;

            using (DB db = new DB())
            {
                MySqlDataReader dr = db.RunAndRead($"Select * from clientes_view WHERE clientes_view.CPF=@cpf", new MySqlParameter[] { new MySqlParameter("cpf", cpf) });

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cliente = new ClienteModel()
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

            return cliente;
        }

        public void AddAgente(string cpf) 
        {
            using (DB db = new DB())
            {
                MySqlDataReader dr = db.RunAndRead($"Select * from Agente WHERE Agente.CPF=@cpf", new MySqlParameter[] { new MySqlParameter("cpf", cpf) });

                bool valid_agente = dr.HasRows;

                dr.Close();

                if (!valid_agente) throw new Exception("CPF não cadastrado como Agente!");

                dr = db.RunAndRead($"Select * from clientes_view WHERE clientes_view.CPF=@cpf", new MySqlParameter[] { new MySqlParameter("cpf", cpf) });

                bool already_cliente = dr.HasRows;

                dr.Close();

                if (already_cliente) throw new Exception("CPF já cadastrado como cliente!");

                db.Run($"INSERT INTO Cliente VALUES (@cpf)", new MySqlParameter[] { new MySqlParameter("cpf", cpf) });
            }
        }

        public void DelAgente(string cpf)
        {
            using (DB db = new DB())
            {

                MySqlDataReader dr = db.RunAndRead($"Select * from Agente WHERE Agente.CPF=@cpf", new MySqlParameter[] { new MySqlParameter("cpf", cpf) });

                bool valid_agente = dr.HasRows;

                dr.Close();

                if (!valid_agente) throw new Exception("CPF não cadastrado como Agente!");

                dr = db.RunAndRead($"Select * from clientes_view WHERE clientes_view.CPF=@cpf", new MySqlParameter[] { new MySqlParameter("cpf", cpf) });

                bool already_cliente = dr.HasRows;

                dr.Close();

                if (!already_cliente) throw new Exception("CPF não cadastrado como cliente!");

                db.Run($"DELETE FROM Cliente WHERE Cliente.Agente_CPF=@cpf", new MySqlParameter[] { new MySqlParameter("cpf", cpf) });
            }
        }
    }
}
