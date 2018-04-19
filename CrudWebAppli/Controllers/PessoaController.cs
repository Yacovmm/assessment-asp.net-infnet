using CrudWebAppli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static CrudWebAppli.Models.Repositorio.RepositorioPessoa;

namespace CrudWebAppli.Controllers
{
    public class PessoaController : Controller
    {
        private PessoaRepository respository = new PessoaRepository();
       

        // GET: Pessoa
        public ActionResult Index()
        {
          
            return View(respository.GetAll());
        }

        // GET: Pessoa/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Ordenada() {

            DateTime hoje = DateTime.Now;

            respository = new PessoaRepository();

            List<Pessoa> lst_pessoa = respository.GetAll();

            ViewBag.Pessoas = lst_pessoa;

            List<Pessoa> lst_aniversariante = lst_pessoa.Where(p => p.Dt_nascimento.Day == hoje.Day && p.Dt_nascimento.Month == hoje.Month).ToList();

            ViewBag.AniversariantesDoDia = lst_aniversariante;

            ModelState.Clear();

            return View();

        }
       
        [HttpGet]
        public ActionResult Pesquisa()
        {
            return View(respository.Pessoa);
        }

        [HttpPost]
        public ActionResult Pesquisa(string buscar)
        {
            respository = new PessoaRepository();
            List<Pessoa> lst_pessoa = respository.GetAll();
            lst_pessoa.Where(p => p.Nome.ToUpper().Contains(buscar.ToUpper())).ToList();

            return View(lst_pessoa);

            
        }
            


        // POST: Pessoa/Create
        [HttpPost]
        public ActionResult Create(Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                respository.Save(pessoa);
                return RedirectToAction("Index");
            }
            else
            {
                return View(pessoa);
            }
        }

        // GET: Pessoa/Edit/5
        public ActionResult Edit(int id)
        {
            var pessoa = respository.GetById(id);

            if (pessoa == null)
            {
                return HttpNotFound();
            }

            return View(pessoa);
        }

        // POST: Pessoa/Edit/5
        [HttpPost]
        public ActionResult Edit(Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                respository.Update(pessoa);
                return RedirectToAction("Index");
            }
            else
            {
                return View(pessoa);
            }
        }

        // POST: Pessoa/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            respository.DeleteById(id);
            RedirectToAction("Index");
            return Json(respository.GetAll());
        }


        

        
        

    }
}



