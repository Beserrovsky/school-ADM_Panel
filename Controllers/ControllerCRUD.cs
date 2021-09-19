using FelipeB_App3BI.DB;
using FelipeB_App3BI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FelipeB_App3BI.Controllers
{
    public abstract class ControllerCRUD<D, M>: Controller 
        where D : DAO<M>, new()
        where M : Model
    {
        protected readonly DAO<M> DAO;

        public ControllerCRUD() { this.DAO = new D(); }

        [HttpGet]
        public ActionResult Index() {
            IEnumerable<M> models;
            try
            {
                models = this.DAO.Get();
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
            return View(models);
        }

        [HttpGet]
        public ActionResult Details(string ID) {
            M model = null;
            try
            {
                if (ID != null) model = this.DAO.Get(ID);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Form(string ID = null) {
            M model = null;
            try
            {
                if(ID != null) model = this.DAO.Get(ID);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
            return View(model); 
        }

        [HttpPost]
        public ActionResult Save(M model) {
            string ID = null;
            try
            {
                if (!ModelState.IsValid || !model.Validate()) return View("Form", model);
                if (DAO.Exists(model)) ID = DAO.Patch(model);
                else ID = DAO.Post(model);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
            return RedirectToAction("Details", routeValues: new { ID });
        }

        [HttpPost]
        public ActionResult Delete(string ID) {
            try
            {
                if (!DAO.Exists(ID)) throw new Exception("Item não existe!");
                DAO.Delete(ID);
            }
            catch (Exception e) {
                return View("Error", e);
            }
            return RedirectToAction("Index");
        }
    }
}