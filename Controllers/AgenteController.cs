using FelipeB_App3BI.Models;
using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FelipeB_App3BI.Controllers
{
    public class AgenteController : Controller
    {

        public ActionResult Index()
        {

            AgenteDAO agenteDAO = new AgenteDAO();

            ViewBag.AgentesCount = agenteDAO.Count();

            ViewBag.ClientesCount = new ClienteDAO().Count();

            ViewBag.FuncionariosCount = new FuncionarioDAO().Count();

            List<AgenteModel> agentes = agenteDAO.GetAll();

            return View("Dashboard", agentes);
        }

        // GET: Agente/cpf=?
        public ActionResult Details(string cpf)
        {
            AgenteDAO agenteDAO = new AgenteDAO();

            try
            {
                AgenteModel agente = agenteDAO.Get(cpf);
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
                AgenteModel agente = agenteDAO.Get(cpf);
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

            AgenteModel agente = new AgenteModel();

            return View("AgenteForm", agente);
        }

        // POST: Agente/Save
        [HttpPost]
        public ActionResult Save(AgenteModel agente)
        {
            try
            {
                if (!ModelState.IsValid || !IsCpfValid(agente.CPF)) return View("AgenteForm", agente);
                new AgenteDAO().Save(agente);
                return RedirectToAction("Index");
            }
            catch (Exception e) 
            {
                return View("Erro", e);
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
                return View("Erro", e);
            }
        }

        // POST: Agente/CheckCpf/cpf=string
        [HttpPost]
        public JsonResult CheckCpf(string cpf) 
        {

            return Json(IsCpfValid(cpf));
        }

        public static bool IsCpfValid(string cpf) 
        {

            if (cpf.Length != 11) return false; // Verifica se Array está mal formatado

            int[] cpf_arr = new int[11];
            for (int i = 0; i < cpf.Length; i++)
                if (!int.TryParse(cpf[i].ToString(), out cpf_arr[i])) return false; // Converte de String para int[] e retorna falso caso não seja um número

            int sum = 0;

            for (int i = 10; i >= 2; i--) { // Multiplica os 9 primeiros números pela seqência decrescente de 10 a 2
                sum += cpf_arr[10 - i] * i;
            }

            double mod = (sum * 10 ) % 11;

            if (mod != cpf_arr[9]) return false; // Verfica se o resto da soma anterior quando multipliacada por 10 e dividida por 11 é igual ao primeiro digito da confirmação

            sum = 0;

            for (int i = 11; i >= 2; i--){ // Multiplica os 10 primeiros números pela seqência decrescente de 11 a 2
                sum += cpf_arr[11 - i] * i;
            }

            mod = (sum * 10) % 11;

            if (mod != cpf_arr[10]) return false; // Repete a etapa de verificação, mas para o segundo digito

            return true;
        }
    }
}