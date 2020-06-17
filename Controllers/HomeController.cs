using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using ProgramaSRDAMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using LiteDatabase.Services;
using System.Collections.Concurrent;
using System.Linq;
using System.Collections;

namespace ProgramaSRDAMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public ActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Adicionar(EntradaDTO entrada)
        {
            new DataBase().Inserir(entrada);
            return RedirectToAction("Adicionar");
        }

        public ActionResult Excluir(Guid id){
            new DataBase().Excluir(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(Guid id)
        {
            var dados = new DataBase().BuscaPorId(id);
            return View(dados);
        }
        [HttpPost]
        public ActionResult Editar(EntradaDTO entradas)
        {
            new DataBase().Alterar(entradas);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(new DataBase().Listar().OrderByDescending(x => x.Data).ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
