using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Locadora.Controllers
{
    public class ClienteController : System.Web.Mvc.Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            return View(Controller.Cliente.Listar());
        }

        // GET: Cliente/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        public ActionResult Create(Models.Cliente cliente)
        {
            try
            {
                Controller.Cliente.Salvar(cliente);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(int id)
        {
            return View(Controller.Cliente.BuscarPorId(id));
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Models.Cliente cliente)
        {
            try
            {
                Controller.Cliente.Salvar(cliente);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cliente/Delete/5
        public ActionResult Delete(int id)
        {
            return View(Controller.Cliente.BuscarPorId(id));
        }

        // POST: Cliente/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Controller.Cliente.Excluir(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
