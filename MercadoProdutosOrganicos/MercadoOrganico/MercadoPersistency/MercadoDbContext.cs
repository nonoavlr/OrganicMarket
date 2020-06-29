using MercadoApplication;
using MercadoCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MercadoPersistency
{
    public class MercadoDbContext : IdentityDbContext, IMercadoDbContext
    {
        public MercadoDbContext(DbContextOptions<MercadoDbContext> options)
            :base(options)
        {
        }

        public DbSet<Comprador> Compradores { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<ItemPedido> ItemsPedido { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Produtor> Produtores { get; set; }
        public DbSet<TipoProduto> TiposProduto { get; set; }
        public DbSet<TipoQuantidade> TiposQuantidade { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken token = default)
        {
            return await base.SaveChangesAsync(token);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comprador>().HasKey(p => p.IdComprador);
            modelBuilder.Entity<Comprador>().Property(p => p.IdComprador).ValueGeneratedOnAdd();
            modelBuilder.Entity<Comprador>().Property(p => p.UserID).IsRequired();
            modelBuilder.Entity<Comprador>().Property(p => p.CPF).IsRequired();
            modelBuilder.Entity<Comprador>().Property(p => p.CPF).HasMaxLength(11);
            modelBuilder.Entity<Comprador>().Property(p => p.Email).IsRequired();
            modelBuilder.Entity<Comprador>().Property(p => p.Email).HasMaxLength(30);
            modelBuilder.Entity<Comprador>().Property(p => p.NomeResponsavel).IsRequired();
            modelBuilder.Entity<Comprador>().Property(p => p.NomeResponsavel).HasMaxLength(40);
            modelBuilder.Entity<Comprador>().Property(p => p.Telefone).IsRequired();
            modelBuilder.Entity<Comprador>().Property(p => p.Telefone).HasMaxLength(15);
            modelBuilder.Entity<Comprador>().Property(p => p.IdEndereco).IsRequired();
            modelBuilder.Entity<Comprador>().Property(p => p.IsAtivo).IsRequired();

            modelBuilder.Entity<Comprador>()
                .HasOne(p => p.Endereco)
                .WithOne(p => p.Comprador)
                .HasForeignKey<Comprador>(p => p.IdEndereco);

            modelBuilder.Entity<Comprador>()
                .HasMany(p => p.Pedidos)
                .WithOne(p => p.Comprador)
                .HasForeignKey(p => p.IdComprador);

            modelBuilder.Entity<Contato>().HasKey(p => p.IdContato);
            modelBuilder.Entity<Contato>().Property(p => p.IdContato).ValueGeneratedOnAdd();
            modelBuilder.Entity<Contato>().Property(p => p.IdProdutor).IsRequired();
            modelBuilder.Entity<Contato>().Property(p => p.UserID).IsRequired();
            modelBuilder.Entity<Contato>().Property(p => p.NomeResponsavel).IsRequired();
            modelBuilder.Entity<Contato>().Property(p => p.NomeResponsavel).HasMaxLength(40);
            modelBuilder.Entity<Contato>().Property(p => p.Email).IsRequired();
            modelBuilder.Entity<Contato>().Property(p => p.Email).HasMaxLength(30);
            modelBuilder.Entity<Contato>().Property(p => p.Telefone).IsRequired();
            modelBuilder.Entity<Contato>().Property(p => p.Telefone).HasMaxLength(15);

            modelBuilder.Entity<Contato>()
                .HasOne(c => c.Produtor)
                .WithMany(c => c.Contatos)
                .HasForeignKey(c => c.IdProdutor);

            modelBuilder.Entity<Endereco>().HasKey(p => p.IdEndereco);
            modelBuilder.Entity<Endereco>().Property(p => p.IdEndereco).ValueGeneratedOnAdd();
            modelBuilder.Entity<Endereco>().Property(p => p.UserID).IsRequired();
            modelBuilder.Entity<Endereco>().Property(p => p.TipoLogradouro).IsRequired();
            modelBuilder.Entity<Endereco>().Property(p => p.TipoLogradouro).HasMaxLength(20);
            modelBuilder.Entity<Endereco>().Property(p => p.Logradouro).IsRequired();
            modelBuilder.Entity<Endereco>().Property(p => p.Logradouro).HasMaxLength(30);
            modelBuilder.Entity<Endereco>().Property(p => p.Estado).IsRequired();
            modelBuilder.Entity<Endereco>().Property(p => p.Estado).HasMaxLength(2);
            modelBuilder.Entity<Endereco>().Property(p => p.Cidade).IsRequired();
            modelBuilder.Entity<Endereco>().Property(p => p.Cidade).HasMaxLength(25);
            modelBuilder.Entity<Endereco>().Property(p => p.Bairro).IsRequired();
            modelBuilder.Entity<Endereco>().Property(p => p.Bairro).HasMaxLength(20);
            modelBuilder.Entity<Endereco>().Property(p => p.CEP).IsRequired();
            modelBuilder.Entity<Endereco>().Property(p => p.CEP).HasMaxLength(8);

            modelBuilder.Entity<ItemPedido>().HasKey(p => p.IdItemPedido);
            modelBuilder.Entity<ItemPedido>().Property(p => p.IdItemPedido).ValueGeneratedOnAdd();
            modelBuilder.Entity<ItemPedido>().Property(p => p.UserID).IsRequired();
            modelBuilder.Entity<ItemPedido>().Property(p => p.Quantidade).IsRequired();

            modelBuilder.Entity<Pedido>().HasKey(p => p.IdPedido);
            modelBuilder.Entity<Pedido>().Property(p => p.IdPedido).ValueGeneratedOnAdd();
            modelBuilder.Entity<Pedido>().Property(p => p.UserID).IsRequired();

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Comprador)
                .WithMany(p => p.Pedidos)
                .HasForeignKey(p => p.IdComprador);

            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.ItemPedidos)
                .WithOne(p => p.Pedido)
                .HasForeignKey(p => p.IdPedido);


            modelBuilder.Entity<Produto>().HasKey(p => p.IdProduto);
            modelBuilder.Entity<Produto>().Property(p => p.IdProduto).ValueGeneratedOnAdd();
            modelBuilder.Entity<Produto>().Property(p => p.UserID).IsRequired();
            modelBuilder.Entity<Produto>().Property(p => p.Titulo).IsRequired();
            modelBuilder.Entity<Produto>().Property(p => p.Titulo).HasMaxLength(30);
            modelBuilder.Entity<Produto>().Property(p => p.Descricao).IsRequired();
            modelBuilder.Entity<Produto>().Property(p => p.Descricao).HasMaxLength(60);
            modelBuilder.Entity<Produto>().Property(p => p.Preco).IsRequired();
            modelBuilder.Entity<Produto>().Property(p => p.Preco).HasMaxLength(30);
            modelBuilder.Entity<Produto>().Property(p => p.Quantidade).IsRequired();
            modelBuilder.Entity<Produto>().Property(p => p.Quantidade).HasMaxLength(60);

            modelBuilder.Entity<Produto>()
                .HasOne(p => p.Produtor)
                .WithMany(p => p.Produtos)
                .HasForeignKey(p => p.IdProduto);

            modelBuilder.Entity<Produto>()
                .HasOne(p => p.TipoProduto)
                .WithMany(p => p.Produtos)
                .HasForeignKey(p => p.IdTipoProduto);

            modelBuilder.Entity<Produto>()
                .HasOne(p => p.TipoQuantidade)
                .WithMany(p => p.Produtos)
                .HasForeignKey(p => p.IdTipoQuantidade);

            modelBuilder.Entity<Produto>()
                .HasMany(p => p.ItemPedido)
                .WithOne(p => p.Produto)
                .HasForeignKey(p => p.IdProduto);
                

            modelBuilder.Entity<Produtor>().HasKey(p => p.IdProdutor);
            modelBuilder.Entity<Produtor>().Property(p => p.IdProdutor).ValueGeneratedOnAdd();
            modelBuilder.Entity<Produtor>().Property(p => p.UserID).IsRequired();
            modelBuilder.Entity<Produtor>().Property(p => p.RazaoSocial).IsRequired();
            modelBuilder.Entity<Produtor>().Property(p => p.RazaoSocial).HasMaxLength(20);
            modelBuilder.Entity<Produtor>().Property(p => p.CNPJ).IsRequired();
            modelBuilder.Entity<Produtor>().Property(p => p.CNPJ).HasMaxLength(15);
            modelBuilder.Entity<Produtor>().Property(p => p.IsAtivo).IsRequired();

            modelBuilder.Entity<Produtor>()
                .HasOne(p => p.Endereco)
                .WithOne(p => p.Produtor)
                .HasForeignKey<Produtor>(p => p.IdEndereco);

            modelBuilder.Entity<Produtor>()
                .HasMany(p => p.Contatos)
                .WithOne(p => p.Produtor)
                .HasForeignKey(p => p.IdProdutor);

            modelBuilder.Entity<Produtor>()
                .HasMany(p => p.Produtos)
                .WithOne(p => p.Produtor)
                .HasForeignKey(p => p.IdProdutor);


            modelBuilder.Entity<TipoProduto>().HasKey(p => p.IdTipoProduto);
            modelBuilder.Entity<TipoProduto>().Property(p => p.IdTipoProduto).ValueGeneratedOnAdd();

            modelBuilder.Entity<TipoProduto>().HasData(new TipoProduto[] {
                new TipoProduto() {IdTipoProduto = 1, Descricao = "Frutas"},
                new TipoProduto() {IdTipoProduto = 2, Descricao = "Verduras"},
                new TipoProduto() {IdTipoProduto = 3, Descricao = "Legumes"},
                new TipoProduto() {IdTipoProduto = 4, Descricao = "Grãos"}
            });

            modelBuilder.Entity<TipoQuantidade>().HasKey(p => p.IdTipoQuantidade);
            modelBuilder.Entity<TipoQuantidade>().Property(p => p.IdTipoQuantidade).ValueGeneratedOnAdd();

            modelBuilder.Entity<TipoQuantidade>().HasData(new TipoQuantidade[] {
                new TipoQuantidade() {IdTipoQuantidade = 1, Descricao = "Unidade"},
                new TipoQuantidade() {IdTipoQuantidade = 2, Descricao = "Quilos"},
            });

            base.OnModelCreating(modelBuilder);
        }
    }

}
