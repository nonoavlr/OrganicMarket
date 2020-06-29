using System;
using MercadoCore;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MercadoApplication
{
    public class TipoQuantidadeHandler:IEntityCrudHandler<TipoQuantidade>
    {
        private readonly IMercadoDbContext db;

        public TipoQuantidadeHandler (IMercadoDbContext db)
        {
            this.db = db;
        }

        public async Task<int> Alterar(int id, TipoQuantidade tipoQuantidade, string userID)
        {
            TipoQuantidade tipoQuantidadeToAlter = await db.TiposQuantidade
                .SingleOrDefaultAsync(p => p.IdTipoQuantidade == id);

            if(tipoQuantidadeToAlter != null)
            {
                tipoQuantidadeToAlter.Descricao = tipoQuantidade.Descricao ?? tipoQuantidadeToAlter.Descricao;
                return await db.SaveChangesAsync();
            }

            return await Task.FromResult(0);
        }

        public async Task<int> Inserir(TipoQuantidade tipoQuantidade)
        {
            db.TiposQuantidade.Add(tipoQuantidade);
            return await db.SaveChangesAsync();
        }

        public async Task<TipoQuantidade[]> Listar(string userID)
        {
            return await db.TiposQuantidade
                .Include(p => p.Produtos)
                .ToArrayAsync();
        }

        public async Task<TipoQuantidade> ObterUm(int id, string userID)
        {
            return await db.TiposQuantidade
                .Include(p => p.Produtos)
                .SingleOrDefaultAsync(p => p.IdTipoQuantidade == id);
        }

        public async Task<int> Remover(int id, string userID)
        {
            TipoQuantidade tipoQuantidadeToRemove = await db.TiposQuantidade
                .SingleOrDefaultAsync(p => p.IdTipoQuantidade == id);

            if(tipoQuantidadeToRemove != null)
            {
                db.TiposQuantidade.Remove(tipoQuantidadeToRemove);

                return await db.SaveChangesAsync();
            }

            return await Task.FromResult(0);
        }
    }
}
