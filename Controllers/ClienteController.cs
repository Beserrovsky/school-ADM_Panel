using Database;
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
        /*
        // GET: Cliente
        public ActionResult Index()
        {

            ClienteDAO clienteDAO = new ClienteDAO();
            List<Cliente> clientes = clienteDAO.GetAll();

            ViewBag.ClientesCount = clienteDAO.Count();

            return View("Clientes", clientes);
        }

        // POST: Cliente/Add
        [HttpPost]
        public ActionResult Add(string cpf)
        {

            ClienteDAO clienteDAO = new ClienteDAO();
            try { clienteDAO.AddAgente(cpf); }
            catch (Exception e) { return View("Error", e); }

            return RedirectToAction("Index", "Agente");
        }

        // POST: Cliente/Del
        [HttpPost]
        public ActionResult Del(string cpf)
        {

            ClienteDAO clienteDAO = new ClienteDAO();
            try { clienteDAO.DelAgente(cpf); }
            catch (Exception e) { return View("Error", e); }

            return RedirectToAction("Index", "Agente");
        }
        */
    }
}
