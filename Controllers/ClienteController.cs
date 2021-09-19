using FelipeB_App3BI.DB;
using FelipeB_App3BI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FelipeB_App3BI.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<ClienteModel> models;
            try
            {
                models = new AgenteDAO().GetClientes();
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
            return View(models);
        }

        public ActionResult Add(string id) {
            return View("Error", new NotImplementedException());
        }

        public ActionResult Del(string id)
        {
            return View("Error", new NotImplementedException());
        }
    }
}
