using System;
using MercadoCore;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MercadoApplication
{
    public class ContatoHandler : IEntityCrudHandler<Contato>
    {
        private readonly IMercadoDbContext db;

        public ContatoHandler(IMercadoDbContext db)
        {
            this.db = db;
        }
        public async Task<int> Alterar(int id, Contato contato, string userID)
        {
            Contato contatoToAlter = await db.Contatos.SingleOrDefaultAsync(
                p => p.IdContato == id && p.UserID == userID);

            if (id == contato.IdContato)
            {
                contatoToAlter.NomeResponsavel = contato.NomeResponsavel ?? contatoToAlter.NomeResponsavel;
                contatoToAlter.Telefone = contato.Telefone ?? contatoToAlter.Telefone;
                contatoToAlter.Email = contato.Email ?? contatoToAlter.Email;
                return await db.SaveChangesAsync();
            }

            return await Task.FromResult(0);
        }

        public async Task<int> Inserir(Contato contato)
        {
            db.Contatos.Add(contato);
            return await db.SaveChangesAsync();
        }

        public async Task<Contato[]> Listar(string userID)
        {
            return await db.Contatos
                .Where(p => p.UserID == userID)
                .Include(p => p.Produtor)
                .ToArrayAsync();
        }

        public async Task<Contato> ObterUm(int id, string userID)
        {
            return await db.Contatos
                .SingleOrDefaultAsync(p => p.IdContato == id && p.UserID == userID);
        }

        public async Task<int> Remover(int id, string userID)
        {
            Contato contatoToRemove = await db.Contatos.SingleOrDefaultAsync(
                p => p.IdContato == id && p.UserID == userID);

            var outrosContatos = await db.Contatos
                .Where(c => c.IdProdutor == contatoToRemove.IdProdutor &&
                c.IdContato != id)
                .ToListAsync();

            if(outrosContatos.Count() > 0)
            {
                if (contatoToRemove != null)
                {
                    db.Contatos.Remove(contatoToRemove);
                    return await db.SaveChangesAsync();
                }
            }

            return await Task.FromResult(0);
        }
    }
}
