using System;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using MercadoCore;

namespace MercadoApplication
{
    public interface IMercadoDbContext
    {
        public DbSet<Comprador> Compradores { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<ItemPedido> ItemsPedido { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Produtor> Produtores { get; set; }
        public DbSet<TipoProduto> TiposProduto { get; set; }
        public DbSet<TipoQuantidade> TiposQuantidade { get; set; }
        public Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
