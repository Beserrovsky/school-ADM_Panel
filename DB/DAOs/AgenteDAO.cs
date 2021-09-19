using FelipeB_App3BI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace FelipeB_App3BI.DB
{
    public class AgenteDAO : DAO<AgenteModel>
    {
        public override IEnumerable<AgenteModel> Get()
        {
            using (Database db = new Database())
            {
                List<AgenteModel> agentes = new List<AgenteModel>();

                MySqlDataReader dr = db.RunAndRead("SELECT * FROM agente_view", new MySqlParameter[0]);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        agentes.Add(ReadRecord(dr));
                    }
                }

                dr.Close();

                return agentes;
            }
        }

        public override AgenteModel Get(string ID)
        {
            using (Database db = new Database())
            {
                AgenteModel agente = null;

                MySqlDataReader dr = db.RunAndRead("SELECT * FROM agente_view WHERE cpf = @id", GetIDParameter(ID));

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        agente = ReadRecord(dr);
                    }
                }

                dr.Close();

                return agente;
            }
        }

        public IEnumerable<string> GetEstados()
        {
            using (Database db = new Database())
            {
                List<string> estados = new List<string>();

                MySqlDataReader dr = db.RunAndRead("Select * from estado", new MySqlParameter[0]);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        estados.Add(dr.GetString(0));
                    }
                }

                dr.Close();

                return estados;
            }
        }

        public IEnumerable<ClienteModel> GetClientes()
        {
            using (Database db = new Database())
            {
                List<ClienteModel> clientes = new List<ClienteModel>();

                MySqlDataReader dr = db.RunAndRead("Select * from clientes_view", new MySqlParameter[0]);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        clientes.Add(ReadCliente(dr));
                    }
                }

                dr.Close();

                return clientes;
            }
        }

        public IEnumerable<FuncionarioModel> GetFuncionarios()
        {
            using (Database db = new Database())
            {
                List<FuncionarioModel> funcionarios = new List<FuncionarioModel>();

                MySqlDataReader dr = db.RunAndRead("Select * from funcionarios_view", new MySqlParameter[0]);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        funcionarios.Add(ReadFuncionario(dr));
                    }
                }

                dr.Close();

                return funcionarios;
            }
        }

        public override bool Exists(AgenteModel item)
        {
            using (Database db = new Database())
            {
                MySqlDataReader dr = db.RunAndRead("SELECT * FROM agente_view WHERE cpf = @id", GetIDParameter(item.CPF));

                bool exists = dr.HasRows;

                dr.Close();

                return exists;
            }
        }

        public override bool Exists(string ID)
        {
            using (Database db = new Database())
            {
                MySqlDataReader dr = db.RunAndRead("SELECT * FROM agente_view WHERE cpf = @id", GetIDParameter(ID));

                bool exists = dr.HasRows;

                dr.Close();

                return exists;
            }
        }

        public bool EstadoExists(string estado)
        {
            using (Database db = new Database())
            {
                MySqlDataReader dr = db.RunAndRead("SELECT * FROM estado WHERE uf = @id", GetIDParameter(estado));

                bool exists = dr.HasRows;

                dr.Close();

                return exists;
            }
        }

        public override string Patch(AgenteModel item)
        {
            throw new NotImplementedException();
        }

        public override string Post(AgenteModel item)
        {
            throw new NotImplementedException();
        }

        public override string Delete(string ID)
        {
            throw new NotImplementedException();
        }

        protected override MySqlParameter[] GetParameters(AgenteModel item)
        {
            return new MySqlParameter[] {
                new MySqlParameter("cpf", item.CPF),
                new MySqlParameter("nome", item.Nome),
                new MySqlParameter("telefone", item.Telefone),
                new MySqlParameter("estado", item.Endereco.Estado),
                new MySqlParameter("cidade", item.Endereco.Cidade),
                new MySqlParameter("logradouro", item.Endereco.Logradouro),
                new MySqlParameter("numero", item.Endereco.Numero)
            };
        }

        protected override AgenteModel ReadRecord(MySqlDataReader dr)
        {
            AgenteModel p = new AgenteModel
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
            return p;
        }

        protected FuncionarioModel ReadFuncionario(MySqlDataReader dr)
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

        protected ClienteModel ReadCliente(MySqlDataReader dr)
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
