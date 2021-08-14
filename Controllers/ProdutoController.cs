using Database;
using FelipeB_App3BI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FelipeB_App3BI.Controllers
{
    public class ProdutoController : Controller
    {
        public ActionResult Index()
        {

            List<ProdutoModel> produtos = new ProdutoDAO().GetAll();

            return View("Produtos", produtos);
        }

        public ActionResult Details(int id)
        {

            ProdutoModel produto = new ProdutoDAO().Get(id);

            return View("Produto", produto);
        }

        public ActionResult Create() 
        {

            ProdutoModel produto = new ProdutoModel();

            return View("ProdutoForm", produto);
        }

        [HttpPost]
        public ActionResult Save(ProdutoModel produto)
        {

            ProdutoDAO produtoDAO = new ProdutoDAO();

            try
            {
                if (!ModelState.IsValid) return View("ProdutoForm", produto);
                produtoDAO.Save(produto);
                return View("Index");
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {

            ProdutoDAO produtoDAO = new ProdutoDAO();

            try
            {
                produtoDAO.Delete(id);
                return View("Index");
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
        }
    }
}