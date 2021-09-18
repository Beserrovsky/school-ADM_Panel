using FelipeB_App3BI.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FelipeB_App3BI.Controllers
{
    public abstract class ControllerCRUD<D, M>: Controller 
        where D : DAO<M>, new()
        where M : class
    {
        private readonly DAO<M> DAO;

        public ControllerCRUD() { this.DAO = new D(); }

        [HttpGet]
        public ActionResult Index() { return View(this.DAO.Get()); }

        [HttpGet]
        public ActionResult Details(M model) { return View(this.DAO.Get(model)); }

        [HttpGet]
        public ActionResult Form(M model = null) { return View(this.DAO.Get(model)); }

        [HttpPost]
        public ActionResult Post(M model) {
            try
            {
                DAO.Post(model);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
            return View(); 
        }

        [HttpPatch]
        public ActionResult Patch(M model) {
            try
            {
                DAO.Patch(model);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
            return View(); 
        }

        [HttpDelete]
        public ActionResult Delete(M model) {
            try
            {
                DAO.Delete(model);
            }
            catch (Exception e) {
                return View("Error", e);
            }
            return View();
        }
    }
}