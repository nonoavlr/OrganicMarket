using MercadoCore;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MercadoApplication
{
    public class ProdutoHandler:IEntityCrudHandler<Produto>
    {
        private readonly IMercadoDbContext db;

        public ProdutoHandler(IMercadoDbContext db)
        {
            this.db = db;
        }

        public async Task<int> Alterar(int id, Produto produto, string userID)
        {
            Produto produtoToAlter = await db.Produtos
                .Include(p => p.TipoProduto)
                .Include(p => p.TipoQuantidade)
                .Include(p => p.Produtor)
                .SingleOrDefaultAsync(p => p.IdProduto == id && p.UserID == userID);

            if(id == produto.IdProduto)
            {
                produtoToAlter.Titulo = produto.Titulo ?? produto.Titulo;
                produtoToAlter.Descricao = produto.Descricao ?? produto.Descricao;
                produtoToAlter.Preco = produto.Preco;
                produtoToAlter.Quantidade = produto.Quantidade;
                //Fazer TipoProduto e TipoQuantidade
                return await db.SaveChangesAsync();
            }

            return await Task.FromResult(0);

        }

        public async Task<int> Inserir(Produto produto)
        {
            db.Produtos.Add(produto);
            return await db.SaveChangesAsync();
        }

        public async Task<Produto[]> Listar(string userID)
        {
            return await db.Produtos.ToArrayAsync();
        }

        public async Task<Produto[]> ListarProdutosProdutor(int id, string userID)
        {
            return await db.Produtos
                .Where(p => p.IdProdutor == id)
                .Include(p => p.TipoProduto)
                .Include(p => p.TipoQuantidade)
                .Include(p => p.Produtor)
                .ToArrayAsync();
        }

        public async Task<Produto> ObterUm(int id, string userID)
        {
            return await db.Produtos
                .Include(p => p.TipoProduto)
                .Include(p => p.TipoQuantidade)
                .Include(p => p.Produtor)
                .SingleOrDefaultAsync(p => p.IdProdutor == id);
        }

        public async Task<int> Remover(int id, string userID)
        {
            Produto produtoToRemove = await db.Produtos.SingleOrDefaultAsync(
                p => p.IdProduto == id && p.UserID == userID);

            if(produtoToRemove != null)
            {
                db.Produtos.Remove(produtoToRemove);
                return await db.SaveChangesAsync();
            }

            return await Task.FromResult(0);
        }
    }
}
