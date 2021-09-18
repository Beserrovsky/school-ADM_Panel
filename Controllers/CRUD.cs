using FelipeB_App3BI.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelipeB_App3BI.Controllers
{
    abstract class CRUD<T>
    {
        private DAO<T> DAO;

        public CRUD() { this.DAOInit(); }

        protected abstract void DAOInit();
    }
}
