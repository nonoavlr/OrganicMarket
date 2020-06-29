using System;
using MercadoCore;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MercadoApplication
{
    public class ItemPedidoHandler:IEntityCrudHandler<ItemPedido>
    {
        private readonly IMercadoDbContext db;

        public ItemPedidoHandler(IMercadoDbContext db)
        {
            this.db = db;
        }

        public async Task<int> Alterar(int id, ItemPedido itempedido, string userID)
        {
            ItemPedido itemPedidoToAlter = await db.ItemsPedido
                .Include(p => p.Pedido)
                .SingleOrDefaultAsync(
                p => p.IdItemPedido == id && p.UserID == userID);

            if(itempedido != null)
            {
                itemPedidoToAlter.Quantidade = itempedido.Quantidade;

                itemPedidoToAlter.SubTotal =
                    itemPedidoToAlter.Produto.Preco * itemPedidoToAlter.Quantidade;

                return await db.SaveChangesAsync();
            }

            return await Task.FromResult(0);
        }

        public async Task<int> Inserir(ItemPedido itempedido)
        {
            db.ItemsPedido.Add(itempedido);
            itempedido.SubTotal =
                    itempedido.Produto.Preco * itempedido.Quantidade;

            return await db.SaveChangesAsync();
        }

        public async Task<ItemPedido[]> Listar(string userID)
        {
            return await db.ItemsPedido
                .Include(p => p.Produto)
                .Include(p => p.Pedido)
                .ToArrayAsync();
        }

        public async Task<ItemPedido> ObterUm(int id, string userID)
        {
            return await db.ItemsPedido
                .Include(p => p.Produto)
                .Include(p => p.Pedido)
                .SingleOrDefaultAsync(p => p.IdItemPedido == id);
        }

        public async Task<int> Remover(int id, string userID)
        {
            ItemPedido itempedido = await db.ItemsPedido.SingleOrDefaultAsync(
                p => p.UserID == userID && p.IdItemPedido == id);

            if(itempedido != null)
            {
                db.ItemsPedido.Remove(itempedido);

                return await db.SaveChangesAsync();
            }

            return await Task.FromResult(0);
        }
    }
}
