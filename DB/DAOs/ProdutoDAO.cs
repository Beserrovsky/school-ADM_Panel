using FelipeB_App3BI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace FelipeB_App3BI.DB
{
    public class ProdutoDAO : DAO<ProdutoModel>
    {

        public override IEnumerable<ProdutoModel> Get()
        {
            using (Database db = new Database()) {
                List<ProdutoModel> produtos = new List<ProdutoModel>();

                MySqlDataReader dr = db.RunAndRead("SELECT * FROM produto", new MySqlParameter[0]);

                if (dr.HasRows) {
                    while (dr.Read()) {
                        produtos.Add(ReadRecord(dr));
                    }
                }

                dr.Close();

                return produtos;
            }
        }

        public override ProdutoModel Get(string ID)
        {
            using (Database db = new Database())
            {
                ProdutoModel produto = null;

                MySqlDataReader dr = db.RunAndRead("SELECT * FROM produto WHERE id = @id", GetIDParameter(ID));

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        produto = ReadRecord(dr);
                    }
                }

                dr.Close();

                return produto;
            }
        }

        public ProdutoModel GetLatest()
        {
            using (Database db = new Database())
            {
                ProdutoModel produto = null;

                MySqlDataReader dr = db.RunAndRead("SELECT * FROM produto ORDER BY id DESC LIMIT 1", new MySqlParameter[0]);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        produto = ReadRecord(dr);
                    }
                }

                dr.Close();

                return produto;
            }
        }

        public override string Patch(ProdutoModel item)
        {
            using (Database db = new Database())
            {

                db.Run("UPDATE produto SET nome = @nome, valor = @valor, quantidade = @quantidade", GetParameters(item));
                return item.ID.ToString();
            }
        }

        public override string Post(ProdutoModel item)
        {
            using (Database db = new Database())
            {

                db.Run("INSERT INTO produto(nome, valor, quantidade) VALUES(@nome, @valor, @quantidade)", GetParameters(item));
                return GetLatest().ID.ToString();
            }
        }

        public override string Delete(string ID)
        {
            using (Database db = new Database())
            {

                db.Run("DELETE FROM produto WHERE id = @id", GetIDParameter(ID));
                return ID;
            }
        }

        public override bool Exists(ProdutoModel item)
        {
            using (Database db = new Database())
            {

                MySqlDataReader dr = db.RunAndRead("SELECT * FROM produto WHERE id = @id", GetParameters(item));

                bool exists = dr.HasRows;

                dr.Close();

                return exists;
            }
        }

        public override bool Exists(string ID)
        {
            using (Database db = new Database())
            {

                MySqlDataReader dr = db.RunAndRead("SELECT * FROM produto WHERE id = @id", GetIDParameter(ID));

                bool exists = dr.HasRows;

                dr.Close();

                return exists;
            }
        }

        protected override MySqlParameter[] GetParameters(ProdutoModel item)
        {
            return new MySqlParameter[] {
                new MySqlParameter("id", item.ID),
                new MySqlParameter("nome", item.Nome),
                new MySqlParameter("valor", item.Valor),
                new MySqlParameter("quantidade", item.Quantidade)
            };
        }

        protected override ProdutoModel ReadRecord(MySqlDataReader dr)
        {
            ProdutoModel p = new ProdutoModel
            {
                ID = dr.GetInt32(0),
                Nome = dr.GetString(1),
                Valor = dr.GetDecimal(2),
                Quantidade = dr.GetInt32(3),
            };
            return p;
        }
    }
}
