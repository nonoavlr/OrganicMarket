using System;
using MercadoCore;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace MercadoApplication
{
    public  class EnderecoHandler:IEntityCrudHandler<Endereco>
    {
        private readonly IMercadoDbContext db;

        public EnderecoHandler(IMercadoDbContext db)
        {
            this.db = db;
        }

        public async Task<int> Alterar(int id, Endereco endereco, string userID)
        {
            Endereco enderecoToAlter = await db.Enderecos.SingleAsync(p => p.IdEndereco == id && p.UserID == userID);

            if(id == endereco.IdEndereco)
            {
                enderecoToAlter.CEP = endereco.CEP ?? enderecoToAlter.CEP;
                enderecoToAlter.TipoLogradouro = endereco.TipoLogradouro ?? enderecoToAlter.TipoLogradouro;
                enderecoToAlter.Logradouro = endereco.Logradouro ?? enderecoToAlter.Logradouro;
                enderecoToAlter.Bairro = endereco.Bairro ?? enderecoToAlter.Bairro;
                enderecoToAlter.Cidade = endereco.Cidade ?? enderecoToAlter.Cidade;
                enderecoToAlter.Estado = endereco.Estado ?? enderecoToAlter.Estado;
                return await db.SaveChangesAsync();
            }

            return await Task.FromResult(0);
        }

        public async Task<int> Inserir(Endereco endereco)
        {
            db.Enderecos.Add(endereco);
            return await db.SaveChangesAsync();

        }

        public async Task<Endereco[]> Listar(string userID)
        {
            return await db.Enderecos.ToArrayAsync();
        }

        public Task<Endereco> ObterUm(int id, string userID)
        {
            return db.Enderecos.SingleOrDefaultAsync(p => p.IdEndereco == id && p.UserID == userID);
        }

        public async Task<int> Remover(int id, string userID)
        {
            Endereco enderecoToRemove = await db.Enderecos.SingleOrDefaultAsync(
                p => p.IdEndereco == id && p.UserID == userID);

            if(enderecoToRemove != null)
            {
                db.Enderecos.Remove(enderecoToRemove);

                return await db.SaveChangesAsync();
            }

            return await Task.FromResult(0);
        }
    }
}
