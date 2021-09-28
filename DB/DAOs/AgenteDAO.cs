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
            bool clienteExists = false, funcionarioExists = false;
            using (Database db = new Database()) 
            {
                MySqlDataReader dr = db.RunAndRead("SELECT * FROM agente_view WHERE cpf = @id", GetIDParameter(item.CPF));

                if (dr.HasRows)
                {
                    while (dr.Read()) 
                    {
                        clienteExists = (dr.GetInt32(8) == 1);
                        funcionarioExists = (dr.GetInt32(9) == 1);
                    }
                }
                else throw new Exception("Agente não cadastrado!");

                dr.Close();

                dr = db.RunAndRead("SELECT * FROM Cidade WHERE Nome = @cidade", GetParameters(item));
                bool cityExits = dr.HasRows;
                dr.Close();

                if (!cityExits) db.Run("INSERT INTO Cidade VALUES(@cidade)", GetParameters(item));


                dr = db.RunAndRead("SELECT * FROM Endereco WHERE CEP = @cep", GetParameters(item));
                bool enderecoExists = dr.HasRows;
                dr.Close();

                if (!enderecoExists) db.Run("INSERT INTO Endereco VALUES(@cep, @estado, @cidade, @logradouro)", GetParameters(item));

                db.Run($"UPDATE Endereco SET CEP = @cep, Estado_UF = @estado, Cidade_Nome = @cidade, Logradouro = @logradouro WHERE CEP = @cep", GetParameters(item));
                db.Run("UPDATE Agente SET Nome = @nome, Telefone = @telefone, Numero = @numero, Endereco_CEP = @cep WHERE cpf = @cpf", GetParameters(item));
            }

            if (item.IsCliente && !clienteExists) new ClienteDAO().Post(item.CPF);
            if (!item.IsCliente && clienteExists) new ClienteDAO().Delete(item.CPF);

            if (item.IsFuncionario && !funcionarioExists) new FuncionarioDAO().Post(item.CPF);
            if (!item.IsFuncionario && funcionarioExists) new FuncionarioDAO().Delete(item.CPF);

            return item.CPF;
        }

        public override string Post(AgenteModel item)
        {
            using (Database db = new Database())
            {
                MySqlDataReader dr = db.RunAndRead("SELECT * FROM Cidade WHERE Nome = @cidade", GetParameters(item));
                bool cityExits = dr.HasRows;
                dr.Close();

                if(!cityExits) db.Run("INSERT INTO Cidade VALUES(@cidade)", GetParameters(item));

                dr = db.RunAndRead("SELECT * FROM Endereco WHERE CEP = @cep", GetParameters(item));
                bool enderecoExists = dr.HasRows;
                dr.Close();

                if (!enderecoExists) db.Run("INSERT INTO Endereco VALUES(@cep, @estado, @cidade, @logradouro)", GetParameters(item));

                db.Run($"UPDATE Endereco SET CEP = @cep, Estado_UF = @estado, Cidade_Nome = @cidade, Logradouro = @logradouro WHERE CEP = @cep", GetParameters(item));

                db.Run($"INSERT INTO Agente VALUES(@cpf, @nome, @telefone, @cep, @numero)", GetParameters(item));
            }

            var clienteDAO = new ClienteDAO();
            bool exists = clienteDAO.Exists(item.CPF);
            if (item.IsCliente && !exists) clienteDAO.Post(item.CPF);
            if (!item.IsCliente && exists) clienteDAO.Delete(item.CPF);

            var funcionarioDAO = new FuncionarioDAO();
            exists = funcionarioDAO.Exists(item.CPF);
            if (item.IsFuncionario && !exists) funcionarioDAO.Post(item.CPF);
            if (!item.IsFuncionario && exists) funcionarioDAO.Delete(item.CPF);

            return item.CPF;
        }

        public override string Delete(string ID)
        {
            new ClienteDAO().Delete(ID);
            new FuncionarioDAO().Delete(ID);

            using (Database db = new Database()) 
            {
                db.Run("DELETE FROM Agente WHERE cpf = @id", GetIDParameter(ID));
                return ID;
            }
        }

        protected override MySqlParameter[] GetParameters(AgenteModel item)
        {
            return new MySqlParameter[] {
                new MySqlParameter("cpf", item.CPF),
                new MySqlParameter("nome", item.Nome),
                new MySqlParameter("telefone", item.Telefone),
                new MySqlParameter("cep", item.Endereco.CEP),
                new MySqlParameter("estado", item.Endereco.Estado),
                new MySqlParameter("cidade", item.Endereco.Cidade),
                new MySqlParameter("logradouro", item.Endereco.Logradouro),
                new MySqlParameter("numero", item.Numero)
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
                    CEP = dr.GetString(3),
                    Estado = dr.GetString(4),
                    Cidade = dr.GetString(5),
                    Logradouro = dr.GetString(6),
                },
                Numero = dr.GetInt32(7),
                IsCliente = (dr.GetInt32(8) == 1),
                IsFuncionario = (dr.GetInt32(9) == 1)
            };
            return p;
        }
    }
}
