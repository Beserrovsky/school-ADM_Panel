﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FelipeB_App3BI.Models
{
    public class ProdutoModel
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }
    }
}