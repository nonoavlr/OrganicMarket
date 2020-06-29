using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MercadoWeb.Models;
using MercadoApplication;
using MercadoCore;

namespace MercadoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEntityCrudHandler<Produto> produtoHandler;

        public HomeController(ILogger<HomeController> logger, IEntityCrudHandler<Produto> produtoHandler)
        {
            _logger = logger;
            this.produtoHandler = produtoHandler;
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await produtoHandler.Listar(null);
            return View(produtos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
