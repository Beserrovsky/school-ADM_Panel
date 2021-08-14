using FelipeB_App3BI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class ProdutoDAO
    { 

        public List<ProdutoModel> GetAll()
        {

            List<ProdutoModel> produtos = new List<ProdutoModel>();

            using (DB db = new DB()) 
            {
                MySqlDataReader dr = db.RunAndRead("Select * from Produto", new MySqlParameter[0]);

                if (dr.HasRows) 
                {
                    while (dr.Read()) 
                    {
                        produtos.Add(new ProdutoModel()
                        {
                            ID = dr.GetInt32(0),
                            Nome = dr.GetString(1),
                            Valor = dr.GetDecimal(2),
                            Quantidade = dr.GetInt32(3)
                        });
                    }
                }

                dr.Close();
            }

            return produtos;
        }

        public ProdutoModel Get(int id)
        {

            ProdutoModel produto = null;

            using (DB db = new DB())
            {
                MySqlDataReader dr = db.RunAndRead("Select * from Produto WHERE Produto.ID=@id", new MySqlParameter[] { new MySqlParameter("id", id) });

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        produto = new ProdutoModel()
                        {
                            ID = dr.GetInt32(0),
                            Nome = dr.GetString(1),
                            Valor = dr.GetDecimal(2),
                            Quantidade = dr.GetInt32(3)
                        };
                    }
                }

                dr.Close();
            }

            return produto;
        }

        public void Save(ProdutoModel produto) 
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
