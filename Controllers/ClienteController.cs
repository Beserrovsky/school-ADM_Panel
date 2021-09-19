﻿using FelipeB_App3BI.DB;
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
        private ClienteDAO DAO = new ClienteDAO();

        public ActionResult Index()
        {
            IEnumerable<ClienteModel> models;
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

        public ActionResult Add(string id) {
            try
            {
                DAO.Post(id);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
            return RedirectToAction("Details", "Agente", routeValues: id);
        }

        public ActionResult Del(string id)
        {
            try
            {
                DAO.Delete(id);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
            return RedirectToAction("Details", "Agente", routeValues: id);
        }
    }
}
