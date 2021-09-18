﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelipeB_App3BI.DB
{
    abstract class DAO <T>
    {
        public abstract T Get();
        public abstract T Get(T item);
        public abstract T Post(T item);
        public abstract T Patch(T item);
        public abstract T Delete(T item);
        public abstract bool Exists(T item);
    }
}
