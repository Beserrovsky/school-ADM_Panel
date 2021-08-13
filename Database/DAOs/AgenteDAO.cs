using FelipeB_App3BI.Models;
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
