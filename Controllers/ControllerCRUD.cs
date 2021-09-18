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
        private readonly DAO<M> DAO;

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
        public ActionResult Details(M model) {
            try
            {
                if (model != null) model = this.DAO.Get(model);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Form(M model = null) {
            try
            {
                if(model != null) model = this.DAO.Get(model);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
            return View(model); 
        }

        [HttpPost]
        public ActionResult Post(M model) {
            try
            {
                if (!ModelState.IsValid || !model.Validate()) return View("Form", model);
                DAO.Post(model);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
            return RedirectToAction("Details", model);
        }

        [HttpPatch]
        public ActionResult Patch(M model) {
            try
            {
                if (!ModelState.IsValid || !model.Validate()) return View("Form", model);
                DAO.Patch(model);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
            return RedirectToAction("Details", model);
        }

        [HttpDelete]
        public ActionResult Delete(M model) {
            try
            {
                if (!DAO.Exists(model)) throw new Exception("Item não existe!");
                DAO.Delete(model);
            }
            catch (Exception e) {
                return View("Error", e);
            }
            return RedirectToAction("Index");
        }
    }
}