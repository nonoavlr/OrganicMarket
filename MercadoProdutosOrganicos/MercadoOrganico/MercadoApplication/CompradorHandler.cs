using MercadoCore;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MercadoApplication
{
    public class CompradorHandler : IEntityCrudHandler<Comprador>
    {
        private readonly IMercadoDbContext db;

        public CompradorHandler(IMercadoDbContext db)
        {
            this.db = db;
        }
        public async Task<int> Alterar(int id, Comprador comprador, string userID)
        {
            Comprador compradorToAlter = await db.Compradores
                .Include(p => p.Endereco)
                .SingleOrDefaultAsync(
                p => p.IdComprador == id && p.UserID == userID
                )
                ;

            if (compradorToAlter != null)
            {
                compradorToAlter.NomeResponsavel = comprador.NomeResponsavel ?? compradorToAlter.NomeResponsavel;
                compradorToAlter.Telefone = comprador.Telefone ?? compradorToAlter.Telefone;
                compradorToAlter.Email = comprador.Email ?? compradorToAlter.Email;
                compradorToAlter.CPF = comprador.CPF ?? compradorToAlter.CPF;

                EnderecoHandler enderecoHandler = new EnderecoHandler(db);

                if(comprador.Endereco != null)
                {
                    if(compradorToAlter.Endereco != null)
                    {
                        comprador.Endereco.IdEndereco = id;
                        await enderecoHandler.Inserir(comprador.Endereco);
                    }
                    else
                    {
                        await enderecoHandler.Alterar(compradorToAlter.Endereco.IdEndereco,
                            comprador.Endereco, userID);
                    }
                }

                return await db.SaveChangesAsync();
            }

            return await Task.FromResult(0);
        }

        public async Task<int> Inserir(Comprador comprador)
        {
            db.Compradores.Add(comprador);
            return await db.SaveChangesAsync();
        }

        public async Task<Comprador[]> Listar(string userID)
        {
            return await db.Compradores
                .Where(p => p.IsAtivo)
                .Include(p => p.Endereco)
                .Include(p => p.Pedidos)
                .ToArrayAsync();
        }

        public async Task<Comprador> ObterUm(int Id, string userId)
        {
            return await db.Compradores
                .Where(p => p.IsAtivo)
                .Include(p => p.Endereco)
                .Include(p => p.Pedidos)
                .SingleOrDefaultAsync(
                l => l.IdComprador == Id && l.UserID == userId
            );
        }

        public async Task<int> Remover(int id, string userID)
        {
            Comprador compradorToRemove = await db.Compradores.SingleOrDefaultAsync(
                p => p.IdComprador == id && p.UserID == userID);

            if(compradorToRemove != null)
            {
                compradorToRemove.IsAtivo = false;
                //db.Compradores.Remove(compradorToRemove);
                return await db.SaveChangesAsync();
            }

            return await Task.FromResult(0);
        }
    }
}
