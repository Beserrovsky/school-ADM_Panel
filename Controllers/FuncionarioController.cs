using FelipeB_App3BI.DB;
using FelipeB_App3BI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FelipeB_App3BI.Controllers
{
    public class FuncionarioController : Controller
    {
        private FuncionarioDAO DAO = new FuncionarioDAO();

        public ActionResult Index()
        {
            IEnumerable<FuncionarioModel> models;
            try
            {
                models = DAO.Get();
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
            return View(models);
        }

        public ActionResult Add(string id)
        {
            try
            {
                if(!DAO.Exists(id)) DAO.Post(id);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
            return RedirectToAction("Details", "Agente", routeValues: new { id });
        }

        public ActionResult Del(string id)
        {
            try
            {
                if(DAO.Exists(id)) DAO.Delete(id);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
            return RedirectToAction("Details", "Agente", routeValues: new { id });
        }
    }
}
