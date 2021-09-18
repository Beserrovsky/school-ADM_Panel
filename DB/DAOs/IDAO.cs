using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelipeB_App3BI.DB
{
    interface IDAO <T>
    {
        T Get();
        T Get(T item);
        T Post(T item);
        T Patch(T item);
        T Delete(T item);
        bool Exists(T item);
    }
}
