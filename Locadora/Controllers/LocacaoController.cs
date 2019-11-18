using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Locadora.Controllers
{
    public class LocacaoController : System.Web.Mvc.Controller
    {
        // GET: Locacao
        [ChildActionOnly]
        public ActionResult Index()
        {
            return PartialView(Controller.Locacao.Listar());
        }

        public ActionResult Home()
        {
            return View();
        }

        // GET: Locacao/Create
        public ActionResult Create()
        {
            ViewBag.Funcionario = Controller.ItensSelecao.Funcionario.GetSelectListItems;
            ViewBag.Cliente = Controller.ItensSelecao.Cliente.GetSelectListItems;
            ViewBag.Filme = Controller.ItensSelecao.Filme.GetSelectListItems;
            return View();
        }

        [ChildActionOnly]
        public ActionResult Criar()
        {
            ViewBag.Funcionario = Controller.ItensSelecao.Funcionario.GetSelectListItems;
            ViewBag.Cliente = Controller.ItensSelecao.Cliente.GetSelectListItems;
            ViewBag.Filme = Controller.ItensSelecao.Filme.GetSelectListItems;
            return PartialView("CriarPartial");
        }

        // POST: Locacao/Create
        [HttpPost]
        public ActionResult Create(Models.Locacao locacao)
        {
            try
            {
                Controller.Locacao.Salvar(locacao);
                return RedirectToAction("Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: Locacao/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Locacao/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: Locacao/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Locacao/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Home");
            }
            catch
            {
                return View();
            }
        }
    }
}
