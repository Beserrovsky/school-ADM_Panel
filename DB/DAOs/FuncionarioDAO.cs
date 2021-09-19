using FelipeB_App3BI.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace FelipeB_App3BI.DB
{
    public class FuncionarioDAO : DAO<FuncionarioModel>
    {
        public override string Delete(string ID)
        {
            throw new System.NotImplementedException();
        }

        public override bool Exists(FuncionarioModel item)
        {
            throw new System.NotImplementedException();
        }

        public override bool Exists(string ID)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<FuncionarioModel> Get()
        {
            throw new System.NotImplementedException();
        }

        public override FuncionarioModel Get(string ID)
        {
            throw new System.NotImplementedException();
        }

        public override FuncionarioModel GetLatest()
        {
            throw new System.NotImplementedException();
        }

        public override string Patch(FuncionarioModel item)
        {
            throw new System.NotImplementedException();
        }

        public override string Post(FuncionarioModel item)
        {
            throw new System.NotImplementedException();
        }

        protected override MySqlParameter[] GetIDParameter(string ID)
        {
            throw new System.NotImplementedException();
        }

        protected override MySqlParameter[] GetParameters(FuncionarioModel item)
        {
            throw new System.NotImplementedException();
        }

        protected override FuncionarioModel ReadRecord(MySqlDataReader dr)
        {
            throw new System.NotImplementedException();
        }
    }
}
