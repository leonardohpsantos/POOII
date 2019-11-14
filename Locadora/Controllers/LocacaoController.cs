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
        public ActionResult Index()
        {
            return View(Controller.Locacao.Listar());
        }

        // GET: Locacao/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Locacao/Create
        [HttpPost]
        public ActionResult Create(Models.Locacao locacao)
        {
            try
            {
                Controller.Locacao.Salvar(locacao);
                return RedirectToAction("Index");
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

                return RedirectToAction("Index");
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

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
