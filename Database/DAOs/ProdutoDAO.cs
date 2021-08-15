using FelipeB_App3BI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

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
            using (DB db = new DB())
            {

                bool update = false;

                if (produto.ID!=0) 
                {
                    MySqlDataReader dr = db.RunAndRead($"Select * from Produto WHERE Produto.ID=@id", new MySqlParameter[] { new MySqlParameter("id", produto.ID) });

                    update = dr.HasRows;

                    dr.Close();
                }

                if (update)
                {
                    db.Run($"UPDATE Produto SET Nome=@nome, Valor=@valor, Quantidade=@quantidade WHERE Produto.ID=@id", new MySqlParameter[] { new MySqlParameter("id", produto.ID), new MySqlParameter("nome", produto.Nome), new MySqlParameter("valor", produto.Valor), new MySqlParameter("quantidade", produto.Quantidade) });
                }
                else
                {
                    db.Run($"INSERT INTO Produto(Nome, Valor, Quantidade) VALUES (@nome, @valor, @quantidade)", new MySqlParameter[] { new MySqlParameter("nome", produto.Nome), new MySqlParameter("valor", produto.Valor), new MySqlParameter("quantidade", produto.Quantidade) });
                }
            }
        }

        public void Delete(int id)
        {
            using (DB db = new DB())
            {

                MySqlDataReader dr = db.RunAndRead($"Select * from Produto WHERE Produto.ID=@id", new MySqlParameter[] { new MySqlParameter("id", id) });

                bool already_produto = dr.HasRows;

                dr.Close();

                if (!already_produto) throw new Exception("ID não cadastrado como produto!");

                db.Run($"DELETE FROM Produto WHERE Produto.ID=@id", new MySqlParameter[] { new MySqlParameter("id", id) });
            }
        }
    }
}
