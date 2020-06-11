using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using ProgramaSRDAMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using LiteDatabase.Services;

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
            TempData["Msg"] = "Entrada adicionada com sucesso";
            return RedirectToAction("Adicionar");
        }

        public ActionResult Lista(){
            ViewBag.lista = new DataBase().Listar();
            return View();
        }

        public ActionResult Excluir(Guid id){
            new DataBase().Excluir(id);
            return RedirectToAction("Lista");
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
            return RedirectToAction("Lista");
        }

        [HttpGet]
        public ActionResult Index()
        {
            var obj = new List<EntradaDTO>();

            System.IO.StreamReader reader;
            string caminho = "D:\\_Programação\\ProgramaSRDAMVC\\BancoDeDados\\entradas.txt";
            reader = System.IO.File.OpenText(caminho);

            while (reader.EndOfStream != true)
            {
                // teste
                string linha = reader.ReadLine();
                String[] separados = linha.Split(";");
                obj.Add(new EntradaDTO()
                {
                    Data = Convert.ToDateTime(separados[0]),
                    Hora = separados[1],
                    Comentario = separados[2]
                });
            }
            reader.Close();
            return View(obj);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
