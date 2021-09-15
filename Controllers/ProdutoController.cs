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
        /*
        public ActionResult Index()
        {

            try
            {
                List<ProdutoModel> produtos = new ProdutoDAO().GetAll();
                return View("Produtos", produtos);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
        }

        public ActionResult Details(int id)
        {

            try
            {
                ProdutoModel produto = new ProdutoDAO().Get(id);
                return View("Produto", produto);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
        }

        public ActionResult Edit(int id)
        {

            try
            {
                ProdutoModel produto = new ProdutoDAO().Get(id);
                ViewBag.Editing = true;
                return View("ProdutoForm", produto);
            }
            catch (Exception e) 
            {
                return View("Error", e);
            }
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
                return RedirectToAction("Index");
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
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
        }
        */
    }
}