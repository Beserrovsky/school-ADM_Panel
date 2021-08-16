using Database;
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
        // GET: Funcionario
        public ActionResult Index()
        {

            FuncionarioDAO funcionarioDAO = new FuncionarioDAO();
            List<FuncionarioModel> funcionarios = funcionarioDAO.GetAll();

            ViewBag.FuncionariosCount = funcionarioDAO.Count();

            return View("Funcionarios", funcionarios);
        }

        // POST: Funcionario/Add
        [HttpPost]
        public ActionResult Add(string cpf)
        {

            FuncionarioDAO funcionarioDAO = new FuncionarioDAO();
            try { funcionarioDAO.AddAgente(cpf); }
            catch (Exception e) { return View("Error", e); }

            return RedirectToAction("Index", "Agente");
        }

        // POST: Funcionario/Del
        [HttpPost]
        public ActionResult Del(string cpf)
        {

            FuncionarioDAO funcionarioDAO = new FuncionarioDAO();
            try { funcionarioDAO.DelAgente(cpf); }
            catch (Exception e) { return View("Error", e); }

            return RedirectToAction("Index", "Agente");
        }
    }
}
