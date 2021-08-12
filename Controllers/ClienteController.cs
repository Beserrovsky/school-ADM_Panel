using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FelipeB_App3BI.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            return View("Clientes");
        }

        // POST: Cliente/Add
        [HttpPost]
        public ActionResult Add(string cpf)
        {
            throw new NotImplementedException();
        }

        // POST: Cliente/Del
        [HttpPost]
        public ActionResult Del(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}
