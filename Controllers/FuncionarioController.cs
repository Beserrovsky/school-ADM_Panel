using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FelipeB_App3BI.Controllers
{
    public class FuncionarioController : Controller
    {
        // GET: Funcionario
        public ActionResult Index()
        {
            return View("Funcionarios");
        }

        // POST: Funcionario/Add
        [HttpPost]
        public ActionResult Add(string cpf)
        {
            throw new NotImplementedException();
        }

        // POST: Funcionario/Del
        [HttpPost]
        public ActionResult Del(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}
