using System;
using MercadoCore;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MercadoApplication
{
    public class TipoProdutoHandler:IEntityCrudHandler<TipoProduto>
    {
        private readonly IMercadoDbContext db;

        public TipoProdutoHandler(IMercadoDbContext db)
        {
            this.db = db;
        }

        public async Task<int> Alterar(int id, TipoProduto tipoProduto, string userID)
        {
            TipoProduto tipoProdutoToAlter = await db.TiposProduto.SingleOrDefaultAsync(
                p => p.IdTipoProduto == id);

            if(tipoProdutoToAlter != null)
            {
                tipoProdutoToAlter.Descricao = tipoProduto.Descricao ?? tipoProdutoToAlter.Descricao;
                return await db.SaveChangesAsync();
            }

            return await Task.FromResult(0);
        }

        public async Task<int> Inserir(TipoProduto tipoProduto)
        {
            db.TiposProduto.Add(tipoProduto);

            return await db.SaveChangesAsync();
        }

        public async Task<TipoProduto[]> Listar(string userID)
        {
            return await db.TiposProduto
                .Include(p => p.Produtos)
                .ToArrayAsync();
        }

        public async Task<TipoProduto> ObterUm(int id, string userID)
        {
            return await db.TiposProduto
                .Include(p => p.Produtos)
                .SingleOrDefaultAsync(p => p.IdTipoProduto == id);
        }

        public async Task<int> Remover(int id, string userID)
        {
            TipoProduto tipoProdutoToRemove = await db.TiposProduto.SingleOrDefaultAsync(
                p => p.IdTipoProduto == id);

            if(tipoProdutoToRemove != null)
            {
                db.TiposProduto.Remove(tipoProdutoToRemove);
                return await db.SaveChangesAsync();
            }

            return await Task.FromResult(0);
        }
    }
}
