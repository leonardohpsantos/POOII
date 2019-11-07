using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Locadora.Controllers
{
    public class ProdutoraController : System.Web.Mvc.Controller
    {
        // GET: Produtora
        public ActionResult Index()
        {
            return View(Controller.Produtora.Listar());
        }

        // GET: Produtora/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produtora/Create
        [HttpPost]
        public ActionResult Create(Models.Produtora produtora)
        {
            try
            {
                Controller.Produtora.Salvar(produtora);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Produtora/Edit/5
        public ActionResult Edit(int id)
        {
            return View(Controller.Produtora.BuscarPorId(id));
        }

        // POST: Produtora/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Models.Produtora produtora)
        {
            try
            {
                Controller.Produtora.Salvar(produtora);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Produtora/Delete/5
        public ActionResult Delete(int id)
        {
            return View(Controller.Produtora.BuscarPorId(id));
        }

        // POST: Produtora/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Controller.Produtora.Excluir(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
