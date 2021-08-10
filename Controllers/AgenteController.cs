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
            return View();
        }

        [HttpGet]
        public ActionResult Funcionarios()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Clientes()
        {

            return View();
        }


        [HttpPost]
        public JsonResult CheckCpf(int[] cpf) 
        {

            return Json(isCpfValid(cpf));
        }

        public static bool isCpfValid(int[] cpf) 
        {

            if (cpf.Length != 11 || cpf.All(v => v == cpf[0])) return false; // Verifica se Array está mal formatado ou tem todos os digitos iguais

            int sum = 0;

            for (int i = 10; i >= 2; i--) { // Multiplica os 9 primeiros números pela seqência decrescente de 10 a 2
                sum += cpf[10 - i] * i;
            }

            double mod = (sum * 10 ) % 11;

            if (mod != cpf[9]) return false; // Verfica se o resto da soma anterior quando multipliacada por 10 e dividida por 11 é igual ao primeiro digito da confirmação

            sum = 0;

            for (int i = 11; i >= 2; i--){ // Multiplica os 10 primeiros números pela seqência decrescente de 11 a 2
                sum += cpf[11 - i] * i;
            }

            mod = (sum * 10) % 11;

            if (mod != cpf[10]) return false; // Repete a etapa de verificação, mas para o segundo digito

            return true;
        }

    }
}