using FelipeB_App3BI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class ClienteDAO
    {
        const string TABLE_NAME = "Cliente";
        const string VIEW_NAME = "clientes_view";

        public List<ClienteModel> GetAll()
        {
            List<ClienteModel> clientes = new List<ClienteModel>();

            using (DB db = new DB()) 
            {
                MySqlDataReader dr = db.RunAndRead($"Select * from {VIEW_NAME}", new MySqlParameter[0]);

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

        public ClienteModel Get(string cpf)
        {
            ClienteModel cliente = null;

            using (DB db = new DB())
            {
                MySqlDataReader dr = db.RunAndRead($"Select * from {VIEW_NAME} WHERE {VIEW_NAME}.CPF=@cpf", new MySqlParameter[] { new MySqlParameter("cpf", cpf) });

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

        public void AddAgente(AgenteModel agente) 
        {
            using (DB db = new DB())
            {
                MySqlDataReader dr = db.RunAndRead($"Select * from {VIEW_NAME} WHERE {VIEW_NAME}.CPF=@cpf", new MySqlParameter[] { new MySqlParameter("cpf", agente.CPF) });

                bool already_created = dr.HasRows;

                dr.Close();

                if (already_created) return;

                db.Run($"INSERT INTO {TABLE_NAME} VALUES (@cpf)", new MySqlParameter[] { new MySqlParameter("cpf", agente.CPF) });
            }
        }

        public void DelAgente(AgenteModel agente)
        {
            using (DB db = new DB())
            {
                MySqlDataReader dr = db.RunAndRead($"Select * from {VIEW_NAME} WHERE {VIEW_NAME}.CPF=@cpf", new MySqlParameter[] { new MySqlParameter("cpf", agente.CPF) });

                bool already_created = dr.HasRows;

                dr.Close();

                if (!already_created) return;

                db.Run($"DELETE FROM {TABLE_NAME} WHERE {TABLE_NAME}.CPF=@cpf", new MySqlParameter[] { new MySqlParameter("cpf", agente.CPF) });
            }
        }
    }
}
