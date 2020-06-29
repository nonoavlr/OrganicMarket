using System;
using MercadoCore;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MercadoApplication
{
    public class PedidoHandler:IEntityCrudHandler<Pedido>
    {
        private readonly IMercadoDbContext db;

        public PedidoHandler(IMercadoDbContext db)
        {
            this.db = db;
        }

        public async Task<int> Alterar(int id, Pedido pedido, string userID)
        {
            Pedido pedidoToAlter = await db.Pedidos.SingleOrDefaultAsync(
                p => p.IdPedido == id && p.UserID == userID);

            if(pedidoToAlter != null)
            {
                var novositems = pedido.ItemPedidos;
                var items = pedidoToAlter.ItemPedidos;

                ItemPedidoHandler itemHandler = new ItemPedidoHandler(db);

                foreach (var velho in items)
                {
                    if (novositems.SingleOrDefault(i => i.IdItemPedido == velho.IdItemPedido) == null)
                    {
                        await itemHandler.Remover(velho.IdItemPedido, userID);
                    }
                }

                foreach (var novo in novositems)
                {
                    if(items.SingleOrDefault(i => i.IdItemPedido == novo.IdItemPedido) != null)
                    {
                       await itemHandler.Alterar(novo.IdItemPedido, novo, userID);
                    }
                    else
                    {
                        await itemHandler.Inserir(novo);
                    }
                }

                return await db.SaveChangesAsync();
            }

            return await Task.FromResult(0);
        }

        public async Task<int> Inserir(Pedido pedido)
        {
            db.Pedidos.Add(pedido);
            return await db.SaveChangesAsync();
        }

        public async Task<Pedido[]> Listar(string userID)
        {
            return await db.Pedidos
                .Where(p => p.UserID == userID)
                .Include(p => p.ItemPedidos)
                .Include(p => p.Comprador)
                .ToArrayAsync();

            //Fiquei confuso nesse, estaria correto?
        }

        public async Task<Pedido> ObterUm(int id, string userID)
        {
            return await db.Pedidos
                .Include(p => p.ItemPedidos)
                .Include(p => p.Comprador)
                .SingleOrDefaultAsync(p => p.IdPedido == id && p.UserID == userID);
        }

        public async Task<int> Remover(int id, string userID)
        {
            Pedido pedidoToRemove = await db.Pedidos.SingleOrDefaultAsync(
                p => p.IdPedido == id && p.UserID == userID);

            if(pedidoToRemove != null)
            {
                db.Pedidos.Remove(pedidoToRemove);
                return await db.SaveChangesAsync();
            }

            return await Task.FromResult(0);
        }
    }
}
