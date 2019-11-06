using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Locadora.Controllers
{
    public class ClassificacaoController : System.Web.Mvc.Controller
    {
        // GET: Classificacao
        public ActionResult Index()
        {
            return View(Controller.Classificacao.Listar());
        }

        // GET: Classificacao/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Classificacao/Create
        [HttpPost]
        public ActionResult Create(Models.Classificacao classificacao)
        {
            try
            {
                Controller.Classificacao.Salvar(classificacao);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Classificacao/Edit/5
        public ActionResult Edit(int id)
        {
            return View(Controller.Classificacao.BuscarPorId(id));
        }

        // POST: Classificacao/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Models.Classificacao classificacao)
        {
            try
            {
                Controller.Classificacao.Salvar(classificacao);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Classificacao/Delete/5
        public ActionResult Delete(int id)
        {
            return View(Controller.Classificacao.BuscarPorId(id));
        }

        // POST: Classificacao/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Controller.Classificacao.Excluir(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
