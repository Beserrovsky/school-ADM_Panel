using FelipeB_App3BI.Models;
using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FelipeB_App3BI.Util;

namespace FelipeB_App3BI.Controllers
{
    public class AgenteController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.ClientesCount = Context.Count<Cliente>();

            ViewBag.FuncionariosCount = Context.Count<Funcionario>();

            IEnumerable<Agente> agentes = Context.Get<Agente>();

            foreach (var agente in agentes) 
            {
                agente.CPF = Masker.MaskCpf(agente.CPF);
                agente.Telefone = Masker.MaskTelephone(agente.Telefone);
            }

            return View(agentes);
        }

        /*
        // GET: Agente/cpf=?
        public ActionResult Details(string cpf)
        {
            AgenteDAO agenteDAO = new AgenteDAO();

            try
            {
                Agente agente = agenteDAO.Get(cpf);
                return View("Agente", agente);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
        }

        // GET: Agente/Edit
        public ActionResult Edit(string cpf)
        {
            AgenteDAO agenteDAO = new AgenteDAO();

            try
            {
                Agente agente = agenteDAO.Get(cpf);
                ViewBag.Editing = true;
                ViewBag.Estados = new AgenteDAO().GetAllStates();
                return View("AgenteForm", agente);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
        }

        // GET: Agente/Create
        public ActionResult Create()
        {

            Agente agente = new Agente();
            ViewBag.Estados = new AgenteDAO().GetAllStates();
            return View("AgenteForm", agente);
        }

        // POST: Agente/Save
        [HttpPost]
        public ActionResult Save(Agente agente)
        {
            try
            {
                if (!ModelState.IsValid || !IsCpfValid(agente.CPF) || !IsStateValid(agente.Endereco.Estado) || !new AgenteDAO().Exists(agente.CPF)) return View("AgenteForm", agente);

                new AgenteDAO().Save(agente);
                return RedirectToAction("Index");
            }
            catch (Exception e) 
            {
                return View("Error", e);
            }
        }

        // POST: Agente/Delete
        [HttpPost]
        public ActionResult Delete(string cpf)
        {
            try
            {
                if (!IsCpfValid(cpf)) return View("Error", new Exception("CPF não válido!"));

                new AgenteDAO().Delete(cpf);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
        }

        // POST: Agente/CheckCpf/cpf=string
        [HttpPost]
        public JsonResult CheckCpf(string cpf) 
        {

            if (!IsCpfValid(cpf)) return Json("Insira um CPF válido!", JsonRequestBehavior.AllowGet);

            if (new AgenteDAO().Exists(cpf)) return Json("CPF já cadastrado", JsonRequestBehavior.AllowGet);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        // POST: Agente/CheckState/estado=string
        [HttpPost]
        public JsonResult CheckState(Endereco endereco)
        {

            return Json(IsStateValid(endereco.Estado));
        }



        public bool IsStateValid(string state_uf) 
        {

            if (state_uf == null || state_uf.Length != 2) return false;

            return new AgenteDAO().GetAllStates().Any(s => s.ToLower().Equals(state_uf.ToLower()));
        }

        */

    }
}