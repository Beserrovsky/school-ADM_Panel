using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelipeB_App3BI.DB
{
    public abstract class DAO <T>
    {
        public abstract IEnumerable<T> Get();
        public abstract T Get(string ID);
        public abstract string Post(T item);
        public abstract string Patch(T item);
        public abstract string Delete(string ID);
        public abstract bool Exists(T item);
        public abstract bool Exists(string ID);

        protected abstract MySqlParameter[] GetParameters(T item);
        protected MySqlParameter[] GetIDParameter(string ID) { return new MySqlParameter[] { new MySqlParameter("id", ID) };}
        protected abstract T ReadRecord(MySqlDataReader dr);
    }
}
