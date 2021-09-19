using FelipeB_App3BI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace FelipeB_App3BI.DB
{
    public class ClienteDAO : DAO<ClienteModel>
    {
        public override string Delete(string ID)
        {
            throw new NotImplementedException();
        }

        public override bool Exists(ClienteModel item)
        {
            throw new NotImplementedException();
        }

        public override bool Exists(string ID)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<ClienteModel> Get()
        {
            throw new NotImplementedException();
        }

        public override ClienteModel Get(string ID)
        {
            throw new NotImplementedException();
        }

        public override ClienteModel GetLatest()
        {
            throw new NotImplementedException();
        }

        public override string Patch(ClienteModel item)
        {
            throw new NotImplementedException();
        }

        public override string Post(ClienteModel item)
        {
            throw new NotImplementedException();
        }

        protected override MySqlParameter[] GetIDParameter(string ID)
        {
            throw new NotImplementedException();
        }

        protected override MySqlParameter[] GetParameters(ClienteModel item)
        {
            throw new NotImplementedException();
        }

        protected override ClienteModel ReadRecord(MySqlDataReader dr)
        {
            throw new NotImplementedException();
        }
    }
}
