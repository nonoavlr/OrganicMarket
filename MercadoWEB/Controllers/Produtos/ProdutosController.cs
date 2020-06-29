using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MercadoApplication;
using MercadoCore;
using MercadoWeb.Models.AccountCreateModel;
using MercadoWeb.Models.Produtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MercadoWeb.Controllers.Produtos
{
    public class ProdutosController : Controller
    {
        private readonly IEntityCrudHandler<Produto> produtoHandler;
        private readonly IEntityCrudHandler<TipoProduto> tipoProdutoHandler;
        private readonly IEntityCrudHandler<TipoQuantidade> tipoQuantidadeHandler;
        private readonly IEntityCrudHandler<Produtor> produtorHandler;
        private readonly UserManager<IdentityUser> userManager;

        public ProdutosController(
            IEntityCrudHandler<Produto> produtoHandler,
            IEntityCrudHandler<TipoQuantidade> tipoQuantidadeHandler,
            IEntityCrudHandler<TipoProduto> tipoProdutoHandler,
            IEntityCrudHandler<Produtor> produtorHandler,
            UserManager<IdentityUser> userManager)
        {
            this.produtoHandler = produtoHandler;
            this.tipoQuantidadeHandler = tipoQuantidadeHandler;
            this.tipoProdutoHandler = tipoProdutoHandler;
            this.produtorHandler = produtorHandler;
            this.userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> CreateProduto()
        {
            var userID = userManager.GetUserId(User);

            Produtor[] produtores = await produtorHandler.Listar(userID);
            TipoQuantidade[] tiposQuantidade = await tipoQuantidadeHandler.Listar(userID);
            TipoProduto[] tiposProduto = await tipoProdutoHandler.Listar(userID);

            var produtor = produtores.FirstOrDefault(p => p.UserID == userID);

            if (produtor != null)
            {
                CreateProdutoModelView produtomodel = new CreateProdutoModelView()
                {
                    TipoQuantidade = new SelectList(tiposQuantidade, "ID", "Descricao"),
                    TipoProduto = new SelectList(tiposProduto, "ID", "Descricao")
                };

                return View(produtomodel);
            }
 

            return RedirectToAction("ChooseAccount");
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduto(CreateProdutoModelView produtomodel)
        {
            var userID = userManager.GetUserId(User);
            Produtor[] produtores = await produtorHandler.Listar(userID);
            var produtor = produtores.FirstOrDefault(p => p.UserID == userID);
            var idProdutor = produtor.IdProdutor;

            if (ModelState.IsValid)
            {
                var produto = produtomodel.GetProdutoObject(userID,idProdutor);
                await produtoHandler.Inserir(produto);

                return RedirectToAction("Index", "Home");
            }


            return RedirectToAction("ChooseAccount");
        }
    }
}