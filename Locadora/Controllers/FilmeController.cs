using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Locadora.Controllers
{
    public class FilmeController : System.Web.Mvc.Controller
    {
        // GET: Filme
        public ActionResult Index()
        {
            return View(Controller.Filme.Listar());
        }

        // GET: Filme/Create
        public ActionResult Create()
        {
            ViewBag.Classificacao = Controller.ItensSelecao.Classificacao.GetSelectListItems;
            ViewBag.Genero = Controller.ItensSelecao.Genero.GetSelectListItems;
            ViewBag.Produtora = Controller.ItensSelecao.Produtora.GetSelectListItems;
            return View();
        }

        // POST: Filme/Create
        [HttpPost]
        public ActionResult Create(Models.Filme filme)
        {
            try
            {
                Controller.Filme.Salvar(filme);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Filme/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Classificacao = Controller.ItensSelecao.Classificacao.GetSelectListItems;
            ViewBag.Genero = Controller.ItensSelecao.Genero.GetSelectListItems;
            ViewBag.Produtora = Controller.ItensSelecao.Produtora.GetSelectListItems;
            return View(Controller.Filme.BuscarFilmeCompletoPorId(id));
        }

        // POST: Filme/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Models.Filme filme)
        {
            try
            {
                Controller.Filme.Salvar(filme);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Filme/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Filme/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Controller.Filme.Excluir(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
