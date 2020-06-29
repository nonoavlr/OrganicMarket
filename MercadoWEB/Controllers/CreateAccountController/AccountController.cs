using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MercadoApplication;
using MercadoCore;
using MercadoWeb.Models.AccountCreateModel;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MercadoWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly IEntityCrudHandler<Comprador> compradorHandler;
        private readonly IEntityCrudHandler<Produtor> produtorHandler;
        private readonly IEntityCrudHandler<Contato> contatoHandler;
        private readonly IEntityCrudHandler<Endereco> enderecoHandler;
        private readonly UserManager<IdentityUser> userManager;

        public AccountController(
            IEntityCrudHandler<Comprador> compradorHandler,
            IEntityCrudHandler<Produtor> produtorHandler,
            IEntityCrudHandler<Contato> contatoHandler,
            IEntityCrudHandler<Endereco> enderecoHandler,
            UserManager<IdentityUser> userManager)
        {
            this.compradorHandler = compradorHandler;
            this.produtorHandler = produtorHandler;
            this.contatoHandler = contatoHandler;
            this.enderecoHandler = enderecoHandler;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> ChooseAccount()
        {
            var userID = userManager.GetUserId(User);

            Produtor[] produtores = await produtorHandler.Listar(userID);
            Comprador[] compradores = await compradorHandler.Listar(userID);

            var produtor = produtores.FirstOrDefault(p => p.UserID == userID);
            var comprador = compradores.FirstOrDefault(p => p.UserID == userID);

            if(produtor == null && comprador == null)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult CreateProdutor()
        {
            var userID = this.userManager.GetUserId(User);
            AccountCreateModel model = new AccountCreateModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProdutor(AccountCreateModel model)
        {
            var userID = this.userManager.GetUserId(User);

            if(ModelState.IsValid)
            {
                var produtor = model.GetAccountCreateModelProdutor(userID);
                await produtorHandler.Inserir(produtor);

                return RedirectToAction("Index","Home");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult CreateComprador()
        {
            var userID = this.userManager.GetUserId(User);
            AccountCreateModel model = new AccountCreateModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComprador(AccountCreateModel model)
        {
            var userID = this.userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                var comprador = model.GetAccountCreateModelComprador(userID);
                await compradorHandler.Inserir(comprador);

                return RedirectToAction("Index","Home");
            }

            return View(model);
        }
    }
}