using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Locadora.Controllers
{
    public class FuncionarioController : System.Web.Mvc.Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            return View(Controller.Funcionario.Listar());
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        public ActionResult Create(Models.Funcionario funcionario)
        {
            try
            {
                Controller.Funcionario.Salvar(funcionario);
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
            return View(Controller.Funcionario.BuscarPorId(id));
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Models.Funcionario funcionario)
        {
            try
            {
                Controller.Funcionario.Salvar(funcionario);
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
            return View(Controller.Funcionario.BuscarPorId(id));
        }

        // POST: Cliente/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Controller.Funcionario.Excluir(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
