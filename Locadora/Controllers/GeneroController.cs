using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Locadora.Controllers
{
    public class GeneroController : System.Web.Mvc.Controller
    {
        // GET: Genero
        public ActionResult Index()
        {
            return View(Controller.Genero.Listar());
        }

        // GET: Genero/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genero/Create
        [HttpPost]
        public ActionResult Create(Models.Genero genero)
        {
            try
            {
                Controller.Genero.Salvar(genero);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Genero/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Genero/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.Genero genero)
        {
            try
            {
                Controller.Genero.Salvar(genero);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Genero/Delete/5
        public ActionResult Delete(int id)
        {
            return View(Controller.Genero.BuscarPorId(id));
        }

        // POST: Genero/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Models.Genero genero)
        {
            try
            {
                Controller.Genero.Excluir(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
