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
        public abstract T Get(T item);
        public abstract T Post(T item);
        public abstract T Patch(T item);
        public abstract T Delete(T item);
        public abstract bool Exists(T item);

        protected abstract MySqlParameter[] GetParameters(T item);
        protected abstract T ReadRecord(MySqlDataReader dr);
    }
}
