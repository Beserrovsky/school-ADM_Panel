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
            return Json(AgenteModel.IsEstadoValid(endereco.Estado));
        }

    }
}