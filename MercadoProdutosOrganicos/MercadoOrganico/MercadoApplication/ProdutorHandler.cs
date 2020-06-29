using System;
using System.Collections.Generic;
using System.Text;
using MercadoCore;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MercadoApplication
{
    public class ProdutorHandler : IEntityCrudHandler<Produtor>
    {
        private readonly IMercadoDbContext db;

        public ProdutorHandler(IMercadoDbContext db)
        {
            this.db = db;
        }
        public async Task<int> Alterar(int id, Produtor produtor, string userID)
        {
            Produtor produtorToAlter = await db.Produtores
                .Include(p => p.Produtos)
                .Include(p => p.Contatos)
                .Include(p => p.Endereco)
                .SingleOrDefaultAsync(p => p.IdProdutor == id && p.UserID == userID);

            if(produtorToAlter != null)
            {
                produtorToAlter.RazaoSocial = produtor.RazaoSocial ?? produtor.RazaoSocial;
                produtorToAlter.CNPJ = produtor.CNPJ ?? produtor.CNPJ;

                ContatoHandler contatoHandler = new ContatoHandler(db);

                if (produtor.Contatos != null)
                {
                    var novositems = produtor.Contatos;
                    var items = produtorToAlter.Contatos;

                    ItemPedidoHandler itemHandler = new ItemPedidoHandler(db);

                    foreach (var velho in items)
                    {
                        if (novositems.SingleOrDefault(i => i.IdContato == velho.IdContato) == null)
                        {
                            await contatoHandler.Remover(velho.IdContato, userID);
                        }
                    }

                    foreach (var novo in novositems)
                    {
                        if (items.SingleOrDefault(i => i.IdContato == novo.IdContato) != null)
                        {
                            await contatoHandler.Alterar(novo.IdContato, novo, userID);
                        }
                        else
                        {
                            await contatoHandler.Inserir(novo);
                        }
                    }
                }

                return await db.SaveChangesAsync();
            }

            return await Task.FromResult(0);
        }

        public async Task<int> Inserir(Produtor produtor)
        {
            db.Produtores.Add(produtor);
            return await db.SaveChangesAsync();
        }

        public async Task<Produtor[]> Listar(string userID)
        {
            return await db.Produtores
                 .Where(p => p.IsAtivo)
                 .Include(p => p.Produtos)
                 .Include(p => p.Contatos)
                 .Include(p => p.Endereco)
                 .ToArrayAsync();
        }

        public async Task<Produtor> ObterUm(int id, string userID)
        {
            return await db.Produtores
                 .Where(p => p.IsAtivo)
                 .Include(p => p.Produtos)
                 .Include(p => p.Contatos)
                 .Include(p => p.Endereco)
                 .SingleOrDefaultAsync(p => p.IdProdutor == id);
        }

        public async Task<int> Remover(int id, string userID)
        {
            Produtor produtorToRemove = await db.Produtores.SingleOrDefaultAsync(
                p => p.IdProdutor == id && p.UserID == userID);

            if(produtorToRemove != null)
            {
                //db.Produtores.Remove(produtorToRemove);

                produtorToRemove.IsAtivo = false;
                return await db.SaveChangesAsync();
            }

            return await Task.FromResult(0);
        }
    }
}
