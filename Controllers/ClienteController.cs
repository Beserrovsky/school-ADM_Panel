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
        // GET: Cliente
        public ActionResult Index()
        {

            ClienteDAO clienteDAO = new ClienteDAO();
            List<ClienteModel> clientes = clienteDAO.GetAll();

            return View("Clientes", clientes);
        }

        // GET: Cliente/cpf=?
        public ActionResult Details(string cpf)
        {
            ClienteDAO clienteDAO = new ClienteDAO();

            try 
            {
                ClienteModel cliente = clienteDAO.Get(cpf);
                return View("Cliente", cliente);
            }
            catch (Exception e) 
            { 
                return View("Error", e); 
            }
        }

        // POST: Cliente/Add
        [HttpPost]
        public ActionResult Add(string cpf)
        {

            ClienteDAO clienteDAO = new ClienteDAO();
            try { clienteDAO.AddAgente(cpf); }
            catch (Exception e) { return View("Error", e); }

            return RedirectToAction("Details", new { cpf });
        }

        // POST: Cliente/Del
        [HttpPost]
        public ActionResult Del(string cpf)
        {

            ClienteDAO clienteDAO = new ClienteDAO();
            try { clienteDAO.DelAgente(cpf); }
            catch (Exception e) { return View("Error", e); }

            return RedirectToAction("Index");
        }
    }
}
