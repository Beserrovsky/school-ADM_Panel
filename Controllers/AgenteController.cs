using FelipeB_App3BI.Models;
using FelipeB_App3BI.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FelipeB_App3BI.Controllers
{
    public class AgenteController : ControllerCRUD<AgenteDAO, AgenteModel>
    {

        [HttpGet]
        public override ActionResult Index()
        {
            IEnumerable<AgenteModel> models;
            try
            {
                models = this.DAO.Get();
                ViewBag.ClientesCount = new ClienteDAO().Get().Count();
                ViewBag.FuncionariosCount = new FuncionarioDAO().Get().Count();
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
            return View(models);
        }

        [HttpGet]
        public override ActionResult Form(string ID = null)
        {
            AgenteModel model = null;
            try
            {
                if (ID != null) model = this.DAO.Get(ID);
                ViewBag.Estados = new AgenteDAO().GetEstados();
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
            return View(model);
        }


        [HttpPost]
        public JsonResult CheckCpf(string cpf) 
        {
            if (!AgenteModel.IsCpfValid(cpf)) return Json("Insira um CPF válido!", JsonRequestBehavior.AllowGet);
            if (base.DAO.Exists(cpf)) return Json("CPF já cadastrado!", JsonRequestBehavior.AllowGet);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CheckState(Endereco endereco)
        {
            return Json(Endereco.IsEstadoValid(endereco.Estado));
        }

    }
}