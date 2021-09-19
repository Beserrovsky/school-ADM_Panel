using FelipeB_App3BI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace FelipeB_App3BI.DB
{
    public class AgenteDAO : DAO<AgenteModel>
    {
        public override string Delete(string ID)
        {
            throw new NotImplementedException();
        }

        public override bool Exists(AgenteModel item)
        {
            throw new NotImplementedException();
        }

        public override bool Exists(string ID)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<AgenteModel> Get()
        {
            throw new NotImplementedException();
        }

        public override AgenteModel Get(string ID)
        {
            throw new NotImplementedException();
        }

        public override AgenteModel GetLatest()
        {
            throw new NotImplementedException();
        }

        public override string Patch(AgenteModel item)
        {
            throw new NotImplementedException();
        }

        public override string Post(AgenteModel item)
        {
            throw new NotImplementedException();
        }

        protected override MySqlParameter[] GetIDParameter(string ID)
        {
            throw new NotImplementedException();
        }

        protected override MySqlParameter[] GetParameters(AgenteModel item)
        {
            throw new NotImplementedException();
        }

        protected override AgenteModel ReadRecord(MySqlDataReader dr)
        {
            throw new NotImplementedException();
        }
    }
}
